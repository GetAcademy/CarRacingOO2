using System;
using System.Windows;

namespace CarRacingOO.Model
{
    internal class Player
    {
        public Vector PositionLeft { get; private set; }
        public Vector PositionRight => PositionLeft + _size;
        public Rectangle Rectangle => new(PositionLeft, _size);

        private readonly Vector _size = new Vector(11, 15);
        private Direction _direction;
        private readonly int _speedX;

        public Player()
        {
            PositionLeft = new Vector(44, 75);
            _speedX = 2;
        }

        public void SetDirection(bool isLeft, bool isMove)
        {
            _direction = !isMove ? Direction.None :
                          isLeft ? Direction.Left :
                          Direction.Right;
        }

        public void Move()
        {
            var delta = _speedX * (int)_direction;
            var x = Math.Clamp(PositionLeft.X + delta, 0, 100 - _size.X);
            PositionLeft = new Vector(x, PositionLeft.Y);
        }
    }
}
