using System.Windows;

namespace RegularUI.WPF
{
    public static class MenuAssist
    {
        #region AttachedProperty : TopLevelMenuItemHeight
        public static readonly DependencyProperty TopLevelMenuItemHeightProperty
            = DependencyProperty.RegisterAttached(
                "TopLevelMenuItemHeight",
                typeof(double),
                typeof(MenuAssist));

        public static double GetTopLevelMenuItemHeight(DependencyObject element) => (double)element.GetValue(TopLevelMenuItemHeightProperty);
        public static void SetTopLevelMenuItemHeight(DependencyObject element, double value) => element.SetValue(TopLevelMenuItemHeightProperty, value);
        #endregion

        public static readonly DependencyProperty IsColoredProperty = DependencyProperty.RegisterAttached(
                "IsColored", typeof(bool), typeof(MenuAssist), new PropertyMetadata(false));

        public static void SetIsColored(DependencyObject element, bool value) => element.SetValue(IsColoredProperty, value);

        public static bool GetIsColored(DependencyObject element) => (bool)element.GetValue(IsColoredProperty);
    }
}
