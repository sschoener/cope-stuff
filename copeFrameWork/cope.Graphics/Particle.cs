using System.Drawing;
using cope.Maths;

namespace cope.Graphics
{
    public abstract class Particle
    {
        protected const int MAX_LIFETIME = 1000;

        protected int m_lifetime;
        protected Vector3D m_velocity;
        protected Position3D m_position;
        protected Color m_color;

        protected Particle(Color col, Position3D pos, Vector3D vel)
        {
            m_color = col;
            m_position = pos;
            m_velocity = vel;
            m_lifetime = 1;
        }

        public abstract bool DoStep();

        public int Lifetime
        {
            get { return m_lifetime; }
        }

        public Vector3D Velocity
        {
            get { return m_velocity; }
        }

        public Position3D Position
        {
            get { return m_position; }
        }

        public Color Color
        {
            get { return m_color; }
        }

        public override string ToString()
        {
            return "L: " + m_lifetime + ", V: " + m_velocity + ", P: " + m_position + ", C: " + m_color;
        }
    }
}