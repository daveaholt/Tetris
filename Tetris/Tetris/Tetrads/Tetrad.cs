using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    public abstract class Tetrad
    {
        protected List<GameSquare> _shape;
        protected int _squareHeight;
        protected int _squareWidth;
        protected Thickness _squareBorderWidth;
        protected int _rows;
        protected int _columns;

        public bool IsGlued { get; set; }

        public bool IsOnDeck { get; set; }

        public Tetrad(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns)
        {
            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
            _squareBorderWidth = squareBorderWidth;
            _rows = rows;
            _columns = columns;

            _shape = new List<GameSquare>();
            IsGlued = false;
            Build(location);

            if(location.X == 0 && location.Y == 0)
            {
                IsOnDeck = true;
            }
            else
            {
                IsOnDeck = false;
            }
        }

        public abstract void Rotate();

        public abstract void Build(System.Drawing.Point location);

        public void RelocateToBoard()
        {
            foreach (var s in _shape)
            {
                s.X += 4;
                s.Y += 1;
            }
            IsOnDeck = false;
        }

        public void MoveLeft()
        {
            if (!_shape.Any(x => x.X -1 < 0))
            {
                foreach (var s in _shape)
                {
                    s.X -= 1;
                }
            }
        }

        internal void DrawToPane(PreviewPane pane)
        {
            foreach (var s in _shape)
            {
                var _border = new Border
                {
                    BorderThickness = _squareBorderWidth,
                    BorderBrush = Brushes.LightGray
                };

                var _s = new Canvas
                {
                    Background = s.BackgroundColor,
                    Height = _squareHeight,
                    Width = _squareWidth
                };

                _border.Child = _s;
                pane.GetCanvas().Children.Add(_border);
                Canvas.SetTop(_border, (_squareHeight * 2) + (s.Y * _squareHeight));
                Canvas.SetLeft(_border, (_squareWidth * 2) + (s.X * _squareWidth));
            }
        }

        internal void DrawToBoard(GameBoard board)
        {
            foreach (var s in _shape)
            {
                if (s.Y < 0)
                {
                    continue;
                }

                var _border = new Border
                {
                    BorderThickness = _squareBorderWidth,
                    BorderBrush = Brushes.LightGray
                };

                var _s = new Canvas
                {
                    Background = s.BackgroundColor,
                    Height = _squareHeight,
                    Width = _squareWidth
                };

                _border.Child = _s;
                board.GetCanvas().Children.Add(_border);
                Canvas.SetTop(_border, (s.Y * _squareHeight));
                Canvas.SetLeft(_border, (s.X * _squareWidth));
            }
        }

        internal IEnumerable<GameSquare> GetSquares()
        {
            return _shape;
        }

        internal bool CheckCollisionRight(List<GameSquare> gluedSquares)
        {
            foreach (var s in _shape)
            {
                if (gluedSquares.Any(x => x.Y == s.Y && x.X == s.X + 1))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool CheckCollisionLeft(List<GameSquare> gluedSquares)
        {
            foreach (var s in _shape)
            {
                if (gluedSquares.Any(x => x.Y == s.Y && x.X == s.X - 1))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool CheckDownCollision(List<GameSquare> gluedSquares)
        {
            foreach (var s in _shape)
            {
                if (gluedSquares.Any(x => x.X == s.X && x.Y == s.Y + 1))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool ReachedBottom()
        {
            return _shape.Any(x => x.Y + 1 == _rows);
        }

        public void MoveRight()
        {
            if(!_shape.Any(x => x.X + 1 == _columns))
            {
                foreach (var s in _shape)
                {
                    s.X += 1;
                }
            }            
        }

        public void DropOne()
        {
            foreach (var s in _shape)
            {
                s.Y += 1;
            }
        }
    }
}
