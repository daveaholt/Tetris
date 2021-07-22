using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (_game.IsRunning && _stopWatch.ElapsedMilliseconds > FPS_LIMIT)
            {
                _stopWatch.Restart();
                _game.Update();
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
                case Key.S:
                    _game.IsRunning = false;
                    break;
                case Key.R:
                    _game.Reset();
                    break;
                case Key.Down:
                    _game.DownPressed();
                    break;
                case Key.Up:
                    _game.UpPressed();
                    break;
                case Key.Right:
                    _game.RightPressed();
                    break;
                case Key.Left:
                    _game.LeftPressed();
                    break;
            }
        }
    }
}
