using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class PreviewPane
    {
        private const int ROWS = 6;
        private const int COLUMNS = 6;

        private Canvas _pane;
        private List<GameSquare> _squares;

        private int _squareHeight;
        private int _squareWidth;
        private Thickness _squareBorderWidth;

        public PreviewPane(int squareHeight, int squareWidth, Thickness squareBorderWidth)
        {
            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
            _squareBorderWidth = squareBorderWidth;
        }

        public Canvas GetCanvas()
        {
            return _pane;
        }

        public void Draw(Canvas _mainCanvas)
        {
            var boardWidth = _squareWidth * COLUMNS;
            var boardHeight = _squareHeight * ROWS;

            _squares = new List<GameSquare>();

            _pane = new Canvas
            {
                Background = Brushes.White,
                Height = boardHeight,
                Width = boardWidth
            };

            Canvas.SetTop(_pane, 50);
            Canvas.SetLeft(_pane, 350);
            _mainCanvas.Children.Add(_pane);

            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLUMNS; x++)
                {
                    var s = new GameSquare(x, y, _squareHeight, _squareWidth, _squareBorderWidth);
                    _squares.Add(s);
                    s.Draw(_pane);
                }
            }
        }
    }
}
