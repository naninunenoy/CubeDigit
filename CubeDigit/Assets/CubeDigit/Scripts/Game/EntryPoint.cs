using UnityEngine;
using UnityEngine.Assertions;

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
        [SerializeField] float cubeSize = 1.0f;

        /// <summary>
        /// Cube間の間隔
        /// </summary>
        [SerializeField] float cubeSpacing = 0.1f;

        /// <summary>
        /// 生成するCubeの数（X,Y,Z）
        /// </summary>
        [SerializeField] Vector3Int cubeCount = default;

        /// <summary>
        /// Cubeの色
        /// </summary>
        [SerializeField] Color color = Color.white;

        [SerializeField] GameObject cubeParent = null;

        /// <summary>
        /// キューブ群のレンダラー
        /// </summary>
        private ICubeRenderer _cubeRenderer;

        void Awake()
        {
            Assert.IsNotNull(cubeParent, $"{nameof(cubeParent)} is null.");
        }

        void Start()
        {
            var cubeGenerator = new CubeGenerator();
            _cubeRenderer = cubeGenerator.Generate(
                cubeCount.x,
                cubeCount.y,
                cubeCount.z,
                cubeSize,
                cubeSpacing,
                cubeParent.transform
            );
            // 全cubeに色を指定
            for (int x = 0; x < cubeCount.x; x++)
            {
                for (int y = 0; y < cubeCount.y; y++)
                {
                    for (int z = 0; z < cubeCount.z; z++)
                    {
                        var cubeID = new CubeID(x, y, z);
                        _cubeRenderer.SetColor(cubeID, color);
                    }
                }
            }
        }
    }
}
