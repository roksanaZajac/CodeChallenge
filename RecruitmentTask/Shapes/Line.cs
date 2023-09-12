using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RecruitmentTask.Shapes
{
    public class Line : Shape<System.Windows.Shapes.Line, Line>
    {
        public string type { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string color { get; set; }
        public double a1 { get; set; }
        public double a2 { get; set; }
        public double b1 { get; set; }
        public double b2 { get; set; }

        public override System.Windows.Shapes.Line Draw(Canvas myCanvas)
        {
            double moveX = myCanvas.Width / 2;
            double moveY = myCanvas.Height / 2;

            var lineToDraw = new System.Windows.Shapes.Line();

            lineToDraw.X1 = a1 + moveX;
            lineToDraw.Y1 = a2 + moveY;
            lineToDraw.X2 = b1 + moveX;
            lineToDraw.Y2 = b2 + moveY;
            lineToDraw.Stroke = SetColorFromRGB(color.Split(";"));

            myCanvas.Children.Add(lineToDraw);
            return lineToDraw;
        }

        public override void SetPointsScale(Canvas myCanvas)
        {
            a1 = Double.Parse(a.Split(";")[0]);
            a2 = -Double.Parse(a.Split(";")[1]);
            b1 = Double.Parse(b.Split(";")[0]);
            b2 = -Double.Parse(b.Split(";")[1]);

            List<double> scales = new List<double>()
            {
                GetScale(a1, myCanvas.Width),
                GetScale(a2, myCanvas.Height),
                GetScale(b1, myCanvas.Width),
                GetScale(b2, myCanvas.Height),
            };

            scale = scales.Min();
        }

        public override void ScalePoints()
        {
            a1 *= scale;
            a2 *= scale;
            b1 *= scale;
            b2 *= scale;
        }

        public override Line DeserializeStringFillData(string lineToDeserialize)
        {
            return GenerateShapeObject(lineToDeserialize);
        }
    }
}
