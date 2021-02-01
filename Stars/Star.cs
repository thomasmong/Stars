using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Stars
{
    class Star
    {
        private Canvas Space;
        public Point Position;
        public Line Shape;
        private Random rdm;
        private double LongueurInit;

        public Star(Canvas canevas, Point pos)
        {
            Space = canevas;

            //Init pos
            Position = pos;

            //Init shape
            Shape = new Line();
            Shape.X1 = 0;
            Shape.Y1 = 0;
            SetPosition();
            rdm = new Random();
            LongueurInit = rdm.NextDouble() * 5;
            Shape.Stroke = Brushes.White;
            Space.Children.Add(Shape);
        }

        private void SetPosition()
        {
            Canvas.SetLeft(this.Shape, Position.X);
            Canvas.SetTop(this.Shape, Position.Y);
        }

        private void Move(Vector vect)
        {
            Position = Point.Add(Position,vect);
            Canvas.SetLeft(this.Shape, Position.X);
            Canvas.SetTop(this.Shape, Position.Y);
        }

        public bool IsVisible()
        {
            bool sortie = !(Position.X < 0);

            if (Position.X > Space.Width)
            {
                sortie = false;
            }

            if (Position.Y < 0)
            {
                sortie = false;
            }

            if (Position.Y > Space.Height)
            {
                sortie = false;
            }
            return sortie;
        }

        private void NewPosition(Point center)
        {
            Vector direction = Point.Subtract(Position, center);
            SetDim(direction);
            direction.Normalize();
            direction *= 12;
            Move(direction);
        }

        private void SetDim(Vector vect)
        {
            double distance = vect.Length;
            vect.Normalize();
            double dim = LongueurInit + 150 / (Math.Pow(Space.Width, 2) + Math.Pow(Space.Height, 2)) * Math.Pow(distance,2);
            Point P2 = Point.Add(new Point(0,0), vect * dim);
            Shape.X2 = P2.X;
            Shape.Y2 = P2.Y;

            double dim2 = 1 + 3 / (Math.Pow(Space.Width, 2) + Math.Pow(Space.Height, 2)) * Math.Pow(distance, 2);
            Shape.StrokeThickness = dim2;
        }

        public void Update(Point direction)
        {
            //Calcul de la nouvelle position
            NewPosition(direction);
        }
    }
}
