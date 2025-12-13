using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StickTwo
{
    public class Cloud
    {
        private Canvas parent;
        private Canvas cloudCanvas;   // <--- Each cloud has its own small canvas
        private double originalX;
        private double originalY;

        public Cloud(Canvas parent, double startX, double startY, double size)
        {
            this.parent = parent;
            originalX = startX;
            originalY = startY;

            cloudCanvas = new Canvas();
            Canvas.SetLeft(cloudCanvas, startX);
            Canvas.SetTop(cloudCanvas, startY);
            parent.Children.Add(cloudCanvas);

            AddCloudPart(0, 0, size);
            AddCloudPart(size * 0.5, -size * 0.2, size * 0.8);
            AddCloudPart(size * 0.9, 0, size * 0.9);
        }

        private void AddCloudPart(double offsetX, double offsetY, double size)
        {
            Ellipse e = new Ellipse
            {
                Width = size,
                Height = size * 0.6,
                Fill = Brushes.White,
                Opacity = 0.8,
                IsHitTestVisible = false
            };

            Canvas.SetLeft(e, offsetX);
            Canvas.SetTop(e, offsetY);

            cloudCanvas.Children.Add(e);
        }

        public void update(double offsetX)
        {
            Canvas.SetLeft(cloudCanvas, originalX + offsetX);
        }
    }
}