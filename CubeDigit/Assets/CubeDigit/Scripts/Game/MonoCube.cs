using UnityEngine;
using CubeDigit.UnityUtils;

namespace CubeDigit.Game
{
    /// <summary>
    /// キューブのGameObjectを制御するコンポーネント
    /// </summary>
    public class MonoCube : MonoBehaviour
    {
        /// <summary>
        /// キューブのID（読み取り専用）
        /// </summary>
        public CubeID CubeID { get; private set; }

        /// <summary>
        /// キューブを初期化します
        /// </summary>
        /// <param name="cubeID">キューブのID</param>
        /// <param name="cubeColor">キューブの色情報</param>
        public void Initialize(CubeID cubeID, CubeColor cubeColor)
        {
            // CubeIDを設定
            CubeID = cubeID;

            // オブジェクト名をCubeIDの文字列表現に設定
            gameObject.name = $"Cube#{cubeID.ToString()}";

            // キューブの色を適用
            ApplyCubeColor(cubeColor);
        }

        void ApplyCubeColor(CubeColor cubeColor)
        {
            if (!cubeColor.Visible)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            if (TryGetComponent(out Renderer thisRenderer))
            {
                thisRenderer.material.color = cubeColor.ToColor();
            }
        }
    }
}
