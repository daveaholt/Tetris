using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int FPS_LIMIT = 1000 / 60;

        private Stopwatch _stopWatch;
        private TetrisGame _game;

        public MainWindow()
        {
            InitializeComponent();

            _game = new TetrisGame();
            _stopWatch = new Stopwatch();

            _game.Start(this.MainCanvas);
            CompositionTarget.Rendering += Loop;
            _stopWatch.Start();
        }


        private void Loop(object sender, EventArgs e)
        {
            if (_game.IsRunning && !_game.IsOver && _stopWatch.ElapsedMilliseconds > FPS_LIMIT)
            {
                _stopWatch.Restart();
                _game.Draw();
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    _game.IsRunning = false;
                    Close();
                    break;
                case Key.P:
                    if(!_game.IsOver)
                    {
                        _game.TogglePause();
                    }
                    break;
                case Key.R:
                    _game.Reset();
                    break;
                case Key.Down:
                    if (_game.IsRunning)
                    {
                        _game.DownPressed();
                    }
                    break;
                case Key.Up:
                    if (_game.IsRunning)
                    {
                        _game.UpPressed();
                    }
                    break;
                case Key.Right:
                    if (_game.IsRunning)
                    {
                        _game.RightPressed();
                    }
                    break;
                case Key.Left:
                    if (_game.IsRunning)
                    {
                        _game.LeftPressed();
                    }
                    break;
            }
        }
    }
}
