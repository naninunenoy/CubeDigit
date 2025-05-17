using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CubeDigit.UnityUtils;

namespace CubeDigit.Tests.EditMode
{
    public class CubeColorTests
    {
        [Test]
        public void Constructor_WithRGBAndVisibleTrue_SetsPropertiesCorrectly()
        {
            // Arrange
            float r = 0.5f;
            float g = 0.6f;
            float b = 0.7f;
            bool visible = true;

            // Act
            var cubeColor = new CubeColor(r, g, b, visible);

            // Assert
            Assert.AreEqual(r, cubeColor.r);
            Assert.AreEqual(g, cubeColor.g);
            Assert.AreEqual(b, cubeColor.b);
            Assert.AreEqual(visible, cubeColor.visible);
        }

        [Test]
        public void Constructor_WithRGBAndVisibleFalse_SetsPropertiesCorrectly()
        {
            // Arrange
            float r = 0.5f;
            float g = 0.6f;
            float b = 0.7f;
            bool visible = false;

            // Act
            var cubeColor = new CubeColor(r, g, b, visible);

            // Assert
            Assert.AreEqual(r, cubeColor.r);
            Assert.AreEqual(g, cubeColor.g);
            Assert.AreEqual(b, cubeColor.b);
            Assert.AreEqual(visible, cubeColor.visible);
        }

        [Test]
        public void Constructor_WithNormalColor_SetsPropertiesCorrectlyAndVisible()
        {
            // Arrange
            Color color = new Color(0.1f, 0.2f, 0.3f, 0.4f);

            // Act
            var cubeColor = new CubeColor(color);

            // Assert
            Assert.AreEqual(color.r, cubeColor.r);
            Assert.AreEqual(color.g, cubeColor.g);
            Assert.AreEqual(color.b, cubeColor.b);
            Assert.IsTrue(cubeColor.visible);
        }

        [Test]
        public void Constructor_WithClearColor_SetsVisibleToFalse()
        {
            // Arrange
            Color color = Color.clear;

            // Act
            var cubeColor = new CubeColor(color);

            // Assert
            Assert.AreEqual(0f, cubeColor.r);
            Assert.AreEqual(0f, cubeColor.g);
            Assert.AreEqual(0f, cubeColor.b);
            Assert.IsFalse(cubeColor.visible);
        }

        [Test]
        public void ToColor_WhenVisible_ReturnsCorrectColorWithAlphaOne()
        {
            // Arrange
            float r = 0.1f;
            float g = 0.2f;
            float b = 0.3f;
            bool visible = true;
            var cubeColor = new CubeColor(r, g, b, visible);

            // Act
            Color result = cubeColor.ToColor();

            // Assert
            Assert.AreEqual(r, result.r);
            Assert.AreEqual(g, result.g);
            Assert.AreEqual(b, result.b);
            Assert.AreEqual(1f, result.a);
        }

        [Test]
        public void ToColor_WhenNotVisible_ReturnsClearColor()
        {
            // Arrange
            float r = 0.1f;
            float g = 0.2f;
            float b = 0.3f;
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
            var cubeColor = new CubeColor(0.1f, 0.2f, 0.3f, true);

            // Act
            Color color = cubeColor;

            // Assert
            Assert.AreEqual(cubeColor.r, color.r);
            Assert.AreEqual(cubeColor.g, color.g);
            Assert.AreEqual(cubeColor.b, color.b);
            Assert.AreEqual(1f, color.a);
        }

        [Test]
        public void ImplicitOperator_FromColorToCubeColor_WorksCorrectly()
        {
            // Arrange
            Color color = new Color(0.4f, 0.5f, 0.6f, 0.7f);

            // Act
            CubeColor cubeColor = color;

            // Assert
            Assert.AreEqual(color.r, cubeColor.r);
            Assert.AreEqual(color.g, cubeColor.g);
            Assert.AreEqual(color.b, cubeColor.b);
            Assert.IsTrue(cubeColor.visible);
        }
    }
}
