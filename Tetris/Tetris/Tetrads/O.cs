using System.Windows;
using System.Windows.Media;

namespace Tetris.Tetrads
{
    public class O : Tetrad
    {
        public O(System.Drawing.Point location, int squareHeight, int squareWidth, Thickness squareBorderWidth, int rows, int columns) : base(location, squareHeight, squareWidth, squareBorderWidth, rows, columns)
        {
        }

        public override void Build(System.Drawing.Point location)
        {
            _shape.Add(new GameSquare(location.X, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Yellow));
            _shape.Add(new GameSquare(location.X + 1, location.Y, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Yellow));
            _shape.Add(new GameSquare(location.X, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Yellow));
            _shape.Add(new GameSquare(location.X + 1, location.Y + 1, _squareHeight, _squareWidth, _squareBorderWidth, Brushes.Yellow));
        }

        public override void Rotate()
        {
            //This is basically unnecessary for the O shape
        }
    }
}
