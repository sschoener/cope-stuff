using System;

namespace cope.Maths
{
    /// <summary>
    /// Fraction-class offering various functions.
    /// </summary>
    public class Fraction : ICloneable, IComparable, IGenericClonable<Fraction>
    {
        /// <summary>
        /// The Denominator of this Fraction.
        /// </summary>
        protected long m_lDen;

        /// <summary>
        /// The Numerator of this Fraction.
        /// </summary>
        protected long m_lNum;

        /// <summary>
        /// Constructs a new Fraction.
        /// </summary>
        /// <param name="numerator">The Numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction. Can't be 0.</param>
        /// <exception cref="DivideByZeroException">Throws a DivideByZeroException if denominator is 0.</exception>
        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException("The Denominator of a fraction must not be 0!");

            m_lNum = numerator;
            m_lDen = denominator;
            Reduce();
        }

        /// <summary>
        /// Tries to construct a new Fraction from a double.
        /// </summary>
        /// <param name="value">Double to convert into a Fraction.</param>
        public Fraction(double value)
        {
            m_lDen = 1;
            while (value % 1 != 0)
            {
                value *= 10;
                m_lDen *= 10;
            }
            m_lNum = (long) value;
            Reduce();
        }

        #region methods

        /// <summary>
        /// Expands this Fraction by f. F can't be 0.
        /// </summary>
        /// <param name="f">Factor f to expand by.</param>
        /// <returns>Returns this instance of Fraction.</returns>
        /// <exception cref="DivideByZeroException">Throws a DivideByZeroException if f is 0.</exception>
        public Fraction Expand(long f)
        {
            if (f == 0)
                throw new DivideByZeroException("The Denominator of a fraction must not be 0!");
            m_lNum *= f;
            m_lDen *= f;
            return this;
        }

        /// <summary>
        /// Clones this Fraction and expands the clone by f. F can't be 0.
        /// </summary>
        /// <param name="f">Factor f to expand by.</param>
        /// <returns>Returns a new instance of Fraction.</returns>
        /// <exception cref="DivideByZeroException">Throws a DivideByZeroException if f is 0.</exception>
        public Fraction GetExpanded(long f)
        {
            return GClone().Expand(f);
        }

        /// <summary>
        /// Reduces this Fraction.
        /// </summary>
        /// <returns>Returns this instace of Fraction.</returns>
        public Fraction Reduce()
        {
            var gcd = (long) MathUtil.GCD(m_lNum, m_lDen);
            m_lNum /= gcd;
            m_lDen /= gcd;
            return this;
        }

        /// <summary>
        /// Clones this Fraction and reduces the clone.
        /// </summary>
        /// <returns>Returns a new instance of Fraction.</returns>
        public Fraction GetReduced()
        {
            return GClone().Reduce();
        }

        /// <summary>
        /// Equalizes the denominators of this Fraction and Fraction f.
        /// </summary>
        /// <param name="f">Fraction to equalize with.</param>
        /// <returns>This instance of Fraction.</returns>
        public Fraction ExpandTo(Fraction f)
        {
            var lcm = (long) MathUtil.LCM(m_lDen, f.m_lDen);
            Expand(lcm);
            f.Expand(lcm);
            return this;
        }

        /// <summary>
        /// Clones this Fraction and expands the clone to Fraction f.
        /// </summary>
        /// <param name="f">Fraction to equalize with.</param>
        /// <returns>Returns a new instance of Fraction.</returns>
        public Fraction GetExpandedTo(Fraction f)
        {
            return GClone().ExpandTo(f);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Fraction))
                return false;
            return ((Fraction) obj).m_lDen == m_lDen && ((Fraction) obj).m_lNum == m_lNum;
        }

        public override int GetHashCode()
        {
            return (int) m_lNum ^ (int) (m_lNum >> 32) ^ (int) m_lDen ^ (int) (m_lDen >> 32);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", m_lNum, m_lDen);
        }

        #endregion methods

        #region Properties

        /// <summary>
        /// Returns the value represented by this Fraction.
        /// </summary>
        public double Value
        {
            get { return (double) m_lNum / m_lDen; }
        }

        #endregion Properties

        #region Operators

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.m_lNum * f2.m_lDen, f1.m_lDen * f2.m_lNum);
        }

        public static Fraction operator /(Fraction f1, int v)
        {
            return new Fraction(f1.m_lNum, f1.m_lDen * v);
        }

        public static Fraction operator /(int v, Fraction f1)
        {
            return new Fraction(v * f1.m_lDen, 1 * f1.m_lNum);
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.m_lNum * f2.m_lNum, f1.m_lDen * f2.m_lNum);
        }

        public static Fraction operator *(Fraction f, int v)
        {
            return new Fraction(f.m_lNum * v, f.m_lDen);
        }

        public static Fraction operator *(int v, Fraction f)
        {
            return f * v;
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            var lcm = (int) MathUtil.LCM(f1.m_lDen, f2.m_lDen);
            return new Fraction(f1.m_lNum * lcm + f2.m_lNum * lcm, f1.m_lDen * lcm);
        }

        public static Fraction operator +(Fraction f1, int v)
        {
            return new Fraction(v, 1) + f1;
        }

        public static Fraction operator +(int v, Fraction f1)
        {
            return v + f1;
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            var lcm = (int) MathUtil.LCM(f1.m_lDen, f2.m_lDen);
            return new Fraction(f1.m_lNum * lcm - f2.m_lNum * lcm, f1.m_lDen * lcm);
        }

        public static Fraction operator -(Fraction f, int v)
        {
            return new Fraction(f.m_lNum - v * f.m_lDen, f.m_lDen);
        }

        public static Fraction operator -(int v, Fraction f)
        {
            return new Fraction(v * f.m_lDen - f.m_lNum, f.m_lDen);
        }

        public static Fraction operator -(Fraction f)
        {
            return new Fraction(-f.m_lNum, f.m_lDen);
        }

        public static bool operator ==(Fraction f1, Fraction f2)
        {
            f1.Reduce();
            f2.Reduce();
            return f1.m_lDen == f2.m_lDen && f1.m_lNum == f2.m_lNum;
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return !(f1 == f2);
        }

        public static bool operator <=(Fraction f1, Fraction f2)
        {
            f1.ExpandTo(f2);
            return f1.m_lNum <= f2.m_lNum;
        }

        public static bool operator >=(Fraction f1, Fraction f2)
        {
            f1.ExpandTo(f2);
            return f1.m_lNum >= f2.m_lNum;
        }

        public static bool operator <(Fraction f1, Fraction f2)
        {
            f1.ExpandTo(f2);
            return f1.m_lNum < f2.m_lNum;
        }

        public static bool operator >(Fraction f1, Fraction f2)
        {
            f1.ExpandTo(f2);
            return f1.m_lNum > f2.m_lNum;
        }

        public static explicit operator int(Fraction f)
        {
            return (int) Math.Round(f.Value, 0);
        }

        public static implicit operator Fraction(int i)
        {
            return new Fraction(i, 1);
        }

        public static implicit operator double(Fraction f)
        {
            return f.Value;
        }

        #endregion Operators

        #region IComparable Member

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Fraction))
                return 1;
            var cmpto = (Fraction) obj;
            if (cmpto.Value == Value)
                return 0;
            if (cmpto.Value > Value)
                return -1;
            return 1;
        }

        #endregion IComparable Member

        #region ICloneable Member

        public object Clone()
        {
            return new Fraction(m_lNum, m_lDen);
        }

        #endregion ICloneable Member

        #region IGenericClonable<Fraction> Member

        /// <summary>
        /// Returns a new instace of Fraction which is a clone of this instance.
        /// </summary>
        /// <returns>A new instance of Fraction.</returns>
        public Fraction GClone()
        {
            return new Fraction(m_lNum, m_lDen);
        }

        #endregion IGenericClonable<Fraction> Member
    }
}