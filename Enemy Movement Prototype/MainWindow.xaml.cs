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
        private int turtleSpeedX = 7;
        public MainWindow()
        {
            InitializeComponent();
            turtleX = (int)Canvas.GetLeft(turtleBackImage);//sets turtleX to current position on XAML code which is 0
            int framesPerSecond = 90;
            DispatcherTimer update = new DispatcherTimer();           
            update.Tick += Update_Tick;
            update.Interval = TimeSpan.FromMilliseconds(1000 / framesPerSecond);    
            update.Start();
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            moveTurtle();
        }

        private void moveTurtle()
        {
            turtleX = turtleX + turtleSpeedX;//adds 5 to turtleX
            if (turtleX > gameCanvas.Width - turtleBackImage.Width)//if turtleX is greater than 768(second part of condition) will fire
            {
                turtleSpeedX *= -1;
            }
            if (turtleX < 0)
            {
                turtleSpeedX *= -1;
            }
            Canvas.SetLeft(turtleBackImage, turtleX);//sets X coordinate for turtle image
        }
    }
}
