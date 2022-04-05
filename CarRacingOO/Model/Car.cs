using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CarRacingOO.Model
{
    internal class Car
    {
        private readonly Vector _size = new(11, 15);
        public Rectangle Rectangle { get; private set; } = null!;
        public int ImageIndex { get; private set; }
        public double Y => Rectangle.Position.Y;
        public Car()
        {
            Reset();
        }

        public void Reset()
        {
            var x = MyRandom.Instance.Next(0, 89);
            var y = MyRandom.Instance.Next(-80, -20);
            ImageIndex = MyRandom.Instance.Next(0, 5);
            Rectangle = new Rectangle(x, y, _size);
        }

        public void Move(double speedY)
        {
            var x = Rectangle.Position.X;
            var y = Rectangle.Position.Y;
            y += speedY;
            Rectangle = new Rectangle(x, y, _size);
        }

        public bool Contains(params Vector[] positions)
        {
            return positions.Any(Rectangle.Contains);
        }

        public bool CrashesWith(Player player)
        {
            return Rectangle.Intersect(player.Rectangle);
        }
    }
}
