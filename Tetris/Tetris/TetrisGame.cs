using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Tetris.Tetrads;

namespace Tetris
{
    public class TetrisGame
    {
        private const int ROWS = 21;
        private const int COLUMNS = 10;
        private const int _squareHeight = 25;
        private const int _squareWidth = 25;
        private Thickness _squareBorderWidth = new Thickness(1);

        private long _score;

        private Canvas _mainCanvas;
        private GameBoard _board;
        private PreviewPane _pane;
        private Instructions _instructions;

        private Tetrad _activeTetrad;
        private Tetrad _onDeckTetrad;
        private List<GameSquare> _gluedSquares;

        private List<char> _tetradTypes;

        private DispatcherTimer _dispatcherTimer;
        private MediaPlayer _mediaPlayer;

        public bool IsRunning { get; set; }
        public bool IsOver { get; set; }

        public void Start(Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            _tetradTypes = new List<char>();
            _gluedSquares = new List<GameSquare>();
            _tetradTypes.Add('O');
            _tetradTypes.Add('I');
            _tetradTypes.Add('J');
            _tetradTypes.Add('L');
            _tetradTypes.Add('S');
            _tetradTypes.Add('Z');
            Reset();
        }

        public void Draw()
        {
            _mainCanvas.Children.Clear();
            _board.Draw(_mainCanvas);
            _pane.Draw(_mainCanvas);
            _instructions.Draw(_mainCanvas);

            _onDeckTetrad.DrawToPane(_pane);
            _activeTetrad.DrawToBoard(_board);
            foreach (var t in _gluedSquares)
            {
                t.Draw(_board.GetCanvas());
            }

            var title = new TextBlock
            {
                Text = $"Score:   {_score}",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 24
            };
            Canvas.SetLeft(title, 60);
            Canvas.SetTop(title, 10);
            _mainCanvas.Children.Add(title);

            if (IsOver)
            {
                var textBlock = new TextBlock
                {
                    Text = "Game Over",
                    Foreground = Brushes.IndianRed,
                    FontSize = 50
                };
                Canvas.SetLeft(textBlock, 100);
                Canvas.SetTop(textBlock, 200);
                _mainCanvas.Children.Add(textBlock);
            }
        }

        public void DownPressed()
        {
            DropOne();
        }

        public void UpPressed()
        {
            _activeTetrad.Rotate();
        }

        public void RightPressed()
        {
            if (!_activeTetrad.CheckCollisionRight(_gluedSquares))
            {                
                _activeTetrad.MoveRight();
            }
        }

        public void LeftPressed() 
        {
            if (!_activeTetrad.CheckCollisionLeft(_gluedSquares))
            {
                _activeTetrad.MoveLeft();
            }
        }

        public void TogglePause()
        {
            IsRunning = !IsRunning;
            if (!IsRunning)
            {
                var textBlock = new TextBlock
                {
                    Text = "Paused",
                    Foreground = Brushes.IndianRed,
                    FontSize = 50
                };
                Canvas.SetLeft(textBlock, 100);
                Canvas.SetTop(textBlock, 200);
                _mainCanvas.Children.Add(textBlock);
                StartDispatcher();
            }
            else
            {
                _dispatcherTimer.Stop();
            }
        }

        public void Reset()
        {
            ResetGameBoard();
            IsOver = false;
            IsRunning = true;
            StartDispatcher();
        }

        private void StartDispatcher()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += Timer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DropOne();
            CheckForCompleatedRows();
        }

        private void CheckForCompleatedRows()
        {
            var completeRows = new List<int>();
            var groupedStuff = _gluedSquares.GroupBy(sq => sq.Y);
            foreach (var group in groupedStuff.AsEnumerable())
            {
                if(group.ToList().Count == COLUMNS)
                {
                    completeRows.Add(group.Key);
                }
            }
            _gluedSquares.RemoveAll(s => completeRows.Any(r => r == s.Y));            

            foreach (var row in completeRows.OrderBy(x => x))
            {
                foreach (var s in _gluedSquares)
                {
                    if(s.Y < row)
                    {
                        s.Y++;
                    }
                }
            }
            switch (completeRows.Count)
            {
                case 1:
                    _score += 40;
                    SystemSounds.Asterisk.Play();
                    break;
                case 2:
                    _score += 100;
                    SystemSounds.Exclamation.Play();
                    break;
                case 3:
                    _score += 300;
                    SystemSounds.Exclamation.Play();
                    break;
                case 4:
                    _score += 1200;
                    SystemSounds.Exclamation.Play();
                    break;
                default:
                    break;
            }
            Draw();
        }

        private void DropOne()
        {
            if (_activeTetrad.ReachedBottom())
            {
                _gluedSquares.AddRange(_activeTetrad.GetSquares());
                AddTetrad();
            }

            if (_activeTetrad.CheckDownCollision(_gluedSquares))
            {
                _gluedSquares.AddRange(_activeTetrad.GetSquares());
                AddTetrad();
            }
            _activeTetrad.DropOne();
        }

        private void ResetGameBoard()
        {
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
            _mediaPlayer.Open(new Uri("music/tetris-gameboy-02.mp3", UriKind.Relative));
            _mediaPlayer.Play();

            _board = new GameBoard(_squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
            _pane = new PreviewPane(_squareHeight, _squareWidth, _squareBorderWidth);
            _instructions = new Instructions();
            _gluedSquares = new List<GameSquare>();
            _score = 0;

            AddTetrad();
            AddTetrad();
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            _mediaPlayer.Position = TimeSpan.Zero;
            _mediaPlayer.Play();
        }

        private void AddTetrad()
        {
            var rand = new Random();
            var index = rand.Next(_tetradTypes.Count);
            var typeToCreate = _tetradTypes[index];

            if (_onDeckTetrad == null)
            {
                _onDeckTetrad = CreateTetrad(typeToCreate);

                index = rand.Next(_tetradTypes.Count);
                typeToCreate = _tetradTypes[index];
                _activeTetrad = CreateTetrad(typeToCreate);
                _activeTetrad.RelocateToBoard();
            }
            else
            {
                _activeTetrad = _onDeckTetrad;
                _activeTetrad.RelocateToBoard();

                _onDeckTetrad = CreateTetrad(typeToCreate);
            }

            foreach (var s in _activeTetrad.GetSquares())
            {
                if(_gluedSquares.Any(x => x.X == s.X && x.Y == s.Y))
                {
                    IsOver = true;                    
                    _mediaPlayer.Stop();

                    var failPlayer = new MediaPlayer();
                    failPlayer.Open(new Uri("music/fail.wav", UriKind.Relative));
                    failPlayer.Play();

                    _dispatcherTimer.Stop();
                    var textBlock = new TextBlock
                    {
                        Text = "Game Over",
                        Foreground = Brushes.IndianRed,
                        FontSize = 50
                    };
                    Canvas.SetLeft(textBlock, 100);
                    Canvas.SetTop(textBlock, 200);
                    _mainCanvas.Children.Add(textBlock);
                }
            }
        }

        private Tetrad CreateTetrad(char c)
        {
            switch (c)
            {
                case 'O':
                    return new O(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
                case 'I':
                    return new I(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
                case 'J':
                    return new J(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
                case 'L':
                    return new L(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
                case 'S':
                    return new S(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
                case 'Z':
                    return new Z(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
            }
            return new O(new System.Drawing.Point(0, 0), _squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
        }
    }
}
