using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Controls
{
    public class MenuButtonViewModel : IMenuButtonViewModel
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public string StIcon { get; set; }
        public Uri ImagePath { get; set; }
        public int IconFontSize { get; set; }
        public int IconWidth { get; set; }
        public bool StIconVisibility { get; set; }
        public bool FaIconVisibility { get; set; }

        public MenuButtonViewModel(
            FontAwesome.WPF.FontAwesomeIcon faIcon,
            string text,
            string toolTip,
            Action<object> execute,
            Predicate<object> canExecute)
        {
            Icon = faIcon;
            Text = text;
            ToolTip = toolTip;

            StIconVisibility = false;
            FaIconVisibility = true;

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }

        public MenuButtonViewModel(
            FontAwesomeIcon faIcon,
            string text,
            string toolTip,
            Thickness margin,
            Action<object> execute)
        {
            Icon = faIcon;
            Text = text;
            ToolTip = toolTip;
            Margin = margin;
            _execute = execute;

            StIconVisibility = false;
            FaIconVisibility = true;
        }

        public MenuButtonViewModel(
            FontAwesomeIcon faIcon,
            string text,
            string toolTip,
            Thickness margin,
            Action<object> execute,
            Predicate<object> canExecute)
        {
            Icon = faIcon;
            Text = text;
            ToolTip = toolTip;
            Margin = margin;
            _execute = execute;
            _canExecute = canExecute;

            StIconVisibility = false;
            FaIconVisibility = true;
        }

        public MenuButtonViewModel(
            string icon,
            int iconFontSize,
            int iconWidth,
            string text,
            string toolTip,
            Thickness margin,
            Action<object> execute)
        {
            StIcon = icon;
            IconFontSize = iconFontSize;
            IconWidth = iconWidth;
            Text = text;
            ToolTip = toolTip;
            Margin = margin;
            _execute = execute;

            StIconVisibility = true;
            FaIconVisibility = false;
        }

        public MenuButtonViewModel(
            string icon,
            int iconFontSize,
            int iconWidth,
            string text,
            string toolTip,
            Thickness margin,
            Action<object> execute,
            Predicate<object> canExecute)
        {
            StIcon = icon;
            IconFontSize = iconFontSize;
            IconWidth = iconWidth;
            Text = text;
            ToolTip = toolTip;
            Margin = margin;
            _execute = execute;
            _canExecute = canExecute;

            StIconVisibility = true;
            FaIconVisibility = false;
        }

        public MenuButtonViewModel(
            Uri path,
            int imageHeight,
            int imageWidth,
            string text,
            string toolTip,
            Thickness margin,
            Action<object> execute,
            Predicate<object> canExecute)
        {
            ImagePath = path;
            IconFontSize = imageHeight;
            IconWidth = imageWidth;
            Text = text;
            ToolTip = toolTip;
            Margin = margin;
            _execute = execute;
            _canExecute = canExecute;

            StIconVisibility = true;
            FaIconVisibility = false;
        }

        public FontAwesome.WPF.FontAwesomeIcon _buttonIcon;
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get
            {
                return _buttonIcon;
            }
            set
            {
                _buttonIcon = value;
            }
        }

        public string _buttonText;
        public string Text
        {
            get
            {
                return _buttonText;
            }
            set
            {
                _buttonText = value;
            }
        }

        public string _buttonToolTip;
        public string ToolTip
        {
            get
            {
                return _buttonToolTip;
            }
            set
            {
                _buttonToolTip = value;
            }
        }

        public Thickness _buttonMargin;
        public Thickness Margin
        {
            get
            {
                return _buttonMargin;
            }
            set
            {
                _buttonMargin = value;
            }
        }

        #region Commands
        // Bound command for returning to the login screen
        public RelayCommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                {
                    _navigateCommand = new RelayCommand(param => this.ButtonClick(param), _canExecute);
                }
                return _navigateCommand;
            }
        }

        private void ButtonClick(object parameter)
        {
            _execute(parameter);
        }

        //public void Execute(object parameter) { _execute(parameter); }
        #endregion

    }
}
