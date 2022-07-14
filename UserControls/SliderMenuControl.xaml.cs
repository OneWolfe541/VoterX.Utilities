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

namespace VoterX.Utilities.UserControls
{
    public enum MenuCollapseMode
    {
        None,
        ShowIcons,
        Full,
        ThreeState
    }

    /// <summary>
    /// Menu Slider using the Content Presenter Control
    /// </summary>
    public partial class SliderMenuControl : UserControl
    {
        public SliderMenuControl()
        {
            InitializeComponent();
            //if (CollapseMode == null) CollapseMode = MenuCollapseMode.None;
        }

        public MenuCollapseMode CollapseMode { get; set; }

        /// <summary>
        /// Gets or sets additional content for the UserControl
        /// </summary>
        public object TopContent
        {
            get { return (object)GetValue(TopContentProperty); }
            set { SetValue(TopContentProperty, value); }
        }
        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.Register("TopContent", typeof(object), typeof(SliderMenuControl),
              new PropertyMetadata(null));

        public object MiddleContent
        {
            get { return (object)GetValue(MiddleContentProperty); }
            set { SetValue(MiddleContentProperty, value); }
        }
        public static readonly DependencyProperty MiddleContentProperty =
            DependencyProperty.Register("MiddleContent", typeof(object), typeof(SliderMenuControl),
              new PropertyMetadata(null));

        public object BottomContent
        {
            get { return (object)GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(SliderMenuControl),
              new PropertyMetadata(null));

        public void ShowTopContent()
        {
            TopContentControl.Visibility = Visibility.Visible;
        }

        public void HideTopContent()
        {
            TopContentControl.Visibility = Visibility.Collapsed;
        }

        public void ShowMiddleContent()
        {
            MiddleContentControl.Visibility = Visibility.Visible;
        }

        public void HideMiddleContent()
        {
            MiddleContentControl.Visibility = Visibility.Collapsed;
        }

        public void ShowBottomContent()
        {
            BottomContentControl.Visibility = Visibility.Visible;
        }

        public void HideBottomContent()
        {
            BottomContentControl.Visibility = Visibility.Collapsed;
        }

        private void Hide()
        {
            MenuControl.Width = 0;
        }

        public void Open()
        {
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (MenuControl.DataContext.ToString() == "Close")
                {
                    AnimateMenuSliderFullOpen();
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
                To = 45,
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
                From = 45,
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
                From = 45,
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
                To = 45,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "Open";
        }

        private void AnimateMenuSliderShortCloseClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 300,
                To = 45,
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
                To = 45,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            MenuControl.DataContext = "IconsOpen";
        }
    }
}
