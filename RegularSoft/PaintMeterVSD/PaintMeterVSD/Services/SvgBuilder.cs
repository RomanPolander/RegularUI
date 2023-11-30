using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintMeterVSD.Services
{
    internal static class SvgBuilder
    {
        private static string _documentContent;
        private static UIElement _mainPanel;

        public static string CreateDocumentFromElement(UIElement element)
        {
            try
            {
                _mainPanel = element;
                _documentContent = "<svg xmlns=\"http://www.w3.org/2000/svg\" ";

                if (element is Canvas) _documentContent = string.Concat(_documentContent, GetXmlStringFromMainCanvas(element as Canvas));

                return string.Concat(_documentContent, "</svg>");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Возникла ошибка");
                return string.Empty;
            }
        }
        private static string GetXmlStringFromMainCanvas(Canvas canvas)
        {
            string xmlString = $"viewBox=\"0 0 {canvas.ActualWidth} {canvas.ActualHeight}\">\n";
            xmlString = string.Concat(xmlString, GetXmlStringFromChildren(canvas.Children, canvas));
            return xmlString;
        }
        private static string GetXmlStringFromChildren(UIElementCollection children, UIElement parent)
        {
            string xmlString = "";

            foreach (UIElement element in children)
            {
                if (element.Visibility != Visibility.Visible) continue;

                if (element is Rectangle)
                {
                    xmlString = string.Concat(xmlString, GetXmlStringFromRectangle(element as Rectangle, parent));
                }
                else if (element is Canvas)
                {
                    xmlString = string.Concat(xmlString, GetXmlStringFromCanvas(element as Canvas, element));
                }
            }

            return xmlString;
        }
        private static string GetXmlStringFromRectangle(Rectangle rectangle, UIElement parent)
        {
            string xmlString = "";

            if (rectangle.Visibility != Visibility.Visible) return "";

            Size size = rectangle.RenderSize;
            Point translatePoint = rectangle.TranslatePoint(new Point(0, 0), _mainPanel);

            string fill = rectangle.Fill != null ? $"fill=\"#{rectangle.Fill.ToString().Substring(3)}\"" : "";
            string stroke = rectangle.Stroke != null ? stroke = $"stroke =\"#{rectangle.Stroke.ToString().Substring(3)}\"" : "";

            xmlString = string.Concat(xmlString, $"<path d=\"M {translatePoint.X} {translatePoint.Y} h {size.Width} v {size.Height} h {-size.Width} Z\" {fill} {stroke} />\n");

            return xmlString;
        }
        private static string GetXmlStringFromCanvas(Canvas canvas, UIElement parent) 
        {
            string xmlString = $"<g id=\"{canvas.Name}\">\n";

            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle)
                {
                    xmlString = string.Concat(xmlString, GetXmlStringFromRectangle(element as Rectangle, canvas));
                }
                else if (element is Ellipse)
                {
                    xmlString = string.Concat(xmlString, GetXmlStringFromEllipse((element as Ellipse), canvas));
                }
                else if (element is Path) 
                {
                    xmlString = string.Concat(xmlString, GetXmlStringFromPath(element as Path, canvas));
                }
            }

            xmlString = string.Concat(xmlString, "</g>\n");

            return xmlString;
        }
        private static string GetXmlStringFromEllipse(Ellipse ellipse, UIElement parent) 
        {
            Point translatePointToMainPanel = ellipse.TranslatePoint(new Point(0, 0), _mainPanel);
            Point translatePointToParent = ellipse.TranslatePoint(new Point(0, 0), parent);

            Point translatePoint = new Point(translatePointToMainPanel.X + translatePointToParent.X, translatePointToMainPanel.Y + translatePointToParent.Y);

            string radius = (ellipse.Width / 2).ToString().Replace(',', '.');
            string cx = (translatePoint.X + ellipse.Width / 2).ToString().Replace(',', '.');
            string cy = (translatePoint.Y + ellipse.Width / 2).ToString().Replace(',', '.');

            string xmlString = $"\t<circle cx=\"{cx}\" cy=\"{cy}\" r=\"{radius}\"/>\n";

            return xmlString;
        }
        private static string GetXmlStringFromPath(Path path, UIElement parent)
        {
            try
            {
                Point translatePointToMainPanel = path.TranslatePoint(new Point(0, 0), _mainPanel);
                Point translatePointToParent = path.TranslatePoint(new Point(0, 0), parent);

                Point translatePoint = new Point(translatePointToMainPanel.X + translatePointToParent.X, translatePointToMainPanel.Y + translatePointToParent.Y);

                string fillRule = "";
                string data = path.Data.ToString();
                data = data.Replace(',', '.');
                data = data.Replace(';', ' ');
                if (data.Substring(0, 2) == "F1") 
                {
                    data = data.Substring(2);
                    fillRule = "fill-rule=\"nonzero\"";
                }


                string fill = path.Fill != null ? $"fill=\"#{path.Fill.ToString().Substring(3)}\"" : "fill=\"none\"";
                string stroke = path.Stroke != null ? stroke = $"stroke=\"#{path.Stroke.ToString().Substring(3)}\"" : "";
                string strokeThickness = path.StrokeThickness != 0 ? strokeThickness = $"stroke-width=\"{path.StrokeThickness.ToString().Replace(',', '.')}\"" : "";
                string transform = $"transform=\"translate({translatePoint.X} {translatePoint.Y})\"";

                return $"\t<path {fillRule} d=\"{data}\" {transform.Replace(',', '.')} {fill} {stroke} {strokeThickness}/>\n";
            }
            catch
            {
                MessageBox.Show("Error from path");
                return "";
            }
        }
    }
}
