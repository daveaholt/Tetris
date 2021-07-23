using System.Windows;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    public class I : Tetrad
    {
        private bool _isHorizantal;

        public I(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns) : base(location, squareHeight, squareWidth, squareBorderWidth, rows, columns)
        {
            _isHorizantal = true;
        }

        public override void Build(System.Drawing.Point location)
        {
            _shape.Add(new GameSquare(location.X - 1, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.CornflowerBlue));
            _shape.Add(new GameSquare(location.X, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.CornflowerBlue));
            _shape.Add(new GameSquare(location.X + 1, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.CornflowerBlue));
            _shape.Add(new GameSquare(location.X + 2, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.CornflowerBlue));
        }

        public override void Rotate()
        {
            int newX;
            int newY;
            if (_isHorizantal)
            {
                newX = _shape[0].X + 2;
                newY = _shape[0].Y - 2;
                _shape[0].X = newX;
                _shape[0].Y = newY;
                for (int i = 1; i < _shape.Count; i++)
                {
                    newY += 1;
                    _shape[i].X = newX;
                    _shape[i].Y = newY;
                }
            }
            else
            {
                newX = _shape[0].X - 2;
                newY = _shape[0].Y + 2;
                _shape[0].X = newX;
                _shape[0].Y = newY;

                for (int i = 1; i < _shape.Count; i++)
                {
                    newX += 1;
                    _shape[i].X = newX;
                    _shape[i].Y = newY;
                }
            }
            _isHorizantal = !_isHorizantal;
        }
    }
}
