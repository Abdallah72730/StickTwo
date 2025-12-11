using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StickTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StickFigure stickFigure;
        private DispatcherTimer timer;
        private bool isRightPressed;
        private bool isLeftPressed;
        private bool isUpPressed;


        private double gravity = 0.5;
        private double groundLevel = 400;
        private double jumpStrength = -10;
        private double moveSpeed = 5;


        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            stickFigure = new StickFigure(GameCanvas, 100, 100);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            timer.Tick += GameLoop;
            timer.Start();
        }

        public void GameLoop(object sender, EventArgs e)
        {
            HandleInput();
            ApplyPhysics();
            stickFigure.Update();
            CheckCollisions();
        }

        public void HandleInput()
        {
            if (isRightPressed)
            {
                stickFigure.VelocityX = moveSpeed;
            }
            else if (isLeftPressed)
            {
                stickFigure.VelocityX = -moveSpeed;
            }
            else
            {
                stickFigure.VelocityX = 0;
            }
            if (isUpPressed && stickFigure.Y >= groundLevel)
            {
                stickFigure.VelocityY = jumpStrength;
                isUpPressed = false; // Prevent double jump
            }
        }

        public void ApplyPhysics()
        {
            stickFigure.VelocityY += gravity;
        }

        public void CheckCollisions()
        {
            if (stickFigure.Y >= groundLevel)
            {
                stickFigure.Y = groundLevel;
                stickFigure.VelocityY = 0;
            }
            if (stickFigure.X < 20) 
            {
                stickFigure.X = 20;
            }
            if (stickFigure.X > GameCanvas.ActualWidth-20) 
            {
                stickFigure.X = GameCanvas.ActualWidth-20 ;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                isRightPressed = true;
            }
            else if (e.Key == Key.Left)
            {
                isLeftPressed = true;
            }
            else if (e.Key == Key.Up)
            {
                isUpPressed = true;
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                isRightPressed = false;
            }
            else if (e.Key == Key.Left)
            {
                isLeftPressed = false;
            }
        }




    }
}