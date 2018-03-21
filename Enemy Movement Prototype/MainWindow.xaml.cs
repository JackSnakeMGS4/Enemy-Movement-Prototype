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
using System.Xaml;
using System.Timers;
using System.Windows.Threading;

namespace Enemy_Movement_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int turtleX;
        private int turtleY;
        private int turtleSpeedX = 5;//sets x speed value
        private int turtleSpeedY = 5;//sets y speed value
        private DispatcherTimer update = new DispatcherTimer();//ensures the game constantly refreshes to ensure game is alive!
        private int framesPerSecond = 90;//this is how you can see the illusion of motion
    
        public MainWindow()
        {
            InitializeComponent();
            turtleX = (int)Canvas.GetLeft(turtleBackImage);//sets turtleX to current position on XAML code which is 384
            turtleY = (int)Canvas.GetTop(turtleBackImage);//sets turtleY to XAML value which is 184
            update.Tick += Update_Tick;//continued from 1, the problem was that multiple events handlers were being stacked leading to the speed increase bug upon reset
            update.Interval = TimeSpan.FromMilliseconds(1000 / framesPerSecond);
        }

        private void startEnemyMovement_Click(object sender, RoutedEventArgs e)
        {
            /* assigns event handler to update timer
             * sets the interval for ticks to occur (about 10 ticks per second)
             * starts timer and disables button to prevent bug where button can be clicked multiple times leading to warp speed
             * 1. fixed bug by removing event assignment from here and putting on the MainWindow()
             */
            update.Start();//start tick intervals
            startEnemyMovementButton.IsEnabled = false;
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            //constantly calls moveTurtle()
            moveTurtle();
        }

        private void moveTurtle()
        {
            turtleX = turtleX + turtleSpeedX;//adds 5 to turtleX
            turtleY = turtleY + turtleSpeedY;//adds 5 to turtleY;

            ///<summary>
            /// I want the image to bounce when multiple conditions are true which is why I used 4 if's 
            ///</summary>
            if (turtleX > gameCanvas.Width - turtleBackImage.Width)
            {
                //if turtleX is greater than 768(second part of condition) will fire and bounce the turtle to the left
                turtleSpeedX *= -1;
            }
            if (turtleX < 0)
            {
                //when fire will return the turtle to the right
                turtleSpeedX *= -1;
            }
            if (turtleY > gameCanvas.Height - turtleBackImage.Height)
            {
                //turn turtleY to a negative when it reaches the bottom
                turtleSpeedY *= -1;
            }
            if (turtleY < 0)
            {
                //turn turtleY to a negative when it reaches the top
                turtleSpeedY *= -1;
            }
            Canvas.SetLeft(turtleBackImage, turtleX);//sets X coordinate for turtle image
            Canvas.SetTop(turtleBackImage, turtleY);//sets Y coordinate for turtle image
        }
       
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            update.Stop();
            Canvas.SetLeft(turtleBackImage, (gameCanvas.Width / 2) - 16);
            Canvas.SetTop(turtleBackImage, (gameCanvas.Height / 2) - 16);
            turtleX = (int)Canvas.GetLeft(turtleBackImage);//resets turtleX to XAML value which is 384
            turtleY = (int)Canvas.GetTop(turtleBackImage);//resets turtleY to XAML value which is 184 
            startEnemyMovementButton.IsEnabled = true;
        }
    }
}
