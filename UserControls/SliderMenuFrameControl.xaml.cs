using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoterX.Utilities.BasePageDefinitions;

namespace VoterX.Utilities.UserControls
{
    /// <summary>
    /// Menu Slider using the Frame control
    /// </summary>
    public partial class SliderMenuFrameControl : UserControl
    {
        public SliderMenuFrameControl()
        {
            InitializeComponent();
        }

        // Public access to the Frame Control
        public Frame MenuFrame
        {
            get { return MenuContentFrame; }
            private set { MenuContentFrame = value; }
        }

        /// <summary>
        /// Controls which menu animation behavior will be used
        /// </summary>
        public MenuCollapseMode CollapseMode { get; set; }

        /// <summary>
        /// Sets the source page for the Frame
        /// </summary>
        /// <param name="pageUri"></param>
        public void SetMenuSource(System.Uri pageUri)
        {
            MenuContentFrame.Source = pageUri;
        }

        /// <summary>
        /// Hides or disables the menu
        /// </summary>
        private void Hide()
        {
            MenuControl.Width = 0;
        }

        /// <summary>
        /// Animates the menu to open based on the predetermined behavior
        /// </summary>
        public void Open()
        {
            // Opens the menu from fully closed or minimized positions
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    //AnimateMenuSliderFullOpen();
                    AnimateMenuSliderIconOpen();
                }
                else if (MenuControl.DataContext.ToString() == "Icons")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderIconOpenOpen();
                }
                else if (MenuControl.DataContext.ToString() == "IconsOpen")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
            }
        }

        public void Close()
        {
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderFullClose();
                }
                else if (MenuControl.DataContext.ToString() == "Icons")
                {
                    AnimateMenuSliderShortCloseClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderShortCloseClose();
                }
                else if (MenuControl.DataContext.ToString() == "IconsOpen")
                {
                    AnimateMenuSliderIconOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderFullClose();
                }
                else if (MenuControl.DataContext.ToString() == "Icons")
                {
                    AnimateMenuSliderIconClose(); 
                }
            }
        }

        public void Toggle()
        {
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
                else if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderShortClose();
                }
                else if (MenuControl.DataContext.ToString() == "Icons")
                {
                    AnimateMenuSliderShortOpen();
                }
                else if (MenuControl.DataContext.ToString() == "IconsClose")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderIconOpenOpen();
                }
                else if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderShortCloseClose();
                }
                else if (MenuControl.DataContext.ToString() == "IconsOpen")
                {
                    AnimateMenuSliderShortOpen();
                }
                else if (MenuControl.DataContext.ToString() == "IconsClose")
                {
                    AnimateMenuSliderIconClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
                else if (MenuControl.DataContext.ToString() == "Open")
                {
                    AnimateMenuSliderFullClose();
                }
            }
        }

        private void AnimateMenuSliderFullClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 300,
                To = 0,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Close";
        }

        private void AnimateMenuSliderFullOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Open";
        }

        private void AnimateMenuSliderShortClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 300,
                To = 55,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Icons";
        }

        private void AnimateMenuSliderShortOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 55,
                To = 300,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Open";
        }

        private void AnimateMenuSliderIconClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 55,
                To = 0,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Close";
        }

        private void AnimateMenuSliderIconOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 0,
                To = 55,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Icons";
        }

        private void AnimateMenuSliderShortCloseClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 300,
                To = 55,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "IconsClose";
        }

        private void AnimateMenuSliderIconOpenOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 0,
                To = 55,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "IconsOpen";
        }

        private void MenuSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Toggle();
        }
    }
}
