using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VitalRouter;

namespace CubeDigit.Game
{
    /// <summary>
    /// JSONからキューブアニメーションを実行するクラス
    /// </summary>
    public class CubeJsonExecutor
    {
        const int FrameRate = 60;
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
            // JSONをデシリアライズ
            var cubeJson = await _loadJsonTask();
            if (cubeJson == null)
            {
                Debug.LogError("JSONのデシリアライズに失敗しました");
                return;
            }

            // キューブの初期化
            var settings = cubeJson.Settings;
            await _publisher.PublishAsync(
                new InitCubesCommand
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
                if (cubeJson.Animations.TryGetValue(cubeId, out var frames))
                {
                    var zeroFrame = frames.FirstOrDefault(f => f.Frame == 0);
                    if (zeroFrame != null)
                    {
                        color = zeroFrame.Color;
                    }
                }
                // 色を設定するコマンドを発行
                _ = _publisher.PublishAsync(new SetColorCommand { Id = cubeId, Color = color }, cancel);
            }

            // アニメーションフレームを作成
            foreach (var cubeId in cubeJson.Cubes.Keys)
            {
                var animationFrames = cubeJson.Animations.TryGetValue(cubeId, out var frames)
                    ? frames.OrderBy(f => f.Frame).ToList()
                    : new List<CubeJson.FrameJson> { new() { Frame = 0, Color = cubeJson.Cubes[cubeId] } };

                UniTask.Void(async () =>
                {
                    for (var i = 0; i < animationFrames.Count; i++)
                    {
                        var current = animationFrames[i];
                        // 色を設定するコマンドを発行
                        _ = _publisher.PublishAsync(new SetColorCommand { Id = cubeId, Color = current.Color }, cancel);
                        // 最後のフレームの場合、次のフレームは存在しない
                        if (i + 1 >= animationFrames.Count)
                        {
                            break;
                        }
                        var next = animationFrames[i + 1];
                        // 指定された時間だけ待機
                        var frameCount = next.Frame - current.Frame;
                        var seconds = frameCount / (float)FrameRate;
                        await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancel);
                    }
                });
            }
        }
    }
}
