using System.Windows;

namespace CarRacingOO.Model
{
    class Star
    {
        private readonly Vector _size = new(10, 10);
        public Rectangle Rectangle { get; private set; } = null!;
        public Star()
        {
            Reset();
        }

        private void Reset()
        {
            var x = MyRandom.Instance.Next(0, 86);
            var y = MyRandom.Instance.Next(-80, -20);
            Rectangle = new Rectangle(x, y, _size);
        }

        public bool Move(int speedY = 1)
        {
            var x = Rectangle.Position.X;
            var y = Rectangle.Position.Y;
            y += speedY;
            if (y > 100) return false;
            Rectangle = new Rectangle(x, y, _size);
            return true;
        }

        public bool CrashesWith(Player player)
        {
            return Rectangle.Intersect(player.Rectangle);
        }
    }
}
