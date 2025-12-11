using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace StickTwo
{
    public class StickFigure
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        private Canvas Container;
        

        private Ellipse Head;
        private Line Body;
        private Line LeftArm;
        private Line RightArm;
        private Line LeftLeg;
        private Line RightLeg;

        public StickFigure(Canvas container, double startX, double startY)
        {
            Container = container;
            X = startX;
            Y = startY;
            VelocityX = 0;
            VelocityY = 0;

            CreateBodyParts();
        }

        public void CreateBodyParts()
        {
            Head = new Ellipse { Width = 20, Height = 20, Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            Body = new Line { Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            LeftArm = new Line { Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            RightArm = new Line { Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            LeftLeg = new Line { Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            RightLeg = new Line { Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 };
            Container.Children.Add(Head);
            Container.Children.Add(Body);
            Container.Children.Add(LeftArm);
            Container.Children.Add(RightArm);
            Container.Children.Add(LeftLeg);
            Container.Children.Add(RightLeg);
            UpdatePosition(0);
        }

        public void Update()
        {
            X += VelocityX;
            Y += VelocityY; 
        }

        public void UpdatePosition(double cameraOffsetX)
        {
            double screenX = X - cameraOffsetX;
            double screenY = Y; 

            Canvas.SetLeft(Head, screenX - 10);
            Canvas.SetTop(Head, screenY);

            Body.X1 = screenX;
            Body.Y1 = screenY + 20;
            Body.X2 = screenX;
            Body.Y2 = screenY + 60;

            LeftArm.X1 = screenX;
            LeftArm.Y1 = screenY + 30;
            LeftArm.X2 = screenX - 15;
            LeftArm.Y2 = screenY + 50;

            RightArm.X1 = screenX;
            RightArm.Y1 = screenY + 30;
            RightArm.X2 = screenX + 15;
            RightArm.Y2 = screenY + 50;

            LeftLeg.X1 = screenX;
            LeftLeg.Y1 = screenY + 60;
            LeftLeg.X2 = screenX - 15;
            LeftLeg.Y2 = screenY + 90;

            RightLeg.X1 = screenX;
            RightLeg.Y1 = screenY + 60;
            RightLeg.X2 = screenX + 15;
            RightLeg.Y2 = screenY + 90;
        }
    }
}
