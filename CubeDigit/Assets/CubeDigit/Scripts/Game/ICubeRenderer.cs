using CubeDigit.UnityUtils;

namespace CubeDigit.Game
{
    /// <summary>
    /// キューブの色を制御するためのインターフェース
    /// </summary>
    public interface ICubeRenderer
    {
        /// <summary>
        /// 指定されたCubeIDのキューブの色を設定する
        /// </summary>
        /// <param name="cubeID">色を変更するキューブのID</param>
        /// <param name="color">設定する色情報</param>
        void SetColor(CubeID cubeID, CubeColor color);
    }
}
