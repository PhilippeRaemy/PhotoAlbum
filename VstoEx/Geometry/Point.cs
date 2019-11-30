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

        public static Point operator + (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator - (Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator * (Point p, float f)
        {
            return new Point(p.X * f, p.Y * f);
        }

        public static Point operator / (Point p, float f)
        {
            return new Point(p.X / f, p.Y / f);
        }

        public static Point operator * (float f, Point p)
        {
            return new Point(p.X * f, p.Y * f);
        }

        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();
        // ReSharper disable CompareOfFloatsByEqualityOperator
        bool Equals(Point other) => other != null && other.X == X && other.Y == Y;
        // ReSharper enable CompareOfFloatsByEqualityOperator

        public Rectangle AsRectangle() => new Rectangle(this,this);
    }
}