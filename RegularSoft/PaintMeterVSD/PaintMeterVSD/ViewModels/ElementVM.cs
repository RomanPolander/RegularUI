using PaintMeterVSD.ViewModels.Base;
using System.Windows.Controls;

namespace PaintMeterVSD.ViewModels
{
    internal class ElementVM : ViewModel
    {
        private string _name;
        public string Name 
        { 
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private Canvas _content;
        public Canvas Content 
        {
            get { return _content; }
            set 
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private Canvas _icon;
        public Canvas Icon 
        {
            get { return _icon; }
            set 
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        private bool _isVisible;
        public bool IsVisible 
        {
            get { return _isVisible; }
            set 
            {
                if (value)
                    Content.Visibility = System.Windows.Visibility.Visible;
                else
                    Content.Visibility = System.Windows.Visibility.Hidden;

                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
