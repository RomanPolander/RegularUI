using System.Windows;
using System.Windows.Controls;

namespace PaintMeterVSD.ViewModels.Base
{
    internal interface IElementViewModel
    {
        string Name { get; set; }
        Canvas Content { get; set; }
        Visibility Visibility { get; set; }
    }
}
