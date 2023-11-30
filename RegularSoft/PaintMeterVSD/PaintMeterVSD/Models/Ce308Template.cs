using PaintMeterVSD.Models.Base;
using PaintMeterVSD.Views.Templates;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PaintMeterVSD.Models
{
    internal class Ce308Template : ILcdTemplate
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }

        private CE308 _content;

        public Ce308Template() 
        {
            Name = "CE 308";
            _content = new CE308();
        }

        public Canvas GetCanvas()
        {
            return _content.canvas;
        }
    }
}
