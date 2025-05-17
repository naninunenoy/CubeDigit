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
            gameObject.name = cubeID.ToString();

            // CubeColor.Visibleに基づいてオブジェクトの表示/非表示を設定
            gameObject.SetActive(cubeColor.Visible);

            // ここに必要に応じて色の設定処理を追加することも可能
            // 例：GetComponent<Renderer>()?.material.color = cubeColor.ToColor();
        }
    }
}
