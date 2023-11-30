using Microsoft.Win32;
using PaintMeterVSD.Core;
using PaintMeterVSD.Models;
using PaintMeterVSD.Models.Base;
using PaintMeterVSD.Services;
using PaintMeterVSD.ViewModels.Base;
using PaintMeterVSD.Views.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PaintMeterVSD.ViewModels
{
    internal class MainVM : ViewModel
    {
        #region Свойства

        #region Templates
        private ObservableCollection<ILcdTemplate> _templates;
        public ObservableCollection<ILcdTemplate> Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                OnPropertyChanged(nameof(Templates));
            }
        }
        #endregion

        #region Elements
        private ObservableCollection<ElementVM> _elements;
        public ObservableCollection<ElementVM> Elements
        {
            get { return _elements; }
            set
            {
                _elements = value;
                OnPropertyChanged(nameof(Elements));
            }
        }

        private void SetElements(UIElementCollection elements) 
        {
            Elements.Clear();

            foreach(UIElement element in elements) 
            {
                if (element is Canvas) 
                {
                    Elements.Add(new ElementVM());

                    if ((element as Canvas).Name == string.Empty)
                        Elements.Last().Name = "Без названия";
                    else
                        Elements.Last().Name = (element as Canvas).Name;

                    Elements.Last().Content = element as Canvas;
                    var xaml = System.Windows.Markup.XamlWriter.Save(element as Canvas);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as Canvas;
                    Elements.Last().Icon = deepCopy;

                    if ((element as Canvas).Visibility == Visibility.Visible)
                        Elements.Last().IsVisible = true;
                    else
                        Elements.Last().IsVisible = false;

                }
            }
        }
        #endregion

        #region CurrentTemplate
        private ILcdTemplate _currentTemplate;
        public ILcdTemplate CurrentTemplate
        {
            get { return _currentTemplate; }
            set
            {
                _currentTemplate = value;
                CurrentCanvas = _currentTemplate.GetCanvas();
                OnPropertyChanged(nameof(CurrentTemplate));
            }
        }
        #endregion

        #region CurrentCanvas
        private Canvas _currentCanvas;
        public Canvas CurrentCanvas
        {
            get { return _currentCanvas; }
            set
            {
                _currentCanvas = value;
                SetElements(_currentCanvas.Children);
                OnPropertyChanged(nameof(CurrentCanvas));
            }
        }
        #endregion

        #endregion

        public MainVM() 
        {
            Templates = new ObservableCollection<ILcdTemplate> { new Ce308Template(), new Ce307RuTemplate(), new Ce307R33Template()};
            Elements = new ObservableCollection<ElementVM>();
            CurrentTemplate = Templates.First();
        }

        #region Команды

        #region Сохранить как SVG
        public RelayCommand SaveAsSvg
        {
            get
            {
                return new RelayCommand(o =>
                {
                //string path = string.Format($"d:\\users\\{Environment.UserName}\\Downloads\\MyTest.svg");
                    string documentContent = SvgBuilder.CreateDocumentFromElement(CurrentTemplate.GetCanvas());

                    SaveFileDialog dlg = new SaveFileDialog
                    {
                        FileName = CurrentTemplate.Name,
                        DefaultExt = ".svg",
                        Filter = "Масштабируемый векторный рисунок (.svg)|*.svg"
                    };

                    var flag = dlg.ShowDialog();
                    bool flag2 = true;

                    if (flag.GetValueOrDefault() == flag2 & flag.HasValue)
                    {

                        try
                        {
                            // Create the file, or overwrite if the file exists.
                            using (FileStream fs = File.Create(dlg.FileName))
                            {
                                byte[] info = new UTF8Encoding(true).GetBytes(documentContent);
                                // Add some information to the file.
                                fs.Write(info, 0, info.Length);
                            }

                            MessageBox.Show("Файл устпешно сохранен!", "Save As SVG", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                });
            }
        }

        #endregion

        #endregion
    }
}
