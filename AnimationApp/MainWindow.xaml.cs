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

namespace AnimationApp
{
    public partial class MainWindow : Window
    {
        

        BaseOperation operation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void lineButton_Click(object sender, RoutedEventArgs e)
        {
            // set up the proper option
            operation = new DrawLine();
        }



        /// <summary>
        /// Handles mouse clicks on drawing area canvas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paintCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (operation != null)
            {
                Console.WriteLine("drawing line");
                operation.initDrawing(paintCanvas);
            }

            Point startPoint = e.GetPosition(paintCanvas);
            Console.WriteLine("current mouse potiion: {0} : {1}", startPoint.X, startPoint.Y);
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }

    }
}
