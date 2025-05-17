using UnityEngine;

namespace CubeDigit.UnityUtils
{
    /// <summary>
    /// キューブの色情報を表すクラス
    /// RGB値に加えて表示/非表示情報を持つ
    /// </summary>
    public readonly struct CubeColor
    {
        // RGB値
        public readonly float r;
        public readonly float g;
        public readonly float b;

        // 表示するかどうか
        public readonly bool visible;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="r">赤成分 (0-1)</param>
        /// <param name="g">緑成分 (0-1)</param>
        /// <param name="b">青成分 (0-1)</param>
        /// <param name="visible">表示するかどうか</param>
        public CubeColor(float r, float g, float b, bool visible)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.visible = visible;
        }

        /// <summary>
        /// UnityのColorからCubeColorに変換するコンストラクタ
        /// Color.clearの場合は非表示扱いになる
        /// </summary>
        /// <param name="color">Unityのカラー</param>
        public CubeColor(Color color)
        {
            // Color.clear (RGBAすべて0) の場合は非表示
            if (color == Color.clear)
            {
                r = 0f;
                g = 0f;
                b = 0f;
                visible = false;
            }
            else
            {
                // それ以外はRGB値のみを取得し、表示状態にする
                r = color.r;
                g = color.g;
                b = color.b;
                visible = true;
            }
        }

        /// <summary>
        /// UnityのColorに変換
        /// </summary>
        public Color ToColor()
        {
            // 非表示の場合はColor.clearを返す
            if (!visible)
            {
                return Color.clear;
            }

            // 表示する場合はRGB値を使用し、アルファは1に設定
            return new Color(r, g, b, 1f);
        }

        /// <summary>
        /// UnityのColorへの暗黙的変換演算子
        /// </summary>
        public static implicit operator Color(CubeColor cubeColor)
        {
            return cubeColor.ToColor();
        }

        /// <summary>
        /// CubeColorへの暗黙的変換演算子
        /// </summary>
        public static implicit operator CubeColor(Color color)
        {
            return new CubeColor(color);
        }

        /// <summary>
        /// 文字列表現
        /// </summary>
        public override string ToString()
        {
            return $"CubeColor(R:{r}, G:{g}, B:{b}, Visible:{visible})";
        }
    }
}
