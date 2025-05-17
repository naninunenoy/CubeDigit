using System;
using UnityEngine;

namespace CubeDigit.Game
{
    /// <summary>
    /// キューブの位置に基づいて割り振られるID
    /// インデックスベースの位置情報を保持する
    /// </summary>
    public readonly struct CubeID : IEquatable<CubeID>
    {
        /// <summary>
        /// キューブの位置情報（インデックス）
        /// </summary>
        public Vector3Int Position { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X軸のインデックス</param>
        /// <param name="y">Y軸のインデックス</param>
        /// <param name="z">Z軸のインデックス</param>
        public CubeID(int x, int y, int z)
        {
            Position = new Vector3Int(x, y, z);
        }

        /// <summary>
        /// Vector3Intを使用するコンストラクタ
        /// </summary>
        /// <param name="position">キューブのインデックス位置</param>
        public CubeID(Vector3Int position)
        {
            Position = position;
        }

        /// <summary>
        /// 文字列形式（"x|y|z"）でIDを取得
        /// </summary>
        /// <returns>フォーマットされた文字列ID</returns>
        public override string ToString()
        {
            return $"{Position.x}|{Position.y}|{Position.z}";
        }

        /// <summary>
        /// 他のCubeIDと等価かどうかを判定
        /// </summary>
        /// <param name="other">比較対象のCubeID</param>
        /// <returns>等価の場合はtrue、それ以外はfalse</returns>
        public bool Equals(CubeID other)
        {
            return Position.Equals(other.Position);
        }

        /// <summary>
        /// オブジェクトと等価かどうかを判定
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>等価の場合はtrue、それ以外はfalse</returns>
        public override bool Equals(object obj)
        {
            return obj is CubeID other && Equals(other);
        }

        /// <summary>
        /// ハッシュコードを取得
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        /// <summary>
        /// 等価演算子のオーバーロード
        /// </summary>
        public static bool operator ==(CubeID left, CubeID right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 不等価演算子のオーバーロード
        /// </summary>
        public static bool operator !=(CubeID left, CubeID right)
        {
            return !left.Equals(right);
        }
    }
}
