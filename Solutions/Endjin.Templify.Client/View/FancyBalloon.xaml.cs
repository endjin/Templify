namespace Endjin.Templify.Client.View
{
    #region Using Directives

    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    using Hardcodet.Wpf.TaskbarNotification;

    #endregion

    /// <summary>
    /// Interaction logic for FancyBalloon.xaml
    /// </summary>
    public partial class FancyBalloon : UserControl
    {
        private bool isClosing;

        /// <summary>
        /// Description
        /// </summary>
        public static readonly DependencyProperty TextBodyProperty =
            DependencyProperty.Register("TextBody",
                                        typeof(string),
                                        typeof(FancyBalloon),
                                        new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty TextTitleProperty =
            DependencyProperty.Register("TextTitle",
                                        typeof(string),
                                        typeof(FancyBalloon),
                                        new FrameworkPropertyMetadata(""));

        /// <summary>
        /// A property wrapper for the <see cref="BalloonTextProperty"/>
        /// dependency property:<br/>
        /// Description
        /// </summary>
        public string TextBody
        {
            get { return (string)GetValue(TextBodyProperty); }
            set { SetValue(TextBodyProperty, value); }
        }

        public string TextTitle
        {
            get { return (string)GetValue(TextTitleProperty); }
            set { SetValue(TextTitleProperty, value); }
        }

        public FancyBalloon()
        {
            InitializeComponent();
            TaskbarIcon.AddBalloonClosingHandler(this, this.OnBalloonClosing);
        }


        /// <summary>
        /// By subscribing to the <see cref="TaskbarIcon.BalloonClosingEvent"/>
        /// and setting the "Handled" property to true, we suppress the popup
        /// from being closed in order to display the fade-out animation.
        /// </summary>
        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.isClosing = true;
        }


        /// <summary>
        /// Resolves the <see cref="TaskbarIcon"/> that displayed
        /// the balloon and requests a close action.
        /// </summary>
        private void OnCloseButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            // the tray icon assigned this attached property to simplify access
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// If the users hovers over the balloon, we don't close it.
        /// </summary>
        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            // if we're already running the fade-out animation, do not interrupt anymore
            // (makes things too complicated for the sample)
            if (this.isClosing)
            {
                return;
            }

            // the tray icon assigned this attached property to simplify access
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.ResetBalloonCloseTimer();
        }


        /// <summary>
        /// Closes the popup once the fade-out animation completed.
        /// The animation was triggered in XAML through the attached
        /// BalloonClosing event.
        /// </summary>
        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            var pp = (Popup)Parent;
            pp.IsOpen = false;
        }
    }
}