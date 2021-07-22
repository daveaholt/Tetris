using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Tetris
{
    public class TetrisGame
    {
        private const int _squareHeight = 25;
        private const int _squareWidth = 25;
        private Thickness _squareBorderWidth = new Thickness(1);

        private Canvas _mainCanvas;
        private GameBoard _board;
        private PreviewPane _pane;


        public bool IsRunning { get; set; }

        public void Start(Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            Reset();
        }

        public void Draw()
        {
            _mainCanvas.Children.Clear();
            _board.Draw(_mainCanvas);
            _pane.Draw(_mainCanvas);
        }

        public void Update()
        {
            //TODO: Update game.
        }

        private void ResetGameBoard()
        {
            _board = new GameBoard(_squareHeight, _squareWidth, _squareBorderWidth);
            _pane = new PreviewPane(_squareHeight, _squareWidth, _squareBorderWidth);
        }

        internal void DownPressed()
        {
            throw new NotImplementedException();
        }

        internal void UpPressed()
        {
            throw new NotImplementedException();
        }

        internal void RightPressed()
        {
            throw new NotImplementedException();
        }

        internal void LeftPressed()
        {
            throw new NotImplementedException();
        }

        internal void Reset()
        {
            ResetGameBoard();
            IsRunning = true;
        }
    }
}
