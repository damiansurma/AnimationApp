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
        
        void draw(Canvas _canvas, params object[] list);
    }

    /// <summary>
    /// Responsibe for drawing a simple straight line.
    /// </summary>
    class DrawLine : BaseOperation
    {
       
        public async void draw(Canvas _canvas, params object[] list)
        {
                Line line = new Line();
   
                line.Stroke = SystemColors.WindowFrameBrush;

                line.X1 = ((Point)list[0]).X;
                line.Y1 = ((Point)list[0]).Y;
                line.X2 = ((Point)list[1]).X;
                line.Y2 = ((Point)list[1]).Y;

                _canvas.Children.Add(line);

        } // drawline method
    } // class

    class DrawRectangle : BaseOperation
    {

        public async void draw(Canvas _canvas, params object[] list)
        {
            Rectangle rect = new Rectangle();
            double x1, x2, y1, y2;
            Point start = new Point();

            // tmp values to make later calculations more clear
            x1 = ((Point)list[0]).X;     x2 = ((Point)list[1]).X;
            y1 = ((Point)list[0]).Y;     y2 = ((Point)list[1]).Y;

            // width and height must be positives otherwise crash
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);

            // starting point from which to draw the rectangle
            start.X = (x2 > x1) ? x1 : x2;
            start.Y = (y2 > y1) ? y1 : y2;

            rect.Stroke = SystemColors.WindowFrameBrush;

            Canvas.SetLeft(rect, start.X);
            Canvas.SetTop(rect, start.Y);

            rect.Width = width;
            rect.Height = height;
            

            _canvas.Children.Add(rect);

        } // draw rect method
    } // class


    class DrawEllipse : BaseOperation
    {

        public async void draw(Canvas _canvas, params object[] list)
        {
            Ellipse el = new Ellipse();

            double x1, x2, y1, y2;
            Point start = new Point();

            // tmp values to make later calculations more clear
            x1 = ((Point)list[0]).X; x2 = ((Point)list[1]).X;
            y1 = ((Point)list[0]).Y; y2 = ((Point)list[1]).Y;

            // width and height must be positives otherwise crash
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);

            // starting point from which to draw the rectangle
            start.X = (x2 > x1) ? x1 : x2;
            start.Y = (y2 > y1) ? y1 : y2;

            el.Stroke = SystemColors.WindowFrameBrush;

            Canvas.SetLeft(el, start.X);
            Canvas.SetTop(el, start.Y);

            el.Width = width;
            el.Height = height;

            _canvas.Children.Add(el);
        }
    }



} // namespace
