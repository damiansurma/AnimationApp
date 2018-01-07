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
using System.Threading;


namespace AnimationApp
{
    public partial class MainWindow : Window
    {
        
        BaseOperation operation;

        int currentNoOfClicks;
        Point p1, p2;
        CancellationTokenSource cts;



        public MainWindow()
        {
            InitializeComponent();

            currentNoOfClicks = 0;
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
                initDrawing();
            }


            Point startPoint = e.GetPosition(paintCanvas);
            Console.WriteLine("current mouse potiion: {0} : {1}", startPoint.X, startPoint.Y);
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }


        /// <summary>
        /// Keeps track of number of mouse clicks on the painting area.
        /// </summary>
        public void initDrawing()
        {

            Console.WriteLine("mouse click {0}", currentNoOfClicks);

            switch (currentNoOfClicks)
            {
                case 0:
                    p1 = Mouse.GetPosition(paintCanvas);
                    currentNoOfClicks++;

                    cts = new CancellationTokenSource();

                    draw();
                    break;
                case 1:
                    currentNoOfClicks = 0;
                    p2 = Mouse.GetPosition(paintCanvas);
                    cts.Cancel();
                    break;
            }
        }


        /// <summary>
        /// Performs drawing on the canvas depending what the operation variable is pointing at.
        /// </summary>
        private async void draw()
        {
            bool firstRun = true;   // prevents removing canvas child on the first run of the loop (nothing to remove yet)

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
                    // remove last child in canvas
                    int i = paintCanvas.Children.Count - 1;
                    paintCanvas.Children.RemoveAt(i);
                }

                if (paintCanvas.IsMouseOver)
                {
                    p2 = Mouse.GetPosition(paintCanvas);
                }

                operation.draw(paintCanvas, p1, p2);

                await Task.Delay(50);
            }


        }



    }
}
