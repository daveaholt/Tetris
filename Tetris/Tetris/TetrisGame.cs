using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private Canvas _mainCanvas;
        private GameBoard _board;
        private PreviewPane _pane;
        private Instructions _instructions;

        private Tetrad _activeTetrad;
        private Tetrad _onDeckTetrad;
        private List<GameSquare> _gluedSquares;

        private List<char> _tetradTypes;

        private DispatcherTimer _dispatcherTimer;

        public bool IsRunning { get; set; }

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

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += Timer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

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
        }

        public void DownPressed()
        {
            throw new NotImplementedException();
        }

        public void UpPressed()
        {
            _activeTetrad.Rotate();
        }

        public void RightPressed()
        {
            _activeTetrad.MoveRight();
        }

        public void LeftPressed()
        {
            _activeTetrad.MoveLeft();
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
                _dispatcherTimer.Start();
            }
            else
            {
                _dispatcherTimer.Stop();
            }
        }

        public void Reset()
        {
            ResetGameBoard();
            IsRunning = true;
            _dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _activeTetrad.DropOne();
        }

        private void ResetGameBoard()
        {
            _board = new GameBoard(_squareHeight, _squareWidth, _squareBorderWidth, ROWS, COLUMNS);
            _pane = new PreviewPane(_squareHeight, _squareWidth, _squareBorderWidth);
            _instructions = new Instructions();
            AddTetrad();
            AddTetrad();
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
