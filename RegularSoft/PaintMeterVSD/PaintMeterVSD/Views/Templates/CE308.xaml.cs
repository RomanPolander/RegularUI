using System.Windows.Controls;

namespace PaintMeterVSD.Views.Templates
{
    /// <summary>
    /// Логика взаимодействия для CE308.xaml
    /// </summary>
    public partial class CE308 : UserControl
    {
        public CE308()
        {
            InitializeComponent();
        }

        public Canvas GetCanvas() 
        {
            return canvas;
        }
    }
}
