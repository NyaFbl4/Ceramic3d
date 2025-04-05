using UnityEngine;

namespace Ceramic3d
{
    [System.Serializable]
    public class MatrixJson
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public Matrix4x4 ToMatrix4x4()
        {
            return new Matrix4x4(
                new Vector4(m00, m10, m20, m30),
                new Vector4(m01, m11, m21, m31),
                new Vector4(m02, m12, m22, m32),
                new Vector4(m03, m13, m23, m33)
            );
        }
        
        public static MatrixJson FromMatrix4x4(Matrix4x4 matrix)
        {
            return new MatrixJson()
            {
                m00 = matrix.m00, m01 = matrix.m01, m02 = matrix.m02, m03 = matrix.m03,
                m10 = matrix.m10, m11 = matrix.m11, m12 = matrix.m12, m13 = matrix.m13,
                m20 = matrix.m20, m21 = matrix.m21, m22 = matrix.m22, m23 = matrix.m23,
                m30 = matrix.m30, m31 = matrix.m31, m32 = matrix.m32, m33 = matrix.m33
            };
        }
    }
}