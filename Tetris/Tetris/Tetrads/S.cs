using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    class S : Tetrad
    {
        private bool _isHorizantal;

        public S(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns) : base(location, squareHeight, squareWidth, squareBorderWidth, rows, columns)
        {
            _isHorizantal = true;
        }

        public override void Build(System.Drawing.Point location)
        {
            _shape.Add(new GameSquare(location.X + 1, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.DarkSeaGreen));
            _shape.Add(new GameSquare(location.X, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.DarkSeaGreen));
            _shape.Add(new GameSquare(location.X, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.DarkSeaGreen));
            _shape.Add(new GameSquare(location.X - 1, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.DarkSeaGreen));
        }

        public override void Rotate()
        {
            if (_isHorizantal)
            { 
                _shape[0].Y += 2;

                _shape[1].X += 1;
                _shape[1].Y += 1;


                _shape[3].X += 1;
                _shape[3].Y -= 1;
            }
            else
            {
                _shape[0].Y -= 2;

                _shape[1].X -= 1;
                _shape[1].Y -= 1;


                _shape[3].X -= 1;
                _shape[3].Y += 1;
            }
            _isHorizantal = !_isHorizantal;
        }
    }
}
