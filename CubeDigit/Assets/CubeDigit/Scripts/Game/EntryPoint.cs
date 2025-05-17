using UnityEngine;

namespace CubeDigit.Game
{
    /// <summary>
    /// アプリケーションのエントリーポイントとなるクラス
    /// Cubeの生成と配置を管理する
    /// </summary>
    public class EntryPoint : MonoBehaviour
    {
        /// <summary>
        /// Cubeのサイズ（幅・高さ・奥行き）
        /// </summary>
        private const float CubeSize = 1.0f;

        /// <summary>
        /// Cube間の間隔
        /// </summary>
        private const float CubeSpacing = 0.1f;

        /// <summary>
        /// 生成するCubeの数（X,Y,Z）
        /// </summary>
        [SerializeField]
        private Vector3Int cubeCount = new();

        /// <summary>
        /// Cubeに適用する色
        /// </summary>
        [SerializeField]
        private Color cubeColor = new();

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            CreateCubes();
        }

        /// <summary>
        /// 指定された数のCubeを生成して配置する
        /// </summary>
        private void CreateCubes()
        {
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
        private void CreateCubeAt(int x, int y, int z, Vector3 startPosition)
        {
            // Cubeの位置を計算
            var position = new Vector3(
                startPosition.x + x * (CubeSize + CubeSpacing),
                startPosition.y + y * (CubeSize + CubeSpacing),
                startPosition.z + z * (CubeSize + CubeSpacing)
            );

            // Cubeを生成
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"Cube_{x}_{y}_{z}";
            cube.transform.position = position;
            cube.transform.localScale = Vector3.one * CubeSize;

            // Cubeの色を設定（MonoBehaviourでのローカル変数名に注意）
            Renderer objectRenderer = cube.GetComponent<Renderer>();
            objectRenderer.material.color = cubeColor;
        }
    }
}
