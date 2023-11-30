using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PaintMeterVSD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MatrixTransform defaultTransform;

        public MainWindow()
        {
            InitializeComponent();
            defaultTransform = content.RenderTransform.Clone() as MatrixTransform;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region Visio
            //// Запуск приложения Visio для отладки
            //Microsoft.Office.Interop.Visio.Application application = new Microsoft.Office.Interop.Visio.Application();
            //application.Visible = true;

            //// Создание нового .vsd файла, не основанного на каком-либо шаблоне
            //Microsoft.Office.Interop.Visio.Document doc = application.Documents.Add("");
            //// Добавление в документ страницы
            //Microsoft.Office.Interop.Visio.Page page = application.Documents[1].Pages[1];

            ////Get the Width and Height of the Sheet You are Working On

            ////Microsoft.Office.Interop.Visio.Master visioRectMaster = doc.Masters.get_ItemU(@"Rectangle");
            ////Microsoft.Office.Interop.Visio.Shape visioRectShape = page.Drop(visioRectMaster, 4.25, 5.5);
            ////visioRectShape.Text = @"Rectangle text.";

            ////Microsoft.Office.Interop.Visio.Shape rectangle = page.DrawRectangle(1.1, 2.2, 4.5, 6.7);
            //Microsoft.Office.Interop.Visio.Shape rectangle = page.Import(@"D:\Users\PolyakovRS\source\repos\PaintMeterVSD\PaintMeterVSD\Test\Panel2.svg");

            //rectangle.Resize(Microsoft.Office.Interop.Visio.VisResizeDirection.visResizeDirE,-5, Microsoft.Office.Interop.Visio.VisUnitCodes.visCentimeters);

            ////set the shape height
            //rectangle.get_CellsSRC(
            //               (short)Microsoft.Office.Interop.Visio.VisSectionIndices.visSectionObject,
            //               (short)Microsoft.Office.Interop.Visio.VisRowIndices.visRowXFormIn,
            //               (short)Microsoft.Office.Interop.Visio.VisCellIndices.
            //               visXFormHeight).ResultIU = 2d; // 25.5

            ////rectangle.Text = @"Rectangle text.";
            #endregion
        }

        private void Grid_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var matTrans = content.RenderTransform as MatrixTransform;
            var pos1 = e.GetPosition(grid);

            if (Keyboard.IsKeyDown(Key.Z))
            {
                var scale = e.Delta > 0 ? 1.3 : 1 / 1.3;
                var mat = matTrans.Matrix;

                
                mat.ScaleAt(scale, scale, pos1.X, pos1.Y);
                matTrans.Matrix = mat;
                e.Handled = true;
            }
            else 
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl)) 
                {
                    var translate = e.Delta > 0 ? 0.5 : -0.5;
                    var mat = matTrans.Matrix;
                    mat.Translate(e.Delta / 2, 0);
                    matTrans.Matrix = mat;
                    e.Handled = true;
                }
                else 
                {
                    var translate = e.Delta > 0 ? 1.1 : -1.1;
                    var mat = matTrans.Matrix;
                    mat.Translate(0, e.Delta / 2);
                    matTrans.Matrix = mat;
                    e.Handled = true;
                }     
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            content.RenderTransform = defaultTransform.Clone();
        }
    }
}
