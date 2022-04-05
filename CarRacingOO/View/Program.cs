using System;
using System.Windows;

namespace CarRacingOO.View;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var application = new Application();
        var gameWindow = new GameWindow();
        application.Run(gameWindow);
    }
}