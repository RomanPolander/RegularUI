using System.Windows.Controls;

namespace PaintMeterVSD.Models.Base
{
    internal interface ILcdTemplate
    {
        string Name { get; set; }
        Canvas GetCanvas();
    }
}
