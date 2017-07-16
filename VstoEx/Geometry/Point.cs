namespace VstoEx.Geometry
{
    public class Point
    {
        public float Y { get;}
        public float X { get;}

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();
        // ReSharper disable CompareOfFloatsByEqualityOperator
        bool Equals(Point other) => other != null && other.X == X && other.Y == Y;
        // ReSharper enable CompareOfFloatsByEqualityOperator
    }
}