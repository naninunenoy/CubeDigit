using NUnit.Framework;
using UnityEngine;
using CubeDigit.UnityUtils;

namespace CubeDigit.Tests.EditMode
{
    public class CubeColorTests
    {
        [Test]
        public void Constructor_WithRGBAndVisibleTrue_SetsPropertiesCorrectly()
        {
            // Arrange
            float r = 0.5F;
            float g = 0.6F;
            float b = 0.7F;
            bool visible = true;

            // Act
            var cubeColor = new CubeColor(r, g, b, visible);

            // Assert
            Assert.AreEqual(r, cubeColor.R);
            Assert.AreEqual(g, cubeColor.G);
            Assert.AreEqual(b, cubeColor.B);
            Assert.AreEqual(visible, cubeColor.Visible);
        }

        [Test]
        public void Constructor_WithRGBAndVisibleFalse_SetsPropertiesCorrectly()
        {
            // Arrange
            float r = 0.5F;
            float g = 0.6F;
            float b = 0.7F;
            bool visible = false;

            // Act
            var cubeColor = new CubeColor(r, g, b, visible);

            // Assert
            Assert.AreEqual(r, cubeColor.R);
            Assert.AreEqual(g, cubeColor.G);
            Assert.AreEqual(b, cubeColor.B);
            Assert.AreEqual(visible, cubeColor.Visible);
        }

        [Test]
        public void Constructor_WithNormalColor_SetsPropertiesCorrectlyAndVisible()
        {
            // Arrange
            Color color = new Color(0.1F, 0.2F, 0.3F, 0.4F);

            // Act
            var cubeColor = new CubeColor(color);

            // Assert
            Assert.AreEqual(color.r, cubeColor.R);
            Assert.AreEqual(color.g, cubeColor.G);
            Assert.AreEqual(color.b, cubeColor.B);
            Assert.IsTrue(cubeColor.Visible);
        }

        [Test]
        public void Constructor_WithClearColor_SetsVisibleToFalse()
        {
            // Arrange
            Color color = Color.clear;

            // Act
            var cubeColor = new CubeColor(color);

            // Assert
            Assert.AreEqual(0F, cubeColor.R);
            Assert.AreEqual(0F, cubeColor.G);
            Assert.AreEqual(0F, cubeColor.B);
            Assert.IsFalse(cubeColor.Visible);
        }

        [Test]
        public void ToColor_WhenVisible_ReturnsCorrectColorWithAlphaOne()
        {
            // Arrange
            float r = 0.1F;
            float g = 0.2F;
            float b = 0.3F;
            bool visible = true;
            var cubeColor = new CubeColor(r, g, b, visible);

            // Act
            Color result = cubeColor.ToColor();

            // Assert
            Assert.AreEqual(r, result.r);
            Assert.AreEqual(g, result.g);
            Assert.AreEqual(b, result.b);
            Assert.AreEqual(1F, result.a);
        }

        [Test]
        public void ToColor_WhenNotVisible_ReturnsClearColor()
        {
            // Arrange
            float r = 0.1F;
            float g = 0.2F;
            float b = 0.3F;
            bool visible = false;
            var cubeColor = new CubeColor(r, g, b, visible);

            // Act
            Color result = cubeColor.ToColor();

            // Assert
            Assert.AreEqual(Color.clear, result);
        }

        [Test]
        public void ImplicitOperator_FromCubeColorToColor_WorksCorrectly()
        {
            // Arrange
            var cubeColor = new CubeColor(0.1F, 0.2F, 0.3F, true);

            // Act
            Color color = cubeColor;

            // Assert
            Assert.AreEqual(cubeColor.R, color.r);
            Assert.AreEqual(cubeColor.G, color.g);
            Assert.AreEqual(cubeColor.B, color.b);
            Assert.AreEqual(1F, color.a);
        }

        [Test]
        public void ImplicitOperator_FromColorToCubeColor_WorksCorrectly()
        {
            // Arrange
            Color color = new Color(0.4F, 0.5F, 0.6F, 0.7F);

            // Act
            CubeColor cubeColor = color;

            // Assert
            Assert.AreEqual(color.r, cubeColor.R);
            Assert.AreEqual(color.g, cubeColor.G);
            Assert.AreEqual(color.b, cubeColor.B);
            Assert.IsTrue(cubeColor.Visible);
        }
    }
}
