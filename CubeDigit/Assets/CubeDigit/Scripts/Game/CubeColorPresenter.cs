using UnityEngine;
using VitalRouter;

namespace CubeDigit.Game;

[Routes]
public partial class CubeColorPresenter
{
    public ICubeRenderer CubeRenderer { get; set; }

    [Route]
    public void OnCommand(SetColorCommands command)
    {
        Debug.Log($"ApplyColorCommand {command.Id} {command.Color}");

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
        CubeRenderer?.SetColor(cubeId, new CubeColor(cubeColor));
    }
}
