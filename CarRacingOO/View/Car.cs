using System.Windows;

namespace CarRacingOO.View
{
    internal class Car
    {
        public Vector Position { get; }
        public int ImageIndex { get; }

        public Car(Vector position, int imageIndex)
        {
            Position = position;
            ImageIndex = imageIndex;
        }
    }
}
