using System;
using System.Drawing;
using cope.Maths;

namespace cope.Graphics
{
    public class FadeParticle : Particle
    {
        public FadeParticle(Color col, Position3D pos, Vector3D vel) : base(col, pos, vel)
        { }

        public override bool DoStep()
        {
            //_position.Add(_velocity);
            m_position += m_velocity;
            float newVariable = m_lifetime/(float)MAX_LIFETIME;
            m_color = Color.FromArgb((int)Math.Min(255,(1 / newVariable)), m_color);
            m_lifetime++;
            if (m_lifetime >= MAX_LIFETIME)
                return false;
            return true;
        }
    }
}
