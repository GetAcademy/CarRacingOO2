using System;
using System.Linq;

namespace CarRacingOO.Model
{
    internal class Game
    {
        /*
         * Spillet foregår innenfor en tenkt firkant med bredde og høyde på 100.
         * I brukergrensesnittet ganger vi opp til den størrelsen vi ønsker.
         */
        public Player Player { get; }
        public Car[] Cars { get; }
        public Star? Star { get; private set; }
        public double Speed { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsPowerMode { get; private set; }

        public string Text => "Survived " + _score.ToString("#.#") + " Seconds"
            + (IsRunning ? "" : " Press Enter to replay");

        private int _starCounter = 30;
        private int _powerModeCounter = 200;
        private double _score;

        public Game()
        {
            Player = new Player();
            Cars = new[] { new Car(), new Car() };
            Speed = 2;
            IsRunning = true;
        }

        public void GameLoop()
        {
            _score += 0.05;
            Player.Move();
            var isStillAlive1 = UpdateCar(0);
            var isStillAlive2 = UpdateCar(1);
            UpdatePowerMode();
            UpdateStar();
            UpdateSpeed();
            IsRunning = isStillAlive1 && isStillAlive2;
        }

        private void UpdateSpeed()
        {
            var speedLevel = Math.Floor(_score / 10);
            Speed = speedLevel > 5 ? 4.4 : 2.0 + speedLevel * 0.4;
        }

        private bool UpdateCar(int index)
        {
            var car = Cars[index];
            car.Move(Speed);
            var isCrash = car.CrashesWith(Player);
            if (car.Y > 100 || isCrash && IsPowerMode)
            {
                car.Reset();
            }
            return IsPowerMode || !isCrash;
        }

        private void UpdatePowerMode()
        {
            if (!IsPowerMode) return;
            _powerModeCounter--;
            IsPowerMode = _powerModeCounter > 0;
        }

        private void UpdateStar()
        {
            if (Star != null)
            {
                var stillExists = Star.Move();
                var isStarCrash = Star.CrashesWith(Player);
                if (isStarCrash || !stillExists) Star = null;
                if (isStarCrash) PowerUp();
            }
            else if (--_starCounter < 1)
            {
                Star = new Star();
                _starCounter = MyRandom.Instance.Next(600, 900);
            }
        }

        private void PowerUp()
        {
            IsPowerMode = true;
            _powerModeCounter = 200;
        }
    }
}
