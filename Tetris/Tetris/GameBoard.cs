using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    class GameBoard
    {           

        private Canvas _board;
        private List<GameSquare> _squares;
        private int _squareHeight;
        private int _squareWidth;
        private Thickness _squareBorderWidth;
        private int _rows;
        private int _columns;

        public GameBoard(int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns)
        {
            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
            _squareBorderWidth = squareBorderWidth;
            _rows = rows;
            _columns = columns;
        }

        public Canvas GetCanvas()
        {
            return _board;
        }

        public void Draw(Canvas _mainCanvas)
        {
            var boardWidth = _squareWidth * _columns;
            var boardHeight = _squareHeight * _rows;

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

            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    var s = new GameSquare(x, y, _squareHeight, _squareWidth, _squareBorderWidth);
                    _squares.Add(s);
                    s.Draw(_board);
                }
            }
        }
    }
}
