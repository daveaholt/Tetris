using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class Instructions
    {
        public void Draw(Canvas _mainCanvas)
        {
            var title = new TextBlock
            {
                Text = "Instructions:",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 24
            };
            Canvas.SetLeft(title, 350);
            Canvas.SetTop(title, 250);
            _mainCanvas.Children.Add(title);


            var underline = new TextBlock
            {
                Text = "---------------",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 24
            };
            Canvas.SetLeft(underline, 350);
            Canvas.SetTop(underline, 260);
            _mainCanvas.Children.Add(underline);


            var pause = new TextBlock
            {
                Text = "Pause Game:  P",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(pause, 350);
            Canvas.SetTop(pause, 290);
            _mainCanvas.Children.Add(pause);


            var reset = new TextBlock
            {
                Text = "Reset Game:  R",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(reset, 350);
            Canvas.SetTop(reset, 320);
            _mainCanvas.Children.Add(reset);


            var up = new TextBlock
            {
                Text = "Rotate: ↑",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(up, 350);
            Canvas.SetTop(up, 350);
            _mainCanvas.Children.Add(up);


            var down = new TextBlock
            {
                Text = "Send To Bottom: ↓",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(down, 350);
            Canvas.SetTop(down, 380);
            _mainCanvas.Children.Add(down);


            var left = new TextBlock
            {
                Text = "Move left:   ←",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(left, 350);
            Canvas.SetTop(left, 410);
            _mainCanvas.Children.Add(left);


            var right = new TextBlock
            {
                Text = "Move right:   →",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(right, 350);
            Canvas.SetTop(right, 440);
            _mainCanvas.Children.Add(right);


            var quit = new TextBlock
            {
                Text = "Quit Game:  (esc)",
                Foreground = Brushes.DarkSlateGray,
                FontSize = 20
            };
            Canvas.SetLeft(quit, 350);
            Canvas.SetTop(quit, 470);
            _mainCanvas.Children.Add(quit);
        }
    }
}
