using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTask.Shapes
{
    public class Circle : Shape<Ellipse, Circle>
    {
        public string type { get; set; }
        public string center { get; set; }
        public double radius { get; set; }
        public bool filled { get; set; }
        public string color { get; set; }
        public double a1 { get; set; }
        public double a2 { get; set; }
        public double r { get; set; }

        public override Ellipse Draw(Canvas myCanvas)
        {
            double moveX = myCanvas.Width / 2;
            double moveY = myCanvas.Height / 2;
            double diameter = 2 * r;

            var ellipseToDraw = new Ellipse();

            ellipseToDraw.Height = diameter;
            ellipseToDraw.Width = diameter;
            ellipseToDraw.Margin = new Thickness(a1 - r + moveX, a2 - r + moveY, 0, 0);
            ellipseToDraw.Stroke = SetColorFromRGB(color.Split(";"));
            if (filled)
                ellipseToDraw.Fill = SetColorFromRGB(color.Split(";"));

            myCanvas.Children.Add(ellipseToDraw);
            return ellipseToDraw;
        }

        public override void SetPointsScale(Canvas myCanvas)
        {
            a1 = Double.Parse(center.Split(";")[0]);
            a2 = -Double.Parse(center.Split(";")[1]);
            r = radius;

            List<double> scales = new List<double>()
            {
                GetScale(Math.Abs(a1) + r, myCanvas.Width),
                GetScale(Math.Abs(a2) + r, myCanvas.Height),
                GetScale(r, myCanvas.Width)
            };

            scale = scales.Min();
        }

        public override void ScalePoints()
        {
            a1 *= scale;
            a2 *= scale;
            r *= scale;            
        }

        public override Circle DeserializeStringFillData(string circleToDeserialize)
        {
            return GenerateShapeObject(circleToDeserialize);
        }
    }
}
