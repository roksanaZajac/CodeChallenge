using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecruitmentTask.Shapes
{
    public abstract class Shape<T1, T2>
    {
        public double scale = 1;
        public abstract T1 Draw(Canvas myCanvas);
        public abstract void SetPointsScale(Canvas myCanvas);
        public abstract void ScalePoints();
        public abstract T2 DeserializeStringFillData(string shapeToDeserialize);

        protected double GetScale(double dimension, double maxDimension)
        {
            double absDimension = Math.Abs(dimension);
            if (absDimension > maxDimension / 2)
            {
                return CalcScale(absDimension, maxDimension / 2);
            }
            else if (absDimension > maxDimension / 2)
            {
                return CalcScale(absDimension, maxDimension / 2);
            }
            return 1;
        }

        protected double CalcScale(double dimension, double maxDimension)
        {
            double diff = (dimension - maxDimension);
            double reduction = diff / dimension;
            return 1 - reduction;
        }

        protected T2 GenerateShapeObject(string shape)
        {
            Type typeOfShape = typeof(T2);
            object shapeObject = Activator.CreateInstance(typeOfShape);
            shapeObject = System.Text.Json.JsonSerializer.Deserialize<T2>(shape);
            return (T2)shapeObject;
        }

        protected SolidColorBrush SetColorFromRGB(string[] argb)
        {
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(Convert.ToByte(argb[0]), Convert.ToByte(argb[1]), Convert.ToByte(argb[2]), Convert.ToByte(argb[3]));
            return color;
        }
    }
}
