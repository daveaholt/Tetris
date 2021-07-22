using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    class GameBoard
    {
        private const int ROWS = 21;
        private const int COLUMNS = 10;              

        private Canvas _board;
        private List<GameSquare> _squares;
        private int _squareHeight;
        private int _squareWidth;
        private Thickness _squareBorderWidth;

        public GameBoard(int squareHeight, int squareWidth, Thickness squareBorderWidth)
        {
            this._squareHeight = squareHeight;
            this._squareWidth = squareWidth;
            this._squareBorderWidth = squareBorderWidth;
        }

        public void Draw(Canvas _mainCanvas)
        {
            var boardWidth = _squareWidth * COLUMNS;
            var boardHeight = _squareHeight * ROWS;

            _squares = new List<GameSquare>();

            _board = new Canvas
            {
                Background = Brushes.White,
                Height = boardHeight,
                Width = boardWidth
            };
            Canvas.SetTop(_board, 50);
            Canvas.SetLeft(_board, 50);
            _mainCanvas.Children.Add(_board);

            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLUMNS; x++)
                {
                    var s = new GameSquare(x, y, _squareHeight, _squareWidth, _squareBorderWidth);
                    _squares.Add(s);
                    s.Draw(_board);
                }
            }
        }
    }
}
