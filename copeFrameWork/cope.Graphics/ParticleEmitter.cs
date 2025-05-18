using System.Collections.Generic;
using System.Drawing;
using cope.Maths;

namespace cope.Graphics
{
    abstract public class ParticleEmitter
    {
        protected List<Particle> m_particles;
        protected Position3D m_position;

        internal ParticleEmitter(Position3D pos)
        {
            m_position = pos;
            m_particles = new List<Particle>();
        }

        public virtual void Step(System.Drawing.Graphics graphics)
        {
            int count = m_particles.Count;
            for (int i = 0; i < count; i++)
            {
                if (!m_particles[i].DoStep())
                {
                    m_particles.RemoveAt(i--);
                    count--;
                }
                else
                    RenderParticle(graphics, m_particles[i]);
            }
        }

        private static void RenderParticle(System.Drawing.Graphics g, Particle e)
        {
            var p = new Pen(e.Color);
            var x = (float)e.Position.X;
            var y = (float)e.Position.Y;
            g.DrawEllipse(p, x, y, 1f, 1f);
            p.Dispose();
        }
    }
}
