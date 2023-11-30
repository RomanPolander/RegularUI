using PaintMeterVSD.Models.Base;
using PaintMeterVSD.Views.Templates;
using System.Windows.Controls;

namespace PaintMeterVSD.Models
{
    internal class Ce208Template : ILcdTemplate
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private CE208 _content;

        public Ce208Template()
        {
            Name = "CE 208";
            _content = new CE208();
        }

        public Canvas GetCanvas()
        {
            return _content.canvas;
        }
    }
}
