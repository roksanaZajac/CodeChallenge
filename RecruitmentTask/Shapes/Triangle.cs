using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RecruitmentTask.Shapes
{
    public class Triangle : Shape<Polygon, Triangle>
    {
        public string type { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public bool filled { get; set; }
        public string color { get; set; }
        public double a1 { get; set; }
        public double a2 { get; set; }
        public double b1 { get; set; }
        public double b2 { get; set; }
        public double c1 { get; set; }
        public double c2 { get; set; }

        public override Polygon Draw(Canvas myCanvas)
        {
            double moveX = myCanvas.Width / 2;
            double moveY = myCanvas.Height / 2;

            string[] colors = color.Split(";");

            List<Point> newPoints = new List<Point>() {
                new Point(a1 + moveX, a2 + moveY),
                new Point(b1 + moveX, b2 + moveY),
                new Point(c1 + moveX, c2 + moveX),
                new Point(a1 + moveX, a2 + moveX)
            };

            var triangleToDraw = new Polygon();

            triangleToDraw.Points = new PointCollection(newPoints);
            triangleToDraw.Stroke = SetColorFromRGB(colors);
            if (filled)
                triangleToDraw.Fill = SetColorFromRGB(colors);

            myCanvas.Children.Add(triangleToDraw);
            return triangleToDraw;
        }

        public override void SetPointsScale(Canvas myCanvas)
        {
            string[] aSplited = a.Split(";");
            string[] bSplited = b.Split(";");
            string[] cSplited = c.Split(";");

            a1 = Double.Parse(aSplited[0]);
            a2 = -Double.Parse(aSplited[1]);
            b1 = Double.Parse(bSplited[0]);
            b2 = -Double.Parse(bSplited[1]);
            c1 = Double.Parse(cSplited[0]);
            c2 = -Double.Parse(cSplited[1]);

            List<double> scales = new List<double>()
            {
                GetScale(a1, myCanvas.Width),
                GetScale(a2, myCanvas.Height),
                GetScale(b1, myCanvas.Width),
                GetScale(b2, myCanvas.Height),
                GetScale(c1, myCanvas.Width),
                GetScale(c2, myCanvas.Height)
            };

            scale = scales.Min();
        }

        public override void ScalePoints()
        {
            a1 *= scale;
            a2 *= scale;
            b1 *= scale;
            b2 *= scale;
            c1 *= scale;
            c2 *= scale;
        }

        public override Triangle DeserializeStringFillData(string triangleToDeserialize)
        {
            return GenerateShapeObject(triangleToDeserialize);
        }
    }
}
