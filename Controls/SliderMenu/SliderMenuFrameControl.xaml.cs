using VoterX.Utilities.Commands;
using VoterX.Utilities.Extensions;
using VoterX.Utilities.Models;
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

namespace VoterX.Utilities.Controls
{
    /// <summary>
    /// Interaction logic for SliderMenuFrameControl.xaml
    /// </summary>
    public partial class SliderMenuFrameControl : UserControl
    {
        private readonly int _fullWidth = 300;
        private readonly int _iconWidth = 55;
        private readonly int _closedWidth = 0;

        public SliderMenuFrameControl()
        {
            InitializeComponent();
        }

        // Public access to the Frame Control
        private Frame MenuFrame
        {
            get { return MenuContentFrame; }
            set { MenuContentFrame = value; }
        }

        public bool IsMenuSet
        {
            get
            {
                if (MenuContentFrame.Source != null) return true;
                else return false;
            }
        }

        public bool IsClickEnabled
        {
            get
            {
                return this.IsHitTestVisible;
            }
            set
            {
                this.IsHitTestVisible = value;
            }
        }

        /// <summary>
        /// Controls which menu animation behavior will be used
        /// </summary>
        public MenuCollapseMode CollapseMode { get; set; }

        // https://stackoverflow.com/questions/13447940/how-to-create-user-define-new-event-for-user-control-in-wpf-one-small-example/13450584
        private string _animationState;
        public string AnimationState
        {
            get
            {
                return _animationState;
            }

            set
            {
                _animationState = value;

                if (AnimationStateChanged != null)
                {
                    AnimationStateChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler AnimationStateChanged;

        //public static readonly DependencyProperty AnimationStateProperty = DependencyProperty.Register("AnimationState", typeof(string), typeof(SliderMenuFrameControl), new PropertyMetadata(OnAnimationStateChanged));

        //private static void OnAnimationStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    SliderMenuFrameControl c = d as SliderMenuFrameControl;

        //    c.RaiseAnimationStateChanged(e);
        //}

        //public event DependencyPropertyChangedEventHandler AnimationStateChanged;
        //protected virtual void RaiseAnimationStateChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    if (this.AnimationStateChanged != null)
        //    {
        //        this.AnimationStateChanged(this, e);
        //    }
        //}

        /// <summary>
        /// Sets the source page for the Frame
        /// </summary>
        /// <param name="pageUri"></param>
        public void SetMenuSource(System.Uri pageUri)
        {
            MenuContentFrame.Source = pageUri;
        }

        public void SetMenu(Page page, MenuCollapseMode mode)
        {
            MenuContentFrame.Navigate(page);
            Initialize(mode);
        }

        /// <summary>
        /// Hides or disables the menu
        /// </summary>
        public void Hide()
        {
            var test = AnimationState;
            if (AnimationState == "Close")
            {

            }
            else if (AnimationState == "Icons")
            {
                AnimateMenuSliderIconClose();
                AnimationState = "Close";
            }
            else if (AnimationState == "Open")
            {
                AnimateMenuSliderFullClose();
                AnimationState = "Close";
            }
        }

        /// <summary>
        /// Animates the menu to open based on the predetermined behavior
        /// </summary>
        public void Open()
        {
            // Opens the menu from fully closed or minimized positions
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (AnimationState == "Close")
                {
                    //AnimateMenuSliderFullOpen();
                    AnimateMenuSliderIconOpenOpen();
                }
                else if (AnimationState == "Icons")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderIconOpenOpen();
                }
                else if (AnimationState == "IconsOpen" ||
                    AnimationState == "IconsClose")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
                else if(AnimationState == "Open")
                {

                }
            }
        }

        public void Initialize() { Initialize(MenuCollapseMode.Full); }
        public void Initialize(MenuCollapseMode mode)
        {
            CollapseMode = mode;

            if(AnimationState == null)
            {
                AnimationState = "Close";
            }

            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderIconOpen();
                    AnimationState = "Icons";
                }
                else if (AnimationState == "Open")
                {
                    AnimateMenuSliderShortCloseClose();
                    AnimationState = "Icons";
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (AnimationState != "Close")
                {
                    AnimateMenuSliderFullClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (AnimationState != "Close")
                {
                    //AnimateMenuSliderFullClose();
                    AnimateMenuSliderAllClose();
                }
            }
        }

        public void Close()
        {
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (AnimationState == "Open")
                {
                    AnimateMenuSliderShortCloseClose();
                    AnimationState = "Icons";
                }
                else if (MenuControl.DataContext.ToString() == "Icons")
                {
                    //AnimateMenuSliderIconClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (AnimationState == "Open")
                {
                    //AnimateMenuSliderShortCloseClose();
                    AnimateMenuSliderAllClose();
                }
                else if (AnimationState == "IconsOpen")
                {
                    //AnimateMenuSliderIconOpen();
                    AnimateMenuSliderAllClose();
                }
                else if (AnimationState == "IconsClose")
                {
                    AnimateMenuSliderIconClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (AnimationState == "Open")
                {
                    AnimateMenuSliderFullClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.None)
            {
                AnimateMenuSliderAllClose();
            }
        }

        public void Toggle()
        {
            if (CollapseMode == MenuCollapseMode.ShowIcons)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
                else if (AnimationState == "Open")
                {
                    AnimateMenuSliderShortClose();
                }
                else if (AnimationState == "Icons")
                {
                    AnimateMenuSliderShortOpen();
                }
            }
            else if (CollapseMode == MenuCollapseMode.ThreeState)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderIconOpenOpen();
                }
                else if (AnimationState == "Open")
                {
                    AnimateMenuSliderShortCloseClose();
                }
                else if (AnimationState == "IconsOpen")
                {
                    AnimateMenuSliderShortOpen();
                }
                else if (AnimationState == "IconsClose")
                {
                    AnimateMenuSliderIconClose();
                }
            }
            else if (CollapseMode == MenuCollapseMode.Full)
            {
                if (AnimationState == "Close")
                {
                    AnimateMenuSliderFullOpen();
                }
                else if (AnimationState == "Open")
                {
                    AnimateMenuSliderFullClose();
                }
            }
        }

        // Any Position to Fully Closed
        private void AnimateMenuSliderAllClose()
        {
            int currentWidth = (int)MenuControl.Width;

            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = currentWidth,
                To = 0,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Close";
        }

        // Fully Open to Fully Closed
        private void AnimateMenuSliderFullClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _fullWidth,
                To = _closedWidth,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Close";
        }

        // Fully Closed to Fully Open
        private void AnimateMenuSliderFullOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _closedWidth,
                To = _fullWidth,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Open";
        }

        // Fully Open to Only Icons
        private void AnimateMenuSliderShortClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _fullWidth,
                To = _iconWidth,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Icons";
        }

        // Only Icons to Fully Open
        private void AnimateMenuSliderShortOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _iconWidth,
                To = _fullWidth,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Open";
        }

        // Only Icons to Fully Closed
        private void AnimateMenuSliderIconClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _iconWidth,
                To = _closedWidth,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Close";
        }

        // Fully Closed to Icons Only
        private void AnimateMenuSliderIconOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _closedWidth,
                To = _iconWidth,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "Open";
        }

        // Fully Open to Icons Only
        private void AnimateMenuSliderShortCloseClose()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _fullWidth,
                To = _iconWidth,
                Duration = TimeSpan.FromSeconds(.2)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "IconsClose";
        }

        // Fully Closed to Icons Only
        private void AnimateMenuSliderIconOpenOpen()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = _closedWidth,
                To = _iconWidth,
                Duration = TimeSpan.FromSeconds(.1)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Border.WidthProperty));
            Storyboard.SetTarget(widthAnimation, MenuControl);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();

            AnimationState = "IconsOpen";
        }

        private void MenuSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Close();
            Toggle();
        }

        //private void MenuControl_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    e.Handled = true;
        //}
    }
}
