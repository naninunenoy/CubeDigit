using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VitalRouter;

using R3;

namespace CubeDigit.Game
{
    /// <summary>
    /// JSONからキューブアニメーションを実行するクラス
    /// </summary>
    public class CubeJsonExecutor
    {
        private readonly  Func<UniTask<CubeJson>> _loadJsonTask;
        private readonly ICommandPublisher _publisher;

        public CubeJsonExecutor(ICommandPublisher publisher, Func<UniTask<CubeJson>> loadJsonTask)
        {
            _publisher = publisher;
            _loadJsonTask = loadJsonTask;
        }

        /// <summary>
        /// JSON文字列からアニメーションを読み込んで実行する
        /// </summary>
        /// <param name="cancel">キャンセルトークン</param>
        public async UniTask ExecuteAsync(CancellationToken cancel)
        {
            try
            {
                // JSONをデシリアライズ
                var cubeJson = await _loadJsonTask();
                if (cubeJson == null)
                {
                    Debug.LogError("JSONのデシリアライズに失敗しました");
                    return;
                }

                // キューブの初期化
                var settings = cubeJson.Settings;
                await _publisher.PublishAsync(new InitCubesCommand
                {
                    X = settings.CubeNumber.X,
                    Y = settings.CubeNumber.Y,
                    Z = settings.CubeNumber.Z,
                    Size = settings.CubeSize,
                    Spacing = settings.CubeSpacing
                }, cancel);
                foreach (var cubeId in cubeJson.Cubes.Keys)
                {
                    var color = cubeJson.Cubes[cubeId];
                    // 色を設定するコマンドを発行
                    _ = _publisher.PublishAsync(new SetColorCommand { Id = cubeId, Color = color }, cancel);
                }

                await UniTask.Yield();
                // アニメーションフレームを作成
                var cubeAnimations = new Dictionary<string, List<UniTask>>();
                foreach (var cubeId in cubeJson.Frames.Keys)
                {
                    var frames = cubeJson.Frames[cubeId].OrderBy(f => f.Time).ToList();
                    var lastFrame = new CubeJson.FrameJson
                    {
                        Time = float.MinValue,
                        Color = frames[^1].Color
                    };
                    frames.Add(lastFrame);
                    cubeAnimations[cubeId] = frames
                        .Zip(frames.Skip(1), (start, end) => (start.Color, Duration: end.Time - start.Time))
                        .Select(x => UniTask.Defer(async () =>
                        {
                            // 色を設定するコマンドを発行
                            _ = _publisher.PublishAsync(new SetColorCommand { Id = cubeId, Color = x.Color }, cancel);
                            // 指定された時間だけ待機
                            var waitForNext = x.Duration > 0 ? TimeSpan.FromSeconds(x.Duration) : TimeSpan.MaxValue;
                            await UniTask.Delay(waitForNext, cancellationToken: cancel);
                        }))
                        .ToList();
                }
                // アニメーションフレームを順番に実行
                foreach(var animation in cubeAnimations)
                {
                    UniTask.Void(async () =>
                    {
                        foreach (var task in animation.Value)
                        {
                            await task;
                        }
                    });
                }
            }
            catch (OperationCanceledException)
            {
                // キャンセルされた場合は何もしない
                Debug.Log("JSONアニメーションの実行がキャンセルされました");
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSONアニメーションの実行中にエラーが発生しました: {ex.Message}");
            }
        }
    }
}
