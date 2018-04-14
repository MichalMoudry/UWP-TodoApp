﻿using Microsoft.Toolkit.Uwp.Helpers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TodoApp.Views.UserControls
{
    public sealed partial class InAppNotification : UserControl
    {
        private DispatcherTimer _timer;

        public InAppNotification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for collapsing notification.
        /// </summary>
        public void Hide()
        {
            FadingAnimation.Begin();
            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// Method for displaying In-app notification.
        /// </summary>
        /// <param name="content">Content to be displayed.</param>
        /// <param name="color">Color of the notification.</param>
        public void Show(string content, string color)
        {
            Visibility = Visibility.Visible;
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 6)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            DisplayAnimation.Begin();
            NotificationContentGrid.Background = new SolidColorBrush(ColorHelper.ToColor(color));
            NotificationContent.Text = content;
        }

        /// <summary>
        /// CloseButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Fading animation Completed event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void FadingAnimation_Completed(object sender, object e)
        {
            Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Tick event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments</param>
        private void Timer_Tick(object sender, object e)
        {
            Hide();
            (sender as DispatcherTimer).Stop();
        }
    }
}