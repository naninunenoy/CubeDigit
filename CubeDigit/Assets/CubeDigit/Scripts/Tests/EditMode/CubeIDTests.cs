using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CubeDigit.Game;

namespace CubeDigit.Tests.EditMode
{
    public class CubeIDTests
    {
        [Test]
        public void Constructor_WithXYZParameters_SetsPositionCorrectly()
        {
            // Arrange
            int x = 1;
            int y = 2;
            int z = 3;

            // Act
            var cubeId = new CubeID(x, y, z);

            // Assert
            Assert.AreEqual(x, cubeId.Position.x);
            Assert.AreEqual(y, cubeId.Position.y);
            Assert.AreEqual(z, cubeId.Position.z);
        }

        [Test]
        public void Constructor_WithVector3Int_SetsPositionCorrectly()
        {
            // Arrange
            Vector3Int position = new Vector3Int(4, 5, 6);

            // Act
            var cubeId = new CubeID(position);

            // Assert
            Assert.AreEqual(position, cubeId.Position);
        }

        [Test]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            int x = 7;
            int y = 8;
            int z = 9;
            var cubeId = new CubeID(x, y, z);

            // Act
            string result = cubeId.ToString();

            // Assert
            Assert.AreEqual($"{x}|{y}|{z}", result);
        }

        [Test]
        public void Equals_WithSamePosition_ReturnsTrue()
        {
            // Arrange
            var cubeId1 = new CubeID(1, 2, 3);
            var cubeId2 = new CubeID(1, 2, 3);

            // Act & Assert
            Assert.IsTrue(cubeId1.Equals(cubeId2));
            Assert.IsTrue(cubeId1 == cubeId2);
            Assert.IsFalse(cubeId1 != cubeId2);
        }

        [Test]
        public void Equals_WithDifferentPosition_ReturnsFalse()
        {
            // Arrange
            var cubeId1 = new CubeID(1, 2, 3);
            var cubeId2 = new CubeID(3, 2, 1);

            // Act & Assert
            Assert.IsFalse(cubeId1.Equals(cubeId2));
            Assert.IsFalse(cubeId1 == cubeId2);
            Assert.IsTrue(cubeId1 != cubeId2);
        }

        [Test]
        public void Equals_WithObject_ReturnsExpectedResult()
        {
            // Arrange
            var cubeId = new CubeID(1, 2, 3);
            object sameObject = new CubeID(1, 2, 3);
            object differentObject = new CubeID(4, 5, 6);
            object nonCubeIdObject = "not a CubeID";

            // Act & Assert
            Assert.IsTrue(cubeId.Equals(sameObject));
            Assert.IsFalse(cubeId.Equals(differentObject));
            Assert.IsFalse(cubeId.Equals(nonCubeIdObject));
        }

        [Test]
        public void GetHashCode_ReturnsSameValueForEqualObjects()
        {
            // Arrange
            var cubeId1 = new CubeID(1, 2, 3);
            var cubeId2 = new CubeID(1, 2, 3);

            // Act
            int hashCode1 = cubeId1.GetHashCode();
            int hashCode2 = cubeId2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [Test]
        public void GetHashCode_ReturnsDifferentValueForUnequalObjects()
        {
            // Arrange
            var cubeId1 = new CubeID(1, 2, 3);
            var cubeId2 = new CubeID(3, 2, 1);

            // Act
            int hashCode1 = cubeId1.GetHashCode();
            int hashCode2 = cubeId2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2);
        }
    }
}
