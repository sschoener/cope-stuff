using System;
using System.Linq;

namespace cope.Maths
{
    /// <summary>
    /// Abstract base class for Positions.
    /// </summary>
    public abstract class Position
    {
        /// <summary>
        /// The dimensions of the Position.
        /// </summary>
        protected double[] m_dDim;

        /// <summary>
        /// Indexer giving full access to the dimensions.
        /// </summary>
        /// <param name="i">Dimension</param>
        /// <returns></returns>
        public double this[int i]
        {
            get { return m_dDim[i]; }
            set { m_dDim[i] = value; }
        }

        public abstract override bool Equals(object obj);

        public override int GetHashCode()
        {
            return m_dDim.Aggregate(0, (current, k) => current ^ k.GetHashCode());
        }

        public override string ToString()
        {
            var tmp = new System.Text.StringBuilder("(", m_dDim.Length * 8);
            foreach (double i in m_dDim)
            {
                tmp.AppendFormat("{0}|", i);
            }
            tmp.Remove(tmp.Length - 1, 1).Append(")");
            return tmp.ToString();
        }
    }

    /// <summary>
    /// Class for Positions in 3 dimensional spaces.
    /// </summary>
    public class Position3D : Position, ICloneable, IGenericClonable<Position3D>
    {
        /// <summary>
        /// Constructs a new Position3D.
        /// </summary>
        /// <param name="x">X-dimension value.</param>
        /// <param name="y">Y-dimension value.</param>
        /// <param name="z">Z-dimension value.</param>
        public Position3D(double x, double y, double z)
        {
            m_dDim = new double[3];
            m_dDim[0] = x;
            m_dDim[1] = y;
            m_dDim[2] = z;
        }

        /// <summary>
        /// Constructs a new Position3D and initializes the Z-Dimension value with 0.
        /// </summary>
        /// <param name="x">X-dimension value.</param>
        /// <param name="y">Y-dimension value.</param>
        public Position3D(double x, double y)
        {
            m_dDim = new double[3];
            m_dDim[0] = x;
            m_dDim[1] = y;
        }

        /// <summary>
        /// Constructs a new Position3D and initalizes all dimension-values with 0.
        /// </summary>
        public Position3D()
        {
            m_dDim = new double[3];
        }

        #region methods

        /// <summary>
        /// Returns the distance from this Position3D to another.
        /// </summary>
        /// <param name="to">Position to measure the distance to.</param>
        /// <returns>The Length of the Vector between the two Position3D.</returns>
        public double Distance(Position3D to)
        {
            return new Vector3D(this, to).Length;
        }

        public void Add(Vector3D vec)
        {
            m_dDim[0] += vec.X;
            m_dDim[1] += vec.Y;
            m_dDim[2] += vec.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position3D))
                return false;
            return this == (Position3D)obj;
        }

        public override int GetHashCode()
        {
            return m_dDim.Aggregate(0, (current, k) => current ^ k.GetHashCode());
        }

        #endregion methods

        #region Properties

        /// <summary>
        /// Returns the X-dimension value.
        /// </summary>
        public double X
        {
            get { return m_dDim[0]; }
        }

        /// <summary>
        /// Returns the Y-dimension value.
        /// </summary>
        public double Y
        {
            get { return m_dDim[1]; }
        }

        /// <summary>
        /// Returns the Z-dimension value.
        /// </summary>
        public double Z
        {
            get { return m_dDim[2]; }
        }

        #endregion Properties

        #region Operators

        public static Vector3D operator -(Position3D p1, Position3D p2)
        {
            return new Vector3D(p2[0] - p1[0], p1[1] - p2[1], p1[2] - p2[2]);
        }

        public static Position3D operator -(Position3D p, Vector3D v)
        {
            return new Position3D(p[0] - v[0], p[1] - v[1], p[2] - v[2]);
        }

        public static Position3D operator +(Position3D p, Vector3D v)
        {
            return new Position3D(p[0] + v[0], p[1] + v[1], p[2] + v[2]);
        }

        public static bool operator ==(Position3D p1, Position3D p2)
        {
            if (p1[0].Equals(p2[0]) &&
                p1[1].Equals(p2[1]) &&
                p1[2].Equals(p2[2]))
                return true;
            return false;
        }

        public static bool operator !=(Position3D p1, Position3D p2)
        {
            if (p1[0].Equals(p2[0]) &&
                p1[1].Equals(p2[1]) &&
                p1[2].Equals(p2[2]))
                return false;
            return true;
        }

        public static implicit operator Vector3D(Position3D p)
        {
            return new Vector3D(p[0], p[1], p[2]);
        }

        public static implicit operator Position3D(System.Drawing.Point p)
        {
            return new Position3D(p.X, p.Y);
        }

        public static implicit operator System.Drawing.Point(Position3D p)
        {
            return new System.Drawing.Point((int)Math.Round(p.X), (int)Math.Round(p.Y));
        }


        #endregion Operators

        #region IGenericClonable<Position3D> Member

        /// <summary>
        /// Returns a new instance of Position3D which is a clone of this instance.
        /// </summary>
        /// <returns></returns>
        public Position3D GClone()
        {
            return new Position3D(m_dDim[0], m_dDim[1], m_dDim[2]);
        }

        #endregion IGenericClonable<Position3D> Member

        #region ICloneable Member

        public object Clone()
        {
            return new Position3D(m_dDim[0], m_dDim[1], m_dDim[2]);
        }

        #endregion ICloneable Member
    }
}