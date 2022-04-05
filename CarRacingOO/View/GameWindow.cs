using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarRacingOO.Presenter;

namespace CarRacingOO.View
{
    internal class GameWindow : Window
    {
        private readonly Rectangle _player;
        private readonly GamePresenter _presenter;
        private readonly Canvas _canvas;
        private readonly Rectangle[] _roadMarks;
        private readonly Rectangle[] _cars;
        private readonly Rectangle _star;
        private readonly ImageBrush[] _carImages;
        private readonly ImageBrush[] _playerPowerModeImages;
        private int _playerPowerModeImageIndex;
        private readonly Label _label;

        public GameWindow()
        {
            Width = 525;
            Height = 517;
            Title = "Car racing OO";
            _canvas = new Canvas
            {
                Background = new SolidColorBrush(Colors.Gray),
                Focusable = true
            };

            _canvas.KeyDown += UpdateKeys;
            _canvas.KeyUp += UpdateKeys;
            _canvas.Focus();

            _roadMarks = Enumerable.Range(0, 4).Select(
                i => Add(CreateRoadMark(), 237, i * 170 - 152)).ToArray();
            _player = Add(CreateRectangle(Colors.Yellow), null, 374);
            _player.Fill = CreateImage("playerImage");
            _cars = new[] { Add(CreateRectangle(Colors.Blue)), Add(CreateRectangle(Colors.Purple)) };
            _carImages = CreateImageArray("car", 6);
            _playerPowerModeImages = CreateImageArray("powermode", 4);
            _label = new Label { FontSize = 18, FontWeight = FontWeights.Bold };
            _canvas.Children.Add(_label);

            _star = CreateRectangle(Colors.Yellow, 50, 50);
            _star.Fill = CreateImage("star");

            Content = _canvas;
            _presenter = new GamePresenter(this);
            _presenter.Start();
        }

        private void UpdateKeys(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Left or Key.Right)
            {
                _presenter.SetMove(e.Key == Key.Left, e.IsDown);
            }
            else if (e.Key == Key.Enter)
            {
                _presenter.HandleEnter();
            }
        }

        private Rectangle Add(Rectangle rectangle, int? x = null, int? y = null)
        {
            _canvas.Children.Add(rectangle);
            if (x.HasValue) Canvas.SetLeft(rectangle, x.Value);
            if (y.HasValue) Canvas.SetTop(rectangle, y.Value);
            return rectangle;
        }

        public GameWindow UpdatePlayer(double playerX, bool isPowerMode)
        {
            Canvas.SetLeft(_player, playerX);
            _canvas.Background = isPowerMode ? Brushes.LightCoral : Brushes.Gray;
            if (!isPowerMode) return this;
            _playerPowerModeImageIndex = (_playerPowerModeImageIndex + 1) % 8;
            _player.Fill = _playerPowerModeImages[_playerPowerModeImageIndex / 2];
            return this;
        }

        public GameWindow UpdateCars(IEnumerable<Car> cars)
        {
            var index = 0;
            foreach (var car in cars)
            {
                var carRectangle = _cars[index];
                SetPosition(carRectangle, car.Position);
                carRectangle.Fill = _carImages[car.ImageIndex];
                index++;
            }
            return this;
        }

        public GameWindow UpdateRoadMarks(double speed)
        {
            foreach (var roadMark in _roadMarks)
            {
                var top = Canvas.GetTop(roadMark) + speed;
                Canvas.SetTop(roadMark, top > 510 ? -152 : top);
            }
            return this;
        }

        public GameWindow UpdateStar(Vector? starPosition)
        {
            if (starPosition == null)
            {
                if (_canvas.Children.Contains(_star))
                {
                    _canvas.Children.Remove(_star);
                }
            }
            else
            {
                if (!_canvas.Children.Contains(_star))
                {
                    _canvas.Children.Add(_star);
                }
                SetPosition(_star, starPosition.Value);
            }

            return this;
        }
        public GameWindow UpdateText(string text)
        {
            _label.Content = text;
            return this;
        }

        private static void SetPosition(UIElement rectangle, double x, double y)
        {
            SetPosition(rectangle, new Vector(x, y));
        }

        private static void SetPosition(UIElement rectangle, Vector position)
        {
            Canvas.SetLeft(rectangle, position.X);
            Canvas.SetTop(rectangle, position.Y);
        }

        private static ImageBrush[] CreateImageArray(string baseName, int count)
            => Enumerable
                .Range(1, count)
                .Select(n => CreateImage(baseName + n))
                .ToArray();
        private static ImageBrush CreateImage(string fileName)
            => new(new BitmapImage(new Uri($"pack://application:,,,/View/images/{fileName}.png")));
        private static Rectangle CreateRectangle(Color color, int width = 55, int height = 80)
            => new() { Fill = new SolidColorBrush(color), Height = height, Width = width };
        private static Rectangle CreateRoadMark()
            => new() { Fill = new SolidColorBrush(Colors.White), Height = 106, Width = 20 };
    }
}
