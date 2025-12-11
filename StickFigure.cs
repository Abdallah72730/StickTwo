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
            UpdatePosition();
        }

        public void Update() 
        {
            X += VelocityX;
            Y += VelocityY;
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(Head, X - 10);
            Canvas.SetTop(Head, Y);

            Body.X1 = X;
            Body.Y1 = Y + 20;
            Body.X2 = X;
            Body.Y2 = Y + 60;

            LeftArm.X1 = X;
            LeftArm.Y1 = Y + 30;
            LeftArm.X2 = X - 15;
            LeftArm.Y2 = Y + 50;

            RightArm.X1 = X;
            RightArm.Y1 = Y + 30;
            RightArm.X2 = X + 15;
            RightArm.Y2 = Y + 50;

            LeftLeg.X1 = X;
            LeftLeg.Y1 = Y + 60;
            LeftLeg.X2 = X - 15;
            LeftLeg.Y2 = Y + 90;

            RightLeg.X1 = X;
            RightLeg.Y1 = Y + 60;
            RightLeg.X2 = X + 15;
            RightLeg.Y2 = Y + 90;
        }


    }
}
