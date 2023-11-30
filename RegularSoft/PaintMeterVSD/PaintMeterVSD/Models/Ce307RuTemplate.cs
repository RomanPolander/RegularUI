using PaintMeterVSD.Models.Base;
using PaintMeterVSD.Views.Templates;
using System.Windows.Controls;

namespace PaintMeterVSD.Models
{
    internal class Ce307RuTemplate : ILcdTemplate
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private CE307Ru _content;

        public Ce307RuTemplate()
        {
            Name = "CE 307 Ru";
            _content = new CE307Ru();
        }

        public Canvas GetCanvas()
        {
            return _content.canvas;
        }
    }
}
