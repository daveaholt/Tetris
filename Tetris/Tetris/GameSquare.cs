using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class GameSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Thickness BorderWidth { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush BorderColor { get; set; }

        public GameSquare(int x, int y, int height, int width, Thickness borderWidth)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            BorderWidth = borderWidth;
            BackgroundColor = Brushes.White;
            BorderColor = Brushes.LightGray;
        }

        public GameSquare(int x, int y, int height, int width, Thickness borderWidth, SolidColorBrush background)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            BorderWidth = borderWidth;
            BackgroundColor = background;
            BorderColor = Brushes.LightGray;
        }

        public void UpdateColor(SolidColorBrush background)
        {
            BackgroundColor = background;
        }

        public void Draw(Canvas _board)
        {
            var _border = new Border
            {
                BorderThickness = BorderWidth,
                BorderBrush = BorderColor
            };            

            var _s = new Canvas
            {
                Background = Brushes.White,
                Height = Height,
                Width = Width
            };

            _border.Child = _s;
            _board.Children.Add(_border);
            Canvas.SetTop(_border, (Y * Height));
            Canvas.SetLeft(_border, (X * Width));

        }
    }
}
