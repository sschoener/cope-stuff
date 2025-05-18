using System;

namespace cope.Maths
{
    public class Matrix4x3 : IGenericClonable<Matrix4x3>
    {
        private double[,] m_matrix;

        public Matrix4x3()
        {
            m_matrix = new double[4,4];
        }

        public double this[int row,int column]
        {
            get { return m_matrix[row, column]; }
            set { m_matrix[row, column] = value; }
        }

        #region methods

        public Matrix4x3 GClone()
        {
            Matrix4x3 matrix = new Matrix4x3();
            Array.Copy(m_matrix, matrix.m_matrix, 9);
            return matrix;
        }

        public void Multiply(Matrix4x3 m)
        {
            double[,] matrix = new double[3,3];
            for (int row = 0; row < 4; row++ )
            {
                for (int column = 0; column < 4; column++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        matrix[row, column] += m_matrix[row, k] * m.m_matrix[k, column];
                    }
                }
            }
            m_matrix = matrix;
        }

        public void Multiply(double scalar)
        {
            m_matrix[0, 0] *= scalar;
            m_matrix[1, 0] *= scalar;
            m_matrix[2, 0] *= scalar;
            m_matrix[3, 0] *= scalar;
            m_matrix[0, 1] *= scalar;
            m_matrix[1, 1] *= scalar;
            m_matrix[2, 1] *= scalar;
            m_matrix[3, 1] *= scalar;
            m_matrix[0, 2] *= scalar;
            m_matrix[1, 2] *= scalar;
            m_matrix[2, 2] *= scalar;
            m_matrix[3, 2] *= scalar;
            m_matrix[0, 3] *= scalar;
            m_matrix[1, 3] *= scalar;
            m_matrix[2, 3] *= scalar;
            m_matrix[3, 3] *= scalar;
        }

        public void Divide(double scalar)
        {
            m_matrix[0, 0] /= scalar;
            m_matrix[1, 0] /= scalar;
            m_matrix[2, 0] /= scalar;
            m_matrix[3, 0] /= scalar;
            m_matrix[0, 1] /= scalar;
            m_matrix[1, 1] /= scalar;
            m_matrix[2, 1] /= scalar;
            m_matrix[3, 1] /= scalar;
            m_matrix[0, 2] /= scalar;
            m_matrix[1, 2] /= scalar;
            m_matrix[2, 2] /= scalar;
            m_matrix[3, 2] /= scalar;
            m_matrix[0, 3] /= scalar;
            m_matrix[1, 3] /= scalar;
            m_matrix[2, 3] /= scalar;
            m_matrix[3, 3] /= scalar;
        }

        public void Add(Matrix4x3 m)
        {
            m_matrix[0, 0] += m.m_matrix[0, 0];
            m_matrix[1, 0] += m.m_matrix[1, 0];
            m_matrix[2, 0] += m.m_matrix[2, 0];
            m_matrix[3, 0] += m.m_matrix[3, 0];
            m_matrix[0, 1] += m.m_matrix[0, 1];
            m_matrix[1, 1] += m.m_matrix[1, 1];
            m_matrix[2, 1] += m.m_matrix[2, 1];
            m_matrix[3, 1] += m.m_matrix[3, 1];
            m_matrix[0, 2] += m.m_matrix[0, 2];
            m_matrix[1, 2] += m.m_matrix[1, 2];
            m_matrix[2, 2] += m.m_matrix[2, 2];
            m_matrix[3, 2] += m.m_matrix[3, 2];
            m_matrix[1, 3] += m.m_matrix[1, 3];
            m_matrix[2, 3] += m.m_matrix[2, 3];
            m_matrix[3, 3] += m.m_matrix[3, 3];
        }

        public void Subtract(Matrix4x3 m)
        {
            m_matrix[0, 0] -= m.m_matrix[0, 0];
            m_matrix[1, 0] -= m.m_matrix[1, 0];
            m_matrix[2, 0] -= m.m_matrix[2, 0];
            m_matrix[3, 0] -= m.m_matrix[3, 0];
            m_matrix[0, 1] -= m.m_matrix[0, 1];
            m_matrix[1, 1] -= m.m_matrix[1, 1];
            m_matrix[2, 1] -= m.m_matrix[2, 1];
            m_matrix[3, 1] -= m.m_matrix[3, 1];
            m_matrix[0, 2] -= m.m_matrix[0, 2];
            m_matrix[1, 2] -= m.m_matrix[1, 2];
            m_matrix[2, 2] -= m.m_matrix[2, 2];
            m_matrix[3, 2] -= m.m_matrix[3, 2];
            m_matrix[0, 3] -= m.m_matrix[0, 3];
            m_matrix[1, 3] -= m.m_matrix[1, 3];
            m_matrix[2, 3] -= m.m_matrix[2, 3];
            m_matrix[3, 3] -= m.m_matrix[3, 3];
        }

        #endregion

        #region statics

        /// <summary>
        /// Creates a Matrix for counter-clockwise 3D rotation.
        /// </summary>
        /// <param name="axis">The vector to rotate around.</param>
        /// <param name="angle">The angle to rotate by in radians.</param>
        /// <returns></returns>
        public static Matrix4x3 CreateRotationMatrix(Vec3D axis, double angle)
        {
            Matrix4x3 matrix = new Matrix4x3();
            matrix[0, 0] = (1 - Math.Cos(angle)) * axis.X * axis.X + Math.Cos(angle);
            matrix[0, 1] = (1 - Math.Cos(angle)) * axis.X * axis.Y - Math.Sin(angle) * axis.Z;
            matrix[0, 2] = (1 - Math.Cos(angle)) * axis.X * axis.Z + Math.Sin(angle) * axis.Y;

            matrix[1, 0] = (1 - Math.Cos(angle)) * axis.Y * axis.X + Math.Sin(angle) * axis.Z;
            matrix[1, 1] = (1 - Math.Cos(angle)) * axis.Y * axis.Y + Math.Cos(angle);
            matrix[1, 2] = (1 - Math.Cos(angle)) * axis.Y * axis.Z - Math.Sin(angle) * axis.X;

            matrix[2, 0] = (1 - Math.Cos(angle)) * axis.Z * axis.X - Math.Sin(angle) * axis.Y;
            matrix[2, 1] = (1 - Math.Cos(angle)) * axis.Z * axis.Y + Math.Sin(angle) * axis.X;
            matrix[2, 2] = (1 - Math.Cos(angle)) * axis.Z * axis.Z + Math.Cos(angle);
            matrix[3, 3] = 1;
            return matrix;
        }

        #endregion

        #region operators

        public static Matrix4x3 operator *(double d, Matrix4x3 m)
        {
            Matrix4x3 matrix = m.GClone();
            matrix.Multiply(d);
            return matrix;
        }

        public static Matrix4x3 operator *(Matrix4x3 m, double d)
        {
            return d * m;
        }

        public static Vec3D operator *(Vec3D vec, Matrix4x3 m)
        {
            double x, y, z;
            x = m.m_matrix[0, 0] * vec.X + m.m_matrix[0, 1] * vec.Y + m.m_matrix[0, 2] * vec.Z + m.m_matrix[0, 3];
            y = m.m_matrix[1, 0] * vec.X + m.m_matrix[1, 1] * vec.Y + m.m_matrix[1, 2] * vec.Z + m.m_matrix[1, 3];
            z = m.m_matrix[2, 0] * vec.X + m.m_matrix[2, 1] * vec.Y + m.m_matrix[2, 2] * vec.Z + m.m_matrix[2, 3];
            return new Vec3D(x, y, z);
        }

        public static Vec3D operator *(Matrix4x3 m, Vec3D vec)
        {
            return vec * m;
        }

        public static Matrix4x3 operator *(Matrix4x3 m1, Matrix4x3 m2)
        {
            Matrix4x3 matrix = new Matrix4x3();
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        matrix.m_matrix[row, column] += m1.m_matrix[row, k] * m2.m_matrix[k, column];
                    }
                }
            }
            return matrix;
        }

        public static Matrix4x3 operator /(Matrix4x3 m, double d)
        {
            Matrix4x3 matrix = m.GClone();
            matrix.Divide(d);
            return matrix;
        }

        public static Matrix4x3 operator +(Matrix4x3 m1, Matrix4x3 m2)
        {
            Matrix4x3 matrix = m1.GClone();
            matrix.Add(m2);
            return matrix;
        }

        public static Matrix4x3 operator -(Matrix4x3 m1, Matrix4x3 m2)
        {
            Matrix4x3 matrix = m1.GClone();
            matrix.Subtract(m2);
            return matrix;
        }

        #endregion
    }
}
