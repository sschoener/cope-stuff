namespace cope.Maths
{
    public class Ray3D
    {
        public Ray3D(Vec3D start, Vec3D direction)
        {
            Start = start.GClone();
            Direction = direction.GClone().Normalize();
        }

        public Vec3D Start
        {
            get;
            protected set;
        }

        public Vec3D Direction
        {
            get;
            protected set;
        }

        public Vec3D GetPoint(double param)
        {
            return Start + param * Direction;
        }
    }
}
