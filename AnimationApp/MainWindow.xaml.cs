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
using System.Windows.Controls.Primitives;


namespace AnimationApp
{
    public partial class MainWindow : Window
    {       
        BaseOperation operation;

        int currentNoOfClicks;
        bool cancelDrawing;     // if we cancel drawing half-way thru, makes sure to erase the last child in canvas

        Point p1, p2;
        CancellationTokenSource cts;



        public MainWindow()
        {
            InitializeComponent();

            currentNoOfClicks = 0;
            cancelDrawing = false;


        }

        private void lineButton_Click(object sender, RoutedEventArgs e)
        {   
            if (lineButton.IsChecked == true)
            {
                if (operation != null)
                {
                    resetDrawing();
                }
                    

                operation = new DrawLine();
                cancelDrawing = false;
            } else
            {
                resetDrawing();
            }    
        }

        private void rectButton_Click(object sender, RoutedEventArgs e)
        {
            if (rectButton.IsChecked == true)
            {
                if (operation != null)
                {
                    resetDrawing();
                }


                operation = new DrawRectangle();
                cancelDrawing = false;
            }
            else
            {
                resetDrawing();
            }
        }

        private void ellipseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ellipseButton.IsChecked == true)
            {
                if (operation != null)
                {
                    resetDrawing();
                }

                operation = new DrawEllipse();
                cancelDrawing = false;
            }
            else
            {
                resetDrawing();
            }
        }


        private void removeLastDrawing_Button(object sender, RoutedEventArgs e)
        {
            removeLastChild();
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

            Console.WriteLine("Current no of clicks: {0}", currentNoOfClicks);

            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }


        /// <summary>
        /// Keeps track of number of mouse clicks on the painting area. Runs after clicking on the cavans area.
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
                    //p2 = Mouse.GetPosition(paintCanvas);
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
                if (cts.IsCancellationRequested)
                {

                    if (cancelDrawing == true)
                    {
                        //cancelDrawing = false;
                        removeLastChild();
                        cancelDrawing = false;
                    }
                        
                    break;
                }
                // by byla linia stworzona przez peview do usuniecia
                if (firstRun)
                {
                    firstRun = false;
                }
                else
                {
                    removeLastChild();
                }

                // makes sure the line wont get outside the canvas
                if (paintCanvas.IsMouseOver)
                {
                    p2 = Mouse.GetPosition(paintCanvas);
                }


                operation.draw(paintCanvas, p1, p2);

                await Task.Delay(50);
            }


        } // async draw

        // remove last child in canvas
        public void removeLastChild()
        {  
            int i = paintCanvas.Children.Count - 1;
            paintCanvas.Children.RemoveAt(i);
        }

        public void clearCanvas()
        {
            paintCanvas.Children.Clear();
        }


        // incase we switch drawing shapes mid-way thru drawing a shape
        public void resetDrawing()
        {
            currentNoOfClicks = 0;
            operation = null;
            cancelDrawing = true;
            cts.Cancel();
           
        }
    }
}
