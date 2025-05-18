using System;

namespace cope.Maths
{
    public class Vec3D : IGenericClonable<Vec3D>, IComparable, IComparable<Vec3D>
    {
        private double m_dX;
        private double m_dY;
        private double m_dZ;

        #region ctors

        /// <summary>
        /// Constructs a new Vec3D.
        /// </summary>
        /// <param name="x">X dimension value.</param>
        /// <param name="y">Y dimension value.</param>
        /// <param name="z">Z dimension value.</param>
        public Vec3D(double x, double y, double z)
        {
            m_dX = x;
            m_dY = y;
            m_dZ = z;
        }

        /// <summary>
        /// Constructs a new Vec3D and initializes the z-dimension with 0.
        /// </summary>
        /// <param name="x">X dimension value.</param>
        /// <param name="y">Y dimension value.</param>
        public Vec3D(double x, double y)
        {
            m_dX = x;
            m_dY = y;
            m_dZ = 0;
        }

        public Vec3D(Vec3D from, Vec3D to)
        {
            m_dX = to.m_dX - from.m_dX;
            m_dY = to.m_dY - from.m_dY;
            m_dZ = to.m_dZ - from.m_dZ;
        }

        public Vec3D()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indexer giving access to the dimensions of this vector.
        /// </summary>
        /// <param name="i">Dimension</param>
        /// <returns>Returns the value of the dimension.</returns>
        public double this[int i]
        {
            get
            {
                if (i == 0)
                    return m_dX;
                if (i == 1) return m_dY;
                return m_dZ;
            }
            set
            {
                if (i == 0)
                    m_dX = value;
                else if (i == 1)
                    m_dY = value;
                else
                    m_dZ = value;
            }
        }

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        public double Length
        {
            get
            {
                double sum = Math.Pow(m_dX, 2) + Math.Pow(m_dY, 2) + Math.Pow(m_dZ, 2);
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
                return Math.Pow(m_dX, 2) + Math.Pow(m_dY, 2) + Math.Pow(m_dZ, 2);
            }
        }

        /// <summary>
        /// The X-dimension value of this instance of Vec3D.
        /// </summary>
        public double X
        {
            get { return m_dX; }
        }

        /// <summary>
        /// The Y-dimension value of this instance of Vec3D.
        /// </summary>
        public double Y
        {
            get { return m_dY; }
        }

        /// <summary>
        /// The Z-dimension value of this instance of Vec3D.
        /// </summary>
        public double Z
        {
            get { return m_dZ; }
        }

        /// <summary>
        /// Returns a Vec3D which is the normalized vector of this instance of Vec3D.
        /// </summary>
        public Vec3D Normalized
        {
            get { return GClone() / Length; }
        }

        public string MapleString
        {
            get { return ToMaple(); }
        }

        #endregion Properties

        #region methods

        /// <summary>
        /// Returns the dot-product (scalar-product) of this Vec3D and another Vec3D.
        /// </summary>
        /// <param name="v">Vec3D</param>
        /// <returns></returns>
        public double DotP(Vec3D v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        /// <summary>
        /// Returns the cross-product of this Vec3D and another Vec3D.
        /// </summary>
        /// <param name="v">Vec3D</param>
        /// <returns></returns>
        public Vec3D CrossP(Vec3D v)
        {
            return new Vec3D(Y * v.Z - Z * v.Y,
                             Z * v.X - X * v.Z,
                             X * v.Y - Y * v.X);
        }

        public Vec3D Normalize()
        {
            return GClone() / Length;
        }

        public Vec3D VectorTo(Vec3D target)
        {
            return new Vec3D(this, target);
        }

        public Vec3D DirectionTo(Vec3D target)
        {
            return new Vec3D(this, target).Normalize();
        }

        public Vec3D VectorFrom(Vec3D origin)
        {
            return new Vec3D(origin, this);
        }

        public Vec3D DirectionFrom(Vec3D origin)
        {
            return new Vec3D(origin, this).Normalize();
        }

        public double DistanceSquaredTo(Vec3D pos)
        {
            return Math.Pow(pos.X - X, 2) + Math.Pow(pos.Y - Y, 2) + Math.Pow(pos.Z - Z,2);
        }

        public double DistanceTo(Vec3D pos)
        {
            return Math.Sqrt(Math.Pow(pos.X - X, 2) + Math.Pow(pos.Y - Y, 2) + Math.Pow(pos.Z - Z, 2));
        }

        /// <summary>
        /// Rotates this Vec3d counter-clockwise around axis by a specified angle.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angleInRadians"></param>
        /// <returns></returns>
        public Vec3D RotateAround(Vec3D axis, double angleInRadians)
        {
            return Matrix4x3.CreateRotationMatrix(axis, angleInRadians) * this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vec3D))
                return false;
            Vec3D other = (Vec3D)obj;
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public override int GetHashCode()
        {
            return m_dX.GetHashCode() ^ m_dY.GetHashCode() ^ m_dZ.GetHashCode();
        }

        public Vec3D GClone()
        {
            return new Vec3D(X, Y, Z);
        }

        public override string ToString()
        {
            return "( " + m_dX + "; " + m_dY + "; " + m_dZ + " )";
        }

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Vec3D))
                return 1;
            var cmpto = (Vec3D)obj;
            if (cmpto.Length > Length)
                return -1;
            if (cmpto.Length == Length)
                return 0;
            return 1;
        }

        public int CompareTo(Vec3D obj)
        {
            if (obj.Length > Length)
                return -1;
            if (obj.Length == Length)
                return 0;
            return 1;
        }

        /// <summary>
        /// Returns the reflection of this instance of Ray3D on a plane with the specified normal.
        /// </summary>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public Vec3D ReflectOnAsIncoming(Vec3D planeNormal)
        {
            return  this - 2 * (planeNormal.DotP(this)) * planeNormal;
        }

        public Vec3D ReflectOnAsOutgoing(Vec3D planeNormal)
        {
            return 2 * (planeNormal.DotP(this)) * planeNormal - this;
        }

        public string ToMaple()
        {
            return '<' + m_dX.ToString(System.Globalization.CultureInfo.InvariantCulture) + '|' +
                   m_dY.ToString(System.Globalization.CultureInfo.InvariantCulture) + '|' +
                   m_dZ.ToString(System.Globalization.CultureInfo.InvariantCulture) + ">";
        }

        public bool IsBetweenLimits(double xMin, double xMax, double yMin, double yMax, double zMin, double zMax)
        {
            return X >= xMin && X <= xMax && Y >= yMin && Y <= yMax && Z >= zMin && Z <= zMax;
        }

        #endregion

        #region Operators

        public static Vec3D operator *(Vec3D v, int s)
        {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3D operator *(Vec3D v, double s)
        {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3D operator *(int s, Vec3D v)
        {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3D operator *(double s, Vec3D v)
        {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static double operator *(Vec3D v1, Vec3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double operator /(Vec3D v1, Vec3D v2)
        {
            return v1.X / v2.X + v1.Y / v2.Y + v1.Z / v2.Z;
        }

        public static Vec3D operator +(Vec3D v1, Vec3D v2)
        {
            return new Vec3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vec3D operator -(Vec3D v1, Vec3D v2)
        {
            return new Vec3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vec3D operator /(Vec3D v, int s)
        {
            return new Vec3D(v.X / s, v.Y / s, v.Z / s);
        }

        public static Vec3D operator /(Vec3D v, double s)
        {
            return new Vec3D(v.X / s, v.Y / s, v.Z / s);
        }

        public static Vec3D operator -(Vec3D v)
        {
            return new Vec3D(-v.X, -v.Y, -v.Z);
        }

        public static bool operator ==(Vec3D v1, Vec3D v2)
        {
            if (v1.X == v2.X &&
                v1.Y == v2.Y &&
                v1.Z == v2.Z)
                return true;
            return false;
        }

        public static bool operator !=(Vec3D v1, Vec3D v2)
        {
            if (v1.X == v2.X &&
                v1.Y == v2.Y &&
                v1.Z == v2.Z)
                return false;
            return true;
        }

        #endregion Operators
    }
}
