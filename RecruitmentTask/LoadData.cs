using System.Collections.Generic;
using System.IO;
using RecruitmentTask.Shapes;
using Line = RecruitmentTask.Shapes.Line;

namespace RecruitmentTask
{
    public class LoadData
    {
        public void LoadFromJsonFile(List<object> shapesToDraw, string fileName)
        {
            string text = string.Empty;
            List<string> shapesList = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("\""))
                        text += line + "\n";
                    else if (!line.Contains("\"") && !string.IsNullOrEmpty(text))
                    {
                        shapesList.Add("{\n" + text + "}");
                        text = string.Empty;
                    }
                }
            }

            DeserializeStringFillData(shapesList, shapesToDraw);
        }

        public void DeserializeStringFillData(List<string> shapesList, List<object> shapesToDraw)
        {
            foreach (var shape in shapesList)
            {
                if (shape.Contains("line"))
                    shapesToDraw.Add(new Line().DeserializeStringFillData(shape));

                else if (shape.Contains("circle"))
                    shapesToDraw.Add(new Circle().DeserializeStringFillData(shape));

                else if (shape.Contains("triangle"))
                    shapesToDraw.Add(new Triangle().DeserializeStringFillData(shape));
            }
        }
    }
}
