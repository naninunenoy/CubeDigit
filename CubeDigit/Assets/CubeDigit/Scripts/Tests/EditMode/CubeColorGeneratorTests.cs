using CubeDigit.Game;
using NUnit.Framework;
using UnityEngine;
using CubeDigit.UnityUtils;

namespace CubeDigit.Tests.EditMode
{
    public class CubeColorGeneratorTests
    {
        [Test]
        public void TryFromHex_正しい16進数カラーコードの場合_成功して対応するCubeColorを返す()
        {
            // Arrange
            string hex = "#FF0000"; // 赤色

            // Act
            bool success = CubeColorGenerator.TryFromHex(hex, out CubeColor color);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(1.0f, color.R);
            Assert.AreEqual(0.0f, color.G);
            Assert.AreEqual(0.0f, color.B);
            Assert.IsTrue(color.Visible);
        }

        [Test]
        public void TryFromHex_シャープのみの場合_Visibleがfalseのキューブ色を返す()
        {
            // Arrange
            string hex = "#";

            // Act
            bool success = CubeColorGenerator.TryFromHex(hex, out CubeColor color);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(0.0f, color.R);
            Assert.AreEqual(0.0f, color.G);
            Assert.AreEqual(0.0f, color.B);
            Assert.IsFalse(color.Visible);
        }

        [Test]
        public void TryFromHex_不正な形式の場合_失敗する()
        {
            // Arrange
            string[] invalidHexes = {
                "",
                "FF0000",      // #がない
                "#FF00",       // 短すぎる
                "#FF0000FF",   // 長すぎる
                "#FGFFFF",     // 無効な文字を含む
                null           // null
            };

            foreach (var invalidHex in invalidHexes)
            {
                // Act
                bool success = CubeColorGenerator.TryFromHex(invalidHex, out CubeColor color);

                // Assert
                Assert.IsFalse(success, $"Invalid hex code '{invalidHex}' should fail.");
            }
        }

        [Test]
        public void TryFromHex_一般的なカラー値を正しく変換できる()
        {
            // Arrange & Act & Assert
            // 赤
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#FF0000", out CubeColor red));
            Assert.AreEqual(1.0f, red.R);
            Assert.AreEqual(0.0f, red.G);
            Assert.AreEqual(0.0f, red.B);

            // 緑
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#00FF00", out CubeColor green));
            Assert.AreEqual(0.0f, green.R);
            Assert.AreEqual(1.0f, green.G);
            Assert.AreEqual(0.0f, green.B);

            // 青
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#0000FF", out CubeColor blue));
            Assert.AreEqual(0.0f, blue.R);
            Assert.AreEqual(0.0f, blue.G);
            Assert.AreEqual(1.0f, blue.B);

            // 黒
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#000000", out CubeColor black));
            Assert.AreEqual(0.0f, black.R);
            Assert.AreEqual(0.0f, black.G);
            Assert.AreEqual(0.0f, black.B);

            // 白
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#FFFFFF", out CubeColor white));
            Assert.AreEqual(1.0f, white.R);
            Assert.AreEqual(1.0f, white.G);
            Assert.AreEqual(1.0f, white.B);

            // 中間色
            Assert.IsTrue(CubeColorGenerator.TryFromHex("#808080", out CubeColor gray));
            Assert.AreEqual(0.5019608f, gray.R, 0.0001f); // 128/255 ≈ 0.5019608
            Assert.AreEqual(0.5019608f, gray.G, 0.0001f);
            Assert.AreEqual(0.5019608f, gray.B, 0.0001f);
        }

        [Test]
        public void TryFromHex_変換されたCubeColorが正しくUnityのColorに変換される()
        {
            // Arrange
            string hex = "#AABBCC";

            // Act
            CubeColorGenerator.TryFromHex(hex, out CubeColor cubeColor);
            Color unityColor = cubeColor.ToColor();

            // Assert
            Assert.AreEqual(0xAA / 255.0f, unityColor.r, 0.0001f);
            Assert.AreEqual(0xBB / 255.0f, unityColor.g, 0.0001f);
            Assert.AreEqual(0xCC / 255.0f, unityColor.b, 0.0001f);
            Assert.AreEqual(1.0f, unityColor.a);
        }
    }
}
