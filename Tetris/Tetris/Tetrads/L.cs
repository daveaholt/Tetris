using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    public class L : Tetrad
    {
        private int _rotatePhase;

        public L(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns) : base(location, squareHeight, squareWidth, squareBorderWidth, rows, columns)
        {
            _rotatePhase = 0;
        }

        public override void Build(System.Drawing.Point location)
        {
            _shape.Add(new GameSquare(location.X, location.Y - 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Orange));
            _shape.Add(new GameSquare(location.X, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Orange));
            _shape.Add(new GameSquare(location.X, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Orange));
            _shape.Add(new GameSquare(location.X + 1, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Orange));
        }

        public override void Rotate()
        {
            if (_rotatePhase == 0)
            {
                _shape[0].X += 1;
                _shape[0].Y += 1;

                _shape[2].X -= 1;
                _shape[2].Y -= 1;

                _shape[3].X -= 2;
            }
            else if(_rotatePhase == 1)
            {
                _shape[0].X -= 1;
                _shape[0].Y += 1;

                _shape[2].X += 1;
                _shape[2].Y -= 1;

                _shape[3].Y -= 2;
            } 
            else if (_rotatePhase == 2)
            {
                _shape[0].X -= 1;
                _shape[0].Y -= 1;

                _shape[2].X += 1;
                _shape[2].Y += 1;

                _shape[3].X += 2;
            } 
            else if (_rotatePhase == 3)
            {
                _shape[0].X += 1;
                _shape[0].Y -= 1;

                _shape[2].X -= 1;
                _shape[2].Y += 1;

                _shape[3].Y += 2;
            }

            _rotatePhase++;
            if(_rotatePhase > 3)
            {
                _rotatePhase = 0;
            }
        }
    }
}
