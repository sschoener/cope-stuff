using System;
using System.Linq;

namespace cope.Maths
{
    /// <summary>
    /// Base vector class.
    /// </summary>
    public abstract class Vector : ICloneable
    {
        /// <summary>
        /// This array holds the dimensions this vector has.
        /// </summary>
        protected double[] m_dDim;

        /// <summary>
        /// Clones the specified object
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        #region Properties

        /// <summary>
        /// Indexer giving access to the dimensions of this vector.
        /// </summary>
        /// <param name="i">Dimension</param>
        /// <returns>Returns the value of the dimension.</returns>
        public double this[int i]
        {
            get { return m_dDim[i]; }
            set { m_dDim[i] = value; }
        }

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        public double Length
        {
            get
            {
                double sum = m_dDim.Sum(d => Math.Pow(d, 2));
                return Math.Sqrt(sum);
            }
        }

        /// <summary>
        /// Returns the squared length of the vector.
        /// </summary>
        public double LengthSquared
        {
            get
            {
                double sum = m_dDim.Sum(d => Math.Pow(d, 2));
                return sum;
            }
        }

        #endregion Properties

        public abstract override bool Equals(object obj);

        public override int GetHashCode()
        {
            int? hash = null;
            foreach (double k in m_dDim)
            {
                if (hash == null)
                    hash = k.GetHashCode();
                hash ^= k.GetHashCode();
            }
            return (int)hash;
        }

        public override string ToString()
        {
            var tmp = new System.Text.StringBuilder("(", m_dDim.Length * 8);
            foreach (double i in m_dDim)
            {
                tmp.AppendFormat("{0}; ", i);
            }
            tmp.Remove(tmp.Length - 1, 1).Append(")");
            return tmp.ToString();
        }
    }

    /// <summary>
    /// A class for 3 dimensional vectors.
    /// </summary>
    public class Vector3D : Vector, IComparable, IGenericClonable<Vector3D>
    {
        #region ctors

        /// <summary>
        /// Constructs a new Vector3D.
        /// </summary>
        /// <param name="x">X dimension value.</param>
        /// <param name="y">Y dimension value.</param>
        /// <param name="z">Z dimension value.</param>
        public Vector3D(double x, double y, double z)
            : this()
        {
            m_dDim[0] = x;
            m_dDim[1] = y;
            m_dDim[2] = z;
        }

        /// <summary>
        /// Constructs a new Vector3D and initializes the z-dimension with 0.
        /// </summary>
        /// <param name="x">X dimension value.</param>
        /// <param name="y">Y dimension value.</param>
        public Vector3D(double x, double y)
            : this()
        {
            m_dDim[0] = x;
            m_dDim[1] = y;
        }

        /// <summary>
        /// Constructs a new Vector3D that points from Position3D 1 to Position3D 2.
        /// </summary>
        /// <param name="from">Position3D 1</param>
        /// <param name="to">Position 3D 2</param>
        public Vector3D(Position3D from, Position3D to)
            : this()
        {
            m_dDim[0] = to.X - from.X;
            m_dDim[1] = to.Y - from.Y;
            m_dDim[2] = to.Z - from.Z;
        }

        public Vector3D(Vector3D from, Vector3D to)
            : this()
        {
            m_dDim[0] = to.X - from.X;
            m_dDim[1] = to.Y - from.Y;
            m_dDim[2] = to.Z - from.Z;
        }

        /// <summary>
        /// Constructs a new Vector3D which is the position vector of the specified Position3D.
        /// </summary>
        /// <param name="p">Position3D to construct the vector from.</param>
        public Vector3D(Position3D p)
            : this()
        {
            m_dDim[0] = p.X;
            m_dDim[1] = p.Y;
            m_dDim[2] = p.Z;
        }

        /// <summary>
        /// Constructs a new vector and sets all it's dimensions to 0.
        /// </summary>
        public Vector3D()
        {
            m_dDim = new double[3];
        }

        #endregion

        #region methods

        /// <summary>
        /// Returns the dot-product (scalar-product) of this Vector3D and another Vector3D.
        /// </summary>
        /// <param name="v">Vector3D</param>
        /// <returns></returns>
        public double DotP(Vector3D v)
        {
            return this[0] * v[0] + this[1] * v[1] + this[2] * v[2];
        }

        /// <summary>
        /// Returns the cross-product of this Vector3D and another Vector3D.
        /// </summary>
        /// <param name="v">Vector3D</param>
        /// <returns></returns>
        public Vector3D CrossP(Vector3D v)
        {
            return new Vector3D(this[1] * v[2] - this[2] * v[1],
                                this[2] * v[0] - this[0] * v[2],
                                this[0] * v[1] - this[1] * v[0]);
        }

        /// <summary>
        /// Normalizes the length of this Vector3D to 1.
        /// </summary>
        /// <returns>This instance of Vector3D.</returns>
        public Vector3D Normalize()
        {
            double l = Length;
            this[0] /= l;
            this[1] /= l;
            this[2] /= l;
            return this;
        }

        public Vector3D VectorTo(Vector3D target)
        {
            return new Vector3D(this, target);
        }

        public Vector3D DirectionTo(Vector3D target)
        {
            return new Vector3D(this, target).Normalize();
        }

        public Vector3D VectorFrom(Vector3D origin)
        {
            return new Vector3D(origin, this);
        }

        public Vector3D DirectionFrom(Vector3D origin)
        {
            return new Vector3D(origin, this).Normalize();
        }

        /// <summary>
        /// Returns if obj equals this instance of Vector3D.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>Returns true if obj and this instance of Vector3D are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector3D))
                return false;
            return this == (Vector3D)obj;
        }

        public override int GetHashCode()
        {
            int? hash = null;
            foreach (double k in m_dDim)
            {
                if (hash == null)
                    hash = k.GetHashCode();
                hash ^= k.GetHashCode();
            }
            return (int)hash;
        }

        #endregion methods

        #region Properties

        /// <summary>
        /// The X-dimension value of this instance of Vector3D.
        /// </summary>
        public double X
        {
            get { return m_dDim[0]; }
        }

        /// <summary>
        /// The Y-dimension value of this instance of Vector3D.
        /// </summary>
        public double Y
        {
            get { return m_dDim[1]; }
        }

        /// <summary>
        /// The Z-dimension value of this instance of Vector3D.
        /// </summary>
        public double Z
        {
            get { return m_dDim[2]; }
        }

        /// <summary>
        /// Returns a new instance of Vector3D which is the normalized vector of this instance of Vector3D.
        /// </summary>
        public Vector3D Normalized
        {
            get { return GClone() / Length; }
        }

        #endregion Properties

        #region Operators

        public static Vector3D operator *(Vector3D v, int s)
        {
            return new Vector3D(v[0] * s, v[1] * s, v[2] * s);
        }

        public static Vector3D operator *(Vector3D v, double s)
        {
            return new Vector3D(v[0] * s, v[1] * s, v[2] * s);
        }

        public static Vector3D operator *(int s, Vector3D v)
        {
            return new Vector3D(v[0] * s, v[1] * s, v[2] * s);
        }

        public static Vector3D operator *(double s, Vector3D v)
        {
            return new Vector3D(v[0] * s, v[1] * s, v[2] * s);
        }

        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];
        }

        public static double operator /(Vector3D v1, Vector3D v2)
        {
            return v1[0] / v2[0] + v1[1] / v2[1] + v1[2] / v2[2];
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1[0] + v2[0], v1[1] + v2[1], v1[2] + v2[2]);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1[0] - v2[0], v1[1] - v2[1], v1[2] - v2[2]);
        }

        public static Vector3D operator /(Vector3D v, int s)
        {
            return new Vector3D(v[0] / s, v[1] / s, v[2] / s);
        }

        public static Vector3D operator /(Vector3D v, double s)
        {
            return new Vector3D(v[0] / s, v[1] / s, v[2] / s);
        }

        public static Vector3D operator -(Vector3D v)
        {
            return new Vector3D(-v[0], -v[1], -v[2]);
        }

        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            if (v1[0] == v2[0] &&
                v1[1] == v2[1] &&
                v1[2] == v2[2])
                return true;
            return false;
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            if (v1[0] == v2[0] &&
                v1[1] == v2[1] &&
                v1[2] == v2[2])
                return false;
            return true;
        }

        public static explicit operator Position3D(Vector3D v)
        {
            return new Position3D(v[0], v[1], v[3]);
        }

        #endregion Operators

        #region IComparable Member

        /// <summary>
        /// Compares this instance of Vector3D to another object.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>Returns 1 if this instance'S length is greater, 0 if it's equal and -1 if it's smaller than obj's length.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Vector3D))
                return 1;
            var cmpto = (Vector3D)obj;
            if (cmpto.Length > Length)
                return -1;
            if (cmpto.Length == Length)
                return 0;
            return 1;
        }

        #endregion IComparable Member

        #region IGenericClonable<Vector3D> Member

        /// <summary>
        /// Returns a Vector3D which is a clone of this instance of Vector3D.
        /// </summary>
        /// <returns></returns>
        public Vector3D GClone()
        {
            return new Vector3D(m_dDim[0], m_dDim[1], m_dDim[2]);
        }

        #endregion IGenericClonable<Vector3D> Member

        #region ICloneable Member

        public override object Clone()
        {
            return new Vector3D(m_dDim[0], m_dDim[1], m_dDim[2]);
        }

        #endregion ICloneable Member
    }
}