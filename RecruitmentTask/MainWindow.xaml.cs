using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RecruitmentTask.Shapes;
using Line = System.Windows.Shapes.Line;

namespace RecruitmentTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int zoom = 1;
        private int index = 0;
        private List<object> shapesToDraw;
        private Canvas myCanvas;
        private Transform transform;
        private ScrollViewer scrollViewer;

        public MainWindow()
        {
            InitializeComponent();

            LoadData loadData = new LoadData();
            shapesToDraw = new List<object>();
            loadData.LoadFromJsonFile(shapesToDraw, @"..\..\..\..\Figures.txt");
            DrawFirstOrNextShape();
        }

        public void DrawFirstOrNextShape()
        {
            if (myCanvas != null)
                myGrid.Children.Remove(scrollViewer);
            if (shapesToDraw.Count == index)
                index = 0;
            GenerateCanvas();
            DrawShape(shapesToDraw, index);
            index++;
        }

        public void DrawShape(List<object> shapesToDraw, int index)
        {
            if (shapesToDraw.Count > index)
            {
                var shapeType = shapesToDraw[index].GetType().FullName;
                if (string.Equals("RecruitmentTask.Shapes.Line", shapeType))
                {
                    Shapes.Line line = (Shapes.Line)shapesToDraw[index];
                    line.SetPointsScale(myCanvas);
                    line.ScalePoints();
                    GenerateCanvasContent(line.scale);
                    line.Draw(myCanvas);
                }
                else if (string.Equals("RecruitmentTask.Shapes.Circle", shapeType))
                {
                    Circle circle = (Circle)shapesToDraw[index];
                    circle.SetPointsScale(myCanvas);
                    circle.ScalePoints();
                    GenerateCanvasContent(circle.scale);
                    circle.Draw(myCanvas);
                }
                else if (string.Equals("RecruitmentTask.Shapes.Triangle", shapeType))
                {
                    Triangle triangle = (Triangle)shapesToDraw[index];
                    triangle.SetPointsScale(myCanvas);
                    triangle.ScalePoints();
                    GenerateCanvasContent(triangle.scale);
                    triangle.Draw(myCanvas);
                }
            }
        }

        public void GenerateCanvas()
        {
            myCanvas = new Canvas();
            myCanvas.Width = 700;
            myCanvas.Height = 700;
            myCanvas.LayoutTransform = transform;
            myCanvas.Background = Brushes.GhostWhite;
            myCanvas.ClipToBounds = true;

            scrollViewer = new ScrollViewer();
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public void GenerateCanvasContent(double scale)
        {
            GenerateTextBlock("0", 5, myCanvas.Width / 2 + 3, myCanvas.Height / 2 - 3.5);        
            GenerateTextBlock("Y", 10, myCanvas.Width / 2 + 5, 5);

            //0 line
            GenerateLine(-2, 0, 2, 0, myCanvas.Width / 2, myCanvas.Height / 2);

            //Y axis
            GenerateLine(0, myCanvas.Width / 2 - 5, 0, -myCanvas.Width / 2 + 5, myCanvas.Width / 2, myCanvas.Height / 2);

            //direction
            GenerateLine(-2, myCanvas.Width / 2 - 10, 0, myCanvas.Width / 2 - 5, myCanvas.Width / 2, myCanvas.Height / 2);
            GenerateLine(2, myCanvas.Width / 2 - 10, 0, myCanvas.Width / 2 - 5, myCanvas.Width / 2, myCanvas.Height / 2);

            scrollViewer.Content = myCanvas;
            myGrid.Children.Add(scrollViewer);
        }

        private void GenerateLine(double X1, double Y1, double X2, double Y2, double leftMargin, double topMargin)
        {
            Line newLine = new Line();
            newLine.X1 = X1;
            newLine.Y1 = -Y1;
            newLine.X2 = X2;
            newLine.Y2 = -Y2;
            newLine.Stroke = new SolidColorBrush(Colors.Black);
            newLine.StrokeThickness = 0.5;
            Canvas.SetLeft(newLine, leftMargin);
            Canvas.SetTop(newLine, topMargin);
            myCanvas.Children.Add(newLine);
        }

        private void GenerateTextBlock(string text, double fontSize, double leftMargin, double topMargin)
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.FontSize = fontSize;
            newTextBlock.Text = text;
            newTextBlock.TextAlignment = TextAlignment.Center;
            Canvas.SetLeft(newTextBlock, leftMargin);
            Canvas.SetTop(newTextBlock, topMargin);
            myCanvas.Children.Add(newTextBlock);
        }
        private void NextShape_Click(object sender, RoutedEventArgs e)
        {
            DrawFirstOrNextShape();
        }

        private void ScaleUp_Click(object sender, RoutedEventArgs e)
        {
            if (zoom < 1)
                zoom = 1;
            int newZoom = ++zoom;
            if (newZoom > 0)
                transform = new ScaleTransform(newZoom, newZoom, myCanvas.Width / 2, myCanvas.Height / 2);
            myCanvas.LayoutTransform = transform;
        }

        private void ScaleDown_Click(object sender, RoutedEventArgs e)
        {
            if (zoom < 3)
                zoom = 2;
            int newZoom = --zoom;
            if (newZoom > 0)
                transform = new ScaleTransform(newZoom, newZoom, myCanvas.Width / 2, myCanvas.Height / 2);
            myCanvas.LayoutTransform = transform;
        }
    }
}
