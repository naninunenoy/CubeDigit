using System.Collections.Generic;
using Newtonsoft.Json;

namespace CubeDigit.Game;

[JsonObject(MemberSerialization.OptIn)]
public class CubeJson
{
    [JsonProperty("settings")] public SettingsJson Settings { get; set; } = new();
    [JsonProperty("cubes")] public Dictionary<string, string> Cubes { get; set; }　= new();
    [JsonProperty("frames")] public List<Dictionary<string, FrameJson>> Frames { get; set; } = new();

    [JsonObject(MemberSerialization.OptIn)]
    public class SettingsJson
    {
        [JsonProperty("cubeSize")] public int CubeSize { get; set; }
        [JsonProperty("cubeSpacing")] public int CubeSpacing { get; set; }
        [JsonProperty("cubeNumber")] public Int3Json CubeNumber { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class FrameJson
    {
        [JsonProperty("time")] public float Time { get; set; }
        [JsonProperty("color")] public string Color { get; set; } = "#";
    }

    [JsonObject(MemberSerialization.OptIn)]
    public struct Float3Json
    {
        [JsonProperty("x")] public float X { get; set; }
        [JsonProperty("y")] public float Y { get; set; }
        [JsonProperty("z")] public float Z { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public struct Int3Json
    {
        [JsonProperty("x")] public int X { get; set; }
        [JsonProperty("y")] public int Y { get; set; }
        [JsonProperty("z")] public int Z { get; set; }
    }
}
