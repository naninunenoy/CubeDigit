using System.Collections.Generic;
using UnityEngine;
using CubeDigit.UnityUtils;
using UnityEngine.Assertions;

namespace CubeDigit.Game
{
    /// <summary>
    /// アプリケーションのエントリーポイントとなるクラス
    /// Cubeの生成と配置を管理する
    /// </summary>
    public class EntryPoint : MonoBehaviour, ICubeRenderer
    {
        /// <summary>
        /// Cubeのサイズ（幅・高さ・奥行き）
        /// </summary>
        const float CubeSize = 1.0f;

        /// <summary>
        /// Cube間の間隔
        /// </summary>
        const float CubeSpacing = 0.1f;

        [SerializeField] GameObject cubeParent = null;

        /// <summary>
        /// 生成するCubeの数（X,Y,Z）
        /// </summary>
        [SerializeField] Vector3Int cubeCount = default;

        /// <summary>
        /// Cubeに適用する色
        /// </summary>
        [SerializeField] Color cubeColor = default;

        /// <summary>
        /// CubeIDとMonoCubeの対応を管理するDictionary
        /// </summary>
        readonly Dictionary<CubeID, MonoCube> _cubeDictionary = new();

        void Awake()
        {
            Assert.IsNotNull(cubeParent, $"{nameof(cubeParent)} is null.");
        }

        void Start()
        {
            CreateCubes();
        }

        /// <summary>
        /// 指定された数のCubeを生成して配置する
        /// </summary>
        void CreateCubes()
        {
            // 既存のcubeDictionaryをクリア
            _cubeDictionary.Clear();

            // 配置の総サイズを計算（Cubeのサイズ + 間隔）
            float totalWidth = cubeCount.x * CubeSize + (cubeCount.x - 1) * CubeSpacing;
            float totalHeight = cubeCount.y * CubeSize + (cubeCount.y - 1) * CubeSpacing;
            float totalDepth = cubeCount.z * CubeSize + (cubeCount.z - 1) * CubeSpacing;

            // 配置の開始位置を計算（中心が原点になるように）
            var startPosition = new Vector3(
                -totalWidth / 2 + CubeSize / 2,
                -totalHeight / 2 + CubeSize / 2,
                -totalDepth / 2 + CubeSize / 2
            );

            // Cubeを生成して配置
            for (int x = 0; x < cubeCount.x; x++)
            {
                for (int y = 0; y < cubeCount.y; y++)
                {
                    for (int z = 0; z < cubeCount.z; z++)
                    {
                        CreateCubeAt(x, y, z, startPosition);
                    }
                }
            }
        }

        /// <summary>
        /// 指定された座標にCubeを生成する
        /// </summary>
        /// <param name="x">X座標インデックス</param>
        /// <param name="y">Y座標インデックス</param>
        /// <param name="z">Z座標インデックス</param>
        /// <param name="startPosition">配置開始位置</param>
        void CreateCubeAt(int x, int y, int z, Vector3 startPosition)
        {
            // Cubeの位置を計算
            var position = new Vector3(
                startPosition.x + x * (CubeSize + CubeSpacing),
                startPosition.y + y * (CubeSize + CubeSpacing),
                startPosition.z + z * (CubeSize + CubeSpacing)
            );

            // CubeIDを作成
            var cubeID = new CubeID(x, y, z);

            // CubeColorを作成
            var cubeColorStruct = new CubeColor(cubeColor);

            // Cubeを生成
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = position;
            cube.transform.localScale = Vector3.one * CubeSize;
            cube.transform.SetParent(cubeParent.transform, false);

            // MonoCubeコンポーネントを追加して初期化
            MonoCube monoCube = cube.AddComponent<MonoCube>();
            monoCube.Initialize(cubeID, cubeColorStruct);

            // Dictionaryに追加
            _cubeDictionary[cubeID] = monoCube;
        }

        /// <summary>
        /// 指定されたCubeIDのキューブの色を設定する
        /// </summary>
        /// <param name="cubeID">色を変更するキューブのID</param>
        /// <param name="color">設定する色情報</param>
        void ICubeRenderer.SetColor(CubeID cubeID, CubeColor color)
        {
            // キューブが存在するか確認
            if (!_cubeDictionary.TryGetValue(cubeID, out MonoCube monoCube))
            {
                Debug.LogWarning($"CubeID: {cubeID} のキューブは存在しません。");
                return;
            }
            monoCube.ApplyCubeColor(color);
        }
    }
}
