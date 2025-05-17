using UnityEngine;
using VitalRouter;

namespace CubeDigit.Game;

[Routes]
public partial class CubeColorPresenter
{
    /// <summary>
    /// キューブのレンダラー
    /// </summary>
    ICubeRenderer _cubeRenderer;

    /// <summary>
    /// 生成されるキューブの親Transform
    /// </summary>
    public Transform CubeParentTransform { get; set; }

    /// <summary>
    /// キューブの初期化コマンドを処理
    /// </summary>
    /// <param name="command">初期化コマンド</param>
    [Route]
    public void OnCommand(InitCubesCommand command)
    {
        //Debug.Log($"InitCubesCommand X:{command.X} Y:{command.Y} Z:{command.Z} Size:{command.Size} Spacing:{command.Spacing}");

        // CubeParentTransformがnullの場合はエラー
        if (CubeParentTransform == null)
        {
            return;
        }

        // CubeGeneratorを使用してキューブ群を生成
        var cubeGenerator = new CubeGenerator();
        _cubeRenderer = cubeGenerator.Generate(
            command.X,
            command.Y,
            command.Z,
            command.Size,
            command.Spacing,
            CubeParentTransform
        );
    }

    /// <summary>
    /// キューブの色設定コマンドを処理
    /// </summary>
    /// <param name="command">色設定コマンド</param>
    [Route]
    public void OnCommand(SetColorCommand command)
    {
        //Debug.Log($"SetColorCommand {command.Id} {command.Color}");

        // キューブレンダラーがnullの場合は何もしない
        if (_cubeRenderer == null)
        {
            return;
        }

        // コマンドのIDを取得
        // | で分割
        var idParts = command.Id.Split('|');
        if (idParts.Length < 3)
        {
            return;
        }
        if (!int.TryParse(idParts[0], out var x) ||
            !int.TryParse(idParts[1], out var y) ||
            !int.TryParse(idParts[2], out var z))
        {
            return;
        }
        var cubeId = new CubeID(x, y, z);
        // コマンドの色を取得
        var cubeColor = ColorUtility.TryParseHtmlString(command.Color, out var parsedColor) ? parsedColor : Color.white;
        // Cubeの色を変更
        _cubeRenderer?.SetColor(cubeId, new CubeColor(cubeColor));
    }
}
