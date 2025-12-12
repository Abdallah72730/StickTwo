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
    }
}
