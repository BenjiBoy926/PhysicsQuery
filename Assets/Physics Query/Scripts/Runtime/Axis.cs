using UnityEngine;

namespace PQuery
{
    public abstract class Axis
    {
        public abstract int Dimension { get; }
        public Vector3 Vector
        {
            get
            {
                Vector3 vector = Vector3.zero;
                vector[Dimension] = 1;
                return vector;
            }
        }
        public int CrossDimension1 => (Dimension + 1) % 3;
        public int CrossDimension2 => (Dimension + 2) % 3;
    }
    public class Axis_X : Axis
    {
        public override int Dimension => 0;
    }
    public class Axis_Y : Axis
    {
        public override int Dimension => 1;
    }
    public class Axis_Z : Axis
    {
        public override int Dimension => 2;
    }
}