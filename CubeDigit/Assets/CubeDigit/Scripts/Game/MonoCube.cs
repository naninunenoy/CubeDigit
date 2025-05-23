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
        public void Initialize(CubeID cubeID)
        {
            // CubeIDを設定
            CubeID = cubeID;

            // オブジェクト名をCubeIDの文字列表現に設定
            gameObject.name = $"Cube#{cubeID.ToString()}";
        }

        public void ApplyCubeColor(CubeColor cubeColor)
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
