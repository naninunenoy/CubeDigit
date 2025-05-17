using UnityEngine;

namespace CubeDigit.UnityUtils
{
    /// <summary>
    /// キューブの色情報を表すクラス
    /// RGB値に加えて表示/非表示情報を持つ
    /// </summary>
    public readonly struct CubeColor
    {
        // RGB値の内部フィールド

        // 表示するかどうかの内部フィールド

        /// <summary>
        /// 赤成分 (0-1)
        /// </summary>
        public float R { get; }

        /// <summary>
        /// 緑成分 (0-1)
        /// </summary>
        public float G { get; }

        /// <summary>
        /// 青成分 (0-1)
        /// </summary>
        public float B { get; }

        /// <summary>
        /// 表示するかどうか
        /// </summary>
        public bool Visible { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="r">赤成分 (0-1)</param>
        /// <param name="g">緑成分 (0-1)</param>
        /// <param name="b">青成分 (0-1)</param>
        /// <param name="visible">表示するかどうか</param>
        public CubeColor(float r, float g, float b, bool visible)
        {
            R = r;
            G = g;
            B = b;
            Visible = visible;
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
                R = 0F;
                G = 0F;
                B = 0F;
                Visible = false;
            }
            else
            {
                // それ以外はRGB値のみを取得し、表示状態にする
                R = color.r;
                G = color.g;
                B = color.b;
                Visible = true;
            }
        }

        /// <summary>
        /// UnityのColorに変換
        /// </summary>
        public Color ToColor()
        {
            // 非表示の場合はColor.clearを返す
            if (!Visible)
            {
                return Color.clear;
            }

            // 表示する場合はRGB値を使用し、アルファは1に設定
            return new Color(R, G, B, 1F);
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
            return $"CubeColor(R:{R}, G:{G}, B:{B}, Visible:{Visible})";
        }
    }
}
