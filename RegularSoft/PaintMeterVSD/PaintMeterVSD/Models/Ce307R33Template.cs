using PaintMeterVSD.Models.Base;
using PaintMeterVSD.Views.Templates;
using System.Windows.Controls;

namespace PaintMeterVSD.Models
{
    internal class Ce307R33Template : ILcdTemplate
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private CE307R33 _content;

        public Ce307R33Template()
        {
            Name = "CE 307 R33";
            _content = new CE307R33();
        }

        public Canvas GetCanvas()
        {
            return _content.canvas;
        }
    }
}
