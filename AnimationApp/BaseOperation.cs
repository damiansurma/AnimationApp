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



} // namespace
