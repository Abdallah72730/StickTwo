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
        private double groundLevel = 360;
        private double jumpStrength = -10;
        private double moveSpeed = 8.5;


        //Environment
        private Rectangle ground;
        private Rectangle sky;
        private List<Cloud> clouds;
        private double cameraOffsetX = 0;
        private Rectangle grass;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            CreateEnvironment();
            stickFigure = new StickFigure(GameCanvas, 100, 100);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(8); // ~60 FPS
            timer.Tick += GameLoop;
            timer.Start();
        }

        public void GameLoop(object sender, EventArgs e)
        {
            HandleInput();
            ApplyPhysics();
            stickFigure.Update();
            CheckCollisions();
            UpdateCamera();
            stickFigure.UpdatePosition(cameraOffsetX);
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
            if (stickFigure.X > 3000-20) 
            {
                stickFigure.X = 3000-20 ;
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

        public void CreateEnvironment()
        {
            sky = new Rectangle
            {
                Width = GameCanvas.Width,
                Height = GameCanvas.Height,
                Fill = new LinearGradientBrush(
                    Color.FromRgb(135, 206, 235), // Light blue
                    Color.FromRgb(176, 224, 230), // Lighter blue
                    90)
            };
            Canvas.SetLeft(sky, 0);
            Canvas.SetTop(sky, 0);
            GameCanvas.Children.Add(sky);

            //create clouds
            clouds = new List<Cloud>();
            clouds.Add(new Cloud(GameCanvas, 100, 80, 60));
            clouds.Add(new Cloud(GameCanvas, 300, 120, 50));
            clouds.Add(new Cloud(GameCanvas, 600, 60, 70));
            clouds.Add(new Cloud(GameCanvas, 900, 100, 55));
            clouds.Add(new Cloud(GameCanvas, 1200, 90, 65));

            //ground
            ground = new Rectangle 
            {
                Width = 3000,
                Height = 150,
                Fill = new SolidColorBrush(Color.FromRgb(139, 69, 19))
            };
            Canvas.SetLeft(ground, 0);
            Canvas.SetTop(ground, groundLevel + 90);
            GameCanvas.Children.Add(ground);

            grass = new Rectangle
            {
                Width = 3000,
                Height = 50,
                Fill = new SolidColorBrush(Color.FromRgb(34, 139, 34))
            };
            Canvas.SetLeft(grass, 0);
            Canvas.SetTop(grass, groundLevel + 90);
            GameCanvas.Children.Add(grass);

        }

        public void UpdateCamera() 
        {
            if (stickFigure.X > 450)
            {
                cameraOffsetX = stickFigure.X - 450;
            }
            else 
            {
                cameraOffsetX = 0;
            }
            Canvas.SetLeft(ground, -cameraOffsetX);
            Canvas.SetLeft(grass, -cameraOffsetX);

            foreach (var cloud in clouds) 
            {
                cloud.update(-cameraOffsetX * 0.3);
            }
        }
    }
}