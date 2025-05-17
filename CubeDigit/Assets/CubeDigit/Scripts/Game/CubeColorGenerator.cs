namespace CubeDigit.UnityUtils
{
    /// <summary>
    /// 色コードからCubeColorを生成するユーティリティクラス
    /// </summary>
    public static class CubeColorGenerator
    {
        /// <summary>
        /// 16進数カラーコードからCubeColorを生成します
        /// </summary>
        /// <param name="hex">16進数カラーコード (#RRGGBB 形式)</param>
        /// <param name="color">生成されたCubeColor</param>
        /// <returns>変換が成功した場合はtrue、失敗した場合はfalse</returns>
        public static bool TryFromHex(string hex, out CubeColor color)
        {
            // 入力文字列がnullまたは空の場合は失敗
            if (string.IsNullOrEmpty(hex))
            {
                color = default;
                return false;
            }

            // "#"のみの場合は非表示のCubeColorを返す
            if (hex == "#")
            {
                color = new CubeColor(0F, 0F, 0F, false);
                return true;
            }

            // "#RRGGBB"形式かチェック
            if (hex.Length != 7 || !hex.StartsWith("#"))
            {
                color = default;
                return false;
            }

            try
            {
                // 16進数の色コードをRGB成分に変換（各成分は0-255の範囲）
                int r = int.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

                // 0-255の範囲を0-1の範囲に正規化
                float normalizedR = r / 255F;
                float normalizedG = g / 255F;
                float normalizedB = b / 255F;

                // 表示状態のCubeColorを生成
                color = new CubeColor(normalizedR, normalizedG, normalizedB, true);
                return true;
            }
            catch (System.FormatException)
            {
                // 16進数のパースに失敗した場合
                color = default;
                return false;
            }
        }
    }
}
