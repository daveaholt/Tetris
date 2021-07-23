using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    class Z : Tetrad
    {
        private bool _isHorizantal;

        public Z(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns) : base(location, squareHeight, squareWidth, squareBorderWidth, rows, columns)
        {
            _isHorizantal = true;
        }

        public override void Build(System.Drawing.Point location)
        {
            _shape.Add(new GameSquare(location.X, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.IndianRed));
            _shape.Add(new GameSquare(location.X + 1, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.IndianRed));
            _shape.Add(new GameSquare(location.X + 1, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.IndianRed));
            _shape.Add(new GameSquare(location.X + 2, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.IndianRed));
        }

        public override void Rotate()
        {
            if (_isHorizantal)
            {
                _shape[0].X += 2;

                _shape[1].X += 1;
                _shape[1].Y += 1;


                _shape[3].X -= 1;
                _shape[3].Y += 1;
            }
            else
            {
                _shape[0].X -= 2;

                _shape[1].X -= 1;
                _shape[1].Y -= 1;


                _shape[3].X += 1;
                _shape[3].Y -= 1;
            }
            _isHorizantal = !_isHorizantal;
        }
    }
}
