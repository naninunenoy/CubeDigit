using System.Collections.Generic;
using NUnit.Framework;
using CubeDigit.Game;
using Newtonsoft.Json;

namespace CubeDigit.Tests.EditMode
{
    public class CubeJsonTests
    {
        [Test]
        public void SerializeDeserialize_ShouldMaintainAllProperties()
        {
            // Arrange
            var cubeJson = new CubeJson
            {
                Settings = new CubeJson.SettingsJson
                {
                    CubeSize = 10,
                    CubeSpacing = 2,
                    CubeNumber = new CubeJson.Int3Json { X = 3, Y = 4, Z = 5 }
                },
                Cubes = new Dictionary<string, string>
                {
                    { "0|0|0", "cube1" },
                    { "1|0|0", "cube2" }
                },
                Frames = new Dictionary<string, List<CubeJson.FrameJson>>
                {
                    { "0|0|0", new List<CubeJson.FrameJson> { new CubeJson.FrameJson { Time = 0.5f, Color = "#FF0000" } } }
                }
            };

            // Act
            string json = JsonConvert.SerializeObject(cubeJson);
            var deserializedCubeJson = JsonConvert.DeserializeObject<CubeJson>(json);

            // Assert
            Assert.NotNull(deserializedCubeJson);

            // Check Settings
            Assert.AreEqual(10, deserializedCubeJson.Settings.CubeSize);
            Assert.AreEqual(2, deserializedCubeJson.Settings.CubeSpacing);
            Assert.AreEqual(3, deserializedCubeJson.Settings.CubeNumber.X);
            Assert.AreEqual(4, deserializedCubeJson.Settings.CubeNumber.Y);
            Assert.AreEqual(5, deserializedCubeJson.Settings.CubeNumber.Z);

            // Check Cubes
            Assert.AreEqual(2, deserializedCubeJson.Cubes.Count);
            Assert.AreEqual("cube1", deserializedCubeJson.Cubes["0|0|0"]);
            Assert.AreEqual("cube2", deserializedCubeJson.Cubes["1|0|0"]);

            // Check Frames
            Assert.AreEqual(1, deserializedCubeJson.Frames.Count);
            Assert.AreEqual(0.5f, deserializedCubeJson.Frames["0|0|0"][0].Time);
            Assert.AreEqual("#FF0000", deserializedCubeJson.Frames["0|0|0"][0].Color);
        }

        [Test]
        public void DefaultValues_ShouldBeInitializedCorrectly()
        {
            // Arrange & Act
            var cubeJson = new CubeJson();

            // Assert
            Assert.NotNull(cubeJson.Settings);
            Assert.NotNull(cubeJson.Cubes);
            Assert.NotNull(cubeJson.Frames);

            // Default color starts with #
            var frameJson = new CubeJson.FrameJson();
            Assert.AreEqual("#", frameJson.Color);
        }

        [Test]
        public void Float3Json_SerializeDeserialize_ShouldMaintainValues()
        {
            // Arrange
            var float3 = new CubeJson.Float3Json
            {
                X = 1.5f,
                Y = 2.5f,
                Z = 3.5f
            };

            // Act
            string json = JsonConvert.SerializeObject(float3);
            var deserializedFloat3 = JsonConvert.DeserializeObject<CubeJson.Float3Json>(json);

            // Assert
            Assert.AreEqual(1.5f, deserializedFloat3.X);
            Assert.AreEqual(2.5f, deserializedFloat3.Y);
            Assert.AreEqual(3.5f, deserializedFloat3.Z);
        }

        [Test]
        public void Int3Json_SerializeDeserialize_ShouldMaintainValues()
        {
            // Arrange
            var int3 = new CubeJson.Int3Json
            {
                X = 10,
                Y = 20,
                Z = 30
            };

            // Act
            string json = JsonConvert.SerializeObject(int3);
            var deserializedInt3 = JsonConvert.DeserializeObject<CubeJson.Int3Json>(json);

            // Assert
            Assert.AreEqual(10, deserializedInt3.X);
            Assert.AreEqual(20, deserializedInt3.Y);
            Assert.AreEqual(30, deserializedInt3.Z);
        }
    }
}
