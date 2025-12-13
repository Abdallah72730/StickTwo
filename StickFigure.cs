using System.Threading.Tasks.Sources;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StickTwo
{
    public class StickFigure
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        private Canvas container;
        private Canvas figureCanvas;   // <--- NEW: container for all body parts

        private Ellipse head;
        private Line body;
        private Line leftArm;
        private Line rightArm;
        private Line leftLeg;
        private Line rightLeg;

        private bool isMoving;
        private bool facingLeft;
        private int animationFrame;
        private double armSwing;
        double legSwing;
        bool isAttacking;
        bool attackProcessed;
        int attackFrame;
        int attackDuration = 10;
        int maxHealth = 100;
        int currentHealth;
        Rectangle healthBarBackground;
        Rectangle healthBarForeground;
        TextBlock healthText;




        public StickFigure(Canvas container, double startX, double startY)
        {
            this.container = container;
            X = startX;
            Y = startY;
            VelocityX = 0;
            VelocityY = 0;

            CreateBody();
            UpdatePosition(0);
        }

        private void CreateBody()
        {
            // Create the sub-canvas for the whole figure
            figureCanvas = new Canvas();
            container.Children.Add(figureCanvas);

            head = new Ellipse
            {
                Width = 20,
                Height = 20,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetLeft(head, -10);
            Canvas.SetTop(head, 0);
            figureCanvas.Children.Add(head);

            body = MakeLine(0, 20, 0, 60);
            leftArm = MakeLine(0, 30, -15, 50);
            rightArm = MakeLine(0, 30, 15, 50);
            leftLeg = MakeLine(0, 60, -15, 90);
            rightLeg = MakeLine(0, 60, 15, 90);

            figureCanvas.Children.Add(body);
            figureCanvas.Children.Add(leftArm);
            figureCanvas.Children.Add(rightArm);
            figureCanvas.Children.Add(leftLeg);
            figureCanvas.Children.Add(rightLeg);
        }

        private Line MakeLine(double x1, double y1, double x2, double y2)
        {
            return new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
        }

        public void Update()
        {
            X += VelocityX;
            Y += VelocityY;
        }

        public void UpdatePosition(double cameraOffsetX)
        {
            Canvas.SetLeft(figureCanvas, X - cameraOffsetX);
            Canvas.SetTop(figureCanvas, Y);
        }

        public void setMoving(bool moving, bool left) 
        {
            isMoving = moving;
            facingLeft = left;
        }

        public void Attack() 
        {
            if (!isAttacking) 
            {
                isAttacking = true;
                attackProcessed = false;
                attackFrame = 0;
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0) currentHealth = 0;
            UpdateHealthBar();
        }
        public void CreateHealthBar(Canvas figureCanvas) 
        {
            healthBarBackground = new Rectangle
            {
                Width = 60,
                Height = 8,
                Fill = Brushes.DarkRed
            };
            Canvas.SetLeft(healthBarBackground, -30);
            Canvas.SetTop(healthBarBackground, -20);

            healthBarForeground = new Rectangle
            {
                Width = 58,
                Height = 6,
                Fill = Brushes.LimeGreen
            };
            Canvas.SetLeft(healthBarForeground, -29);
            Canvas.SetTop(healthBarForeground, -19);

            healthText = new TextBlock
            {
                Text = $"{currentHealth}/{maxHealth}",
                FontSize = 8,
                Foreground = Brushes.White
            };

            Canvas.SetLeft(healthText, -15);
            Canvas.SetTop(healthText, -35);

            figureCanvas.Children.Add(healthBarBackground);
            figureCanvas.Children.Add(healthBarForeground);
            figureCanvas.Children.Add(healthText);
        }

        public void updateHealthBar()
        {
            double healthPercent = (double)currentHealth / maxHealth * 100;
            healthBarForeground.Width = healthPercent * 0.58;
            healthText.Text = $"{currentHealth}/{maxHealth}";
            if (healthPercent > 0.6)
            {
                healthBarForeground.Fill = Brushes.LimeGreen;
            }
            else if (healthPercent > 0.3)
            {
                healthBarForeground.Fill = Brushes.Orange;
            }
            else
            {
                healthBarForeground.Fill = Brushes.Red;
            }
        }

        //public void updateLimbPositions() 
        //{
        //    if (isAttacking) 
        //    {
        //        for (int i = 0; i <= attackFrame; i++) 
        //        {
        //            leftArm.X2 -= facingLeft ? 5 : 0;
        //            leftArm.Y2 -= 4;
        //        }
        //    }
        //}
    }
}
