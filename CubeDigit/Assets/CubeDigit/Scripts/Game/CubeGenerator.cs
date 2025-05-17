using System.Collections.Generic;
using UnityEngine;

namespace CubeDigit.Game
{
    /// <summary>
    /// キューブ群を生成するクラス
    /// </summary>
    public class CubeGenerator : ICubeRenderer
    {
        /// <summary>
        /// CubeIDとMonoCubeの対応を管理するDictionary
        /// </summary>
        private readonly Dictionary<CubeID, MonoCube> _cubeDictionary = new();

        /// <summary>
        /// キューブ群を生成する
        /// </summary>
        /// <param name="xCount">X方向のキューブ数</param>
        /// <param name="yCount">Y方向のキューブ数</param>
        /// <param name="zCount">Z方向のキューブ数</param>
        /// <param name="cubeSize">キューブのサイズ</param>
        /// <param name="cubeSpacing">キューブ間の間隔</param>
        /// <param name="parent">生成したキューブの親Transform</param>
        /// <returns>生成したキューブ群を制御するためのICubeRenderer</returns>
        public ICubeRenderer Generate(int xCount, int yCount, int zCount, float cubeSize, float cubeSpacing, Transform parent)
        {
            // 既存のcubeDictionaryをクリア
            _cubeDictionary.Clear();

            // 配置の総サイズを計算（Cubeのサイズ + 間隔）
            float totalWidth = xCount * cubeSize + (xCount - 1) * cubeSpacing;
            float totalHeight = yCount * cubeSize + (yCount - 1) * cubeSpacing;
            float totalDepth = zCount * cubeSize + (zCount - 1) * cubeSpacing;

            // 配置の開始位置を計算（中心が原点になるように）
            var startPosition = new Vector3(
                -totalWidth / 2 + cubeSize / 2,
                -totalHeight / 2 + cubeSize / 2,
                -totalDepth / 2 + cubeSize / 2
            );

            // Cubeを生成して配置
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int z = 0; z < zCount; z++)
                    {
                        CreateCubeAt(x, y, z, startPosition, cubeSize, cubeSpacing, parent);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// 指定された座標にCubeを生成する
        /// </summary>
        /// <param name="x">X座標インデックス</param>
        /// <param name="y">Y座標インデックス</param>
        /// <param name="z">Z座標インデックス</param>
        /// <param name="startPosition">配置開始位置</param>
        /// <param name="cubeSize">キューブのサイズ</param>
        /// <param name="cubeSpacing">キューブ間の間隔</param>
        /// <param name="parent">親Transform</param>
        private void CreateCubeAt(int x, int y, int z, Vector3 startPosition, float cubeSize, float cubeSpacing, Transform parent)
        {
            // Cubeの位置を計算
            var position = new Vector3(
                startPosition.x + x * (cubeSize + cubeSpacing),
                startPosition.y + y * (cubeSize + cubeSpacing),
                startPosition.z + z * (cubeSize + cubeSpacing)
            );

            // CubeIDを作成
            var cubeID = new CubeID(x, y, z);

            // Cubeを生成
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = position;
            cube.transform.localScale = Vector3.one * cubeSize;
            cube.transform.SetParent(parent, false);

            // MonoCubeコンポーネントを追加して初期化
            MonoCube monoCube = cube.AddComponent<MonoCube>();
            monoCube.Initialize(cubeID);

            // Dictionaryに追加
            _cubeDictionary[cubeID] = monoCube;
        }

        public void SetColor(CubeID cubeID, CubeColor color)
        {
            // キューブが存在するか確認
            if (!_cubeDictionary.TryGetValue(cubeID, out MonoCube monoCube))
            {
                Debug.LogWarning($"CubeID: {cubeID} のキューブは存在しません。");
                return;
            }

            // キューブの色を変更
            monoCube.ApplyCubeColor(color);
        }
    }
}
