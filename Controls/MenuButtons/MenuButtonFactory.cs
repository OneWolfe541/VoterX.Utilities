using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VoterX.Utilities.Controls
{
    public class MenuButtonFactory
    {
        public MenuButton CreateButton(
            FontAwesomeIcon Icon,
            string ButtonText,
            string ToolTip,
            Thickness Margin,
            Action<object> Execute,
            Predicate<object> CanExecute)
        {
            MenuButtonViewModel model = new MenuButtonViewModel(Icon, ButtonText, ToolTip, Margin, Execute, CanExecute);

            // Create default button
            MenuButton homeButton = new MenuButton(model);

            // Apply the view model
            //homeButton.DataContext = model;

            return homeButton;
        }

        public MenuButton CreateButton(
            FontAwesomeIcon Icon,
            string ButtonText,
            string ToolTip,
            Thickness Margin,
            Action<object> Execute)
        {
            MenuButtonViewModel model = new MenuButtonViewModel(Icon, ButtonText, ToolTip, Margin, Execute);

            // Create default button
            MenuButton homeButton = new MenuButton(model);

            // Apply the view model
            //homeButton.DataContext = model;

            return homeButton;
        }

        public MenuButton CreateButton(
            string Icon,
            int IconFontSize,
            int IconWidth,
            string ButtonText,
            string ToolTip,
            Thickness Margin,
            Action<object> Execute)
        {
            MenuButtonViewModel model = new MenuButtonViewModel(Icon, IconFontSize, IconWidth, ButtonText, ToolTip, Margin, Execute);

            // Create default button
            MenuButton homeButton = new MenuButton(model);

            // Apply the view model
            //homeButton.DataContext = model;

            return homeButton;
        }

        public MenuButton CreateButton(
            string Icon,
            int IconFontSize,
            int IconWidth,
            string ButtonText,
            string ToolTip,
            Thickness Margin,
            Action<object> Execute,
            Predicate<object> CanExecute)
        {
            MenuButtonViewModel model = new MenuButtonViewModel(Icon, IconFontSize, IconWidth, ButtonText, ToolTip, Margin, Execute, CanExecute);

            // Create default button
            MenuButton homeButton = new MenuButton(model);

            // Apply the view model
            //homeButton.DataContext = model;

            return homeButton;
        }

        public MenuButton CreateButton(
            Uri ImagePath,
            int ImageHeight,
            int ImageWidth,
            string ButtonText,
            string ToolTip,
            Thickness Margin,
            Action<object> Execute,
            Predicate<object> CanExecute)
        {
            MenuButtonViewModel model = new MenuButtonViewModel(ImagePath, ImageHeight, ImageWidth, ButtonText, ToolTip, Margin, Execute, CanExecute);

            // Create default button
            MenuButton homeButton = new MenuButton(model);

            // Apply the view model
            //homeButton.DataContext = model;

            return homeButton;
        }
    }
}
