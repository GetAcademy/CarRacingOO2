using System.Linq;
using System.Windows;

namespace CarRacingOO.Model
{
    internal class Rectangle
    {
        public Vector Position { get; }
        public Vector Size { get; }

        public Rectangle(double x, double y, double width, double height)
            : this(x, y, new Vector(width, height))
        {
        }

        public Rectangle(double x, double y, Vector size)
            : this(new Vector(x, y), size)
        {
        }

        public Rectangle(Vector position, Vector size)
        {
            Position = position;
            Size = size;
        }

        public bool Intersect(Rectangle otherRectangle)
        {
            return new[]
            {
                Position,
                Position + new Vector(Size.X, 0),
                Position + new Vector(0, Size.Y),
                Position + Size,
            }.Any(otherRectangle.Contains);
        }

        public bool Contains(Vector position)
        {
            var x1 = Position.X;
            var y1 = Position.Y;
            var x2 = Position.X + Size.X;
            var y2 = Position.Y + Size.Y;
            return position.X >= x1 && position.X <= x2 &&
                   position.Y >= y1 && position.Y <= y2;
        }
    }
}
