using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VoterX.Utilities.Extensions
{
    // https://stackoverflow.com/questions/1356045/set-focus-on-textbox-in-wpf-from-view-model

    //public static class FocusExtension
    //{
    //    public static bool GetIsFocused(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(IsFocusedProperty);
    //    }

    //    public static void SetIsFocused(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(IsFocusedProperty, value);
    //    }

    //    public static readonly DependencyProperty IsFocusedProperty =
    //        DependencyProperty.RegisterAttached(
    //            "IsFocused", typeof(bool), typeof(FocusExtension),
    //            new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

    //    private static void OnIsFocusedPropertyChanged(
    //        DependencyObject d,
    //        DependencyPropertyChangedEventArgs e)
    //    {
    //        var uie = (UIElement)d;
    //        if ((bool)e.NewValue)
    //        {
    //            uie.Focus(); // Don't care about false values.
    //        }
    //    }
    //}

    // https://stackoverflow.com/a/31272370
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
             "IsFocused", typeof(bool), typeof(FocusExtension),
             new UIPropertyMetadata(false, null, OnCoerceValue));

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            if ((bool)baseValue)
                ((UIElement)d).Focus();
            else if (((UIElement)d).IsFocused)
                Keyboard.ClearFocus();
            return ((bool)baseValue);
        }
    }
}
