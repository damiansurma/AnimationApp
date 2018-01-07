using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace AnimationApp
{

    /// <summary>
    /// Drawing operation interface.
    /// </summary>
    interface BaseOperation
    {
        int CurrentNoOfClicks { get; set; }

        void initDrawing(Canvas _canvas, Point _point);
    }

    /// <summary>
    /// Responsibe for drawing a simple straight line.
    /// </summary>
    class DrawLine : BaseOperation
    {
        CancellationTokenSource cts;

        public int CurrentNoOfClicks { get; set; }
        Point p1, p2;


        public DrawLine()
        {
            CurrentNoOfClicks = 0;

        }

        public void initDrawing(Canvas _canvas, Point _point)
        {
            switch (CurrentNoOfClicks)
            {
                case 0:
                    p1 = _point;
                    CurrentNoOfClicks++;

                    cts = new CancellationTokenSource();

                    draw(_canvas);
                    break;
                case 1:
                    CurrentNoOfClicks = 0;
                    p2 = _point;
                    cts.Cancel();
                    break;
            }
        }

        private async void draw(Canvas _canvas)
        {
            bool firstRun = true;

            while (true)
            {
                if (cts.IsCancellationRequested) break;


                // by byla linia stworzona przez peview do usuniecia
                if (firstRun)
                {
                    firstRun = false;
                }
                else
                {
                    // usuniecie ostatniego potomka w canvas
                    int i = _canvas.Children.Count - 1;
                    _canvas.Children.RemoveAt(i);
                }


                Line line = new Line();

                if (_canvas.IsMouseOver)
                {
                    p2 = Mouse.GetPosition(_canvas);
                }


                line.Stroke = SystemColors.WindowFrameBrush;
                line.X1 = p1.X; line.Y1 = p1.Y;
                line.X2 = p2.X; line.Y2 = p2.Y;

                _canvas.Children.Add(line);


                await Task.Delay(50);
            }


        }


    }
}
