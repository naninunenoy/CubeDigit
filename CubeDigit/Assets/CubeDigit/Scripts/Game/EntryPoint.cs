using UnityEngine;

namespace CubeDigit.Game
{
    /// <summary>
    /// アプリケーションのエントリーポイントとなるクラス
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
        private Vector3Int cubeCount = new Vector3Int(1, 1, 1);

        /// <summary>
        /// Cubeに適用する色
        /// </summary>
        [SerializeField]
        private Color cubeColor = Color.white;

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
            Vector3 startPosition = new Vector3(
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
                        // Cubeの位置を計算
                        Vector3 position = new Vector3(
                            startPosition.x + x * (CubeSize + CubeSpacing),
                            startPosition.y + y * (CubeSize + CubeSpacing),
                            startPosition.z + z * (CubeSize + CubeSpacing)
                        );

                        // Cubeを生成
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.name = $"Cube_{x}_{y}_{z}";
                        cube.transform.position = position;
                        cube.transform.localScale = Vector3.one * CubeSize;

                        // Cubeの色を設定
                        Renderer renderer = cube.GetComponent<Renderer>();
                        renderer.material.color = cubeColor;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 現時点では特に処理なし
        }
    }
}
