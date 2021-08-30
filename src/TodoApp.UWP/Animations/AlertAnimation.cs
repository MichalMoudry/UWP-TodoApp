using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace TodoApp.Animations
{
    /// <summary>
    /// Class for working with animations of an alert element.
    /// </summary>
    public class AlertAnimation
    {
        /// <summary>
        /// Storyboarded animation.
        /// </summary>
        public Storyboard Animation { get; set; }

        /// <summary>
        /// Method for setting up storyboarded animation using easing function.
        /// </summary>
        /// <param name="targetName">Name of the target element.</param>
        /// <param name="property">Name of the target property.</param>
        public void SetupAnimation(string targetName, string property)
        {
            if (Animation == null)
            {
                Animation = new Storyboard();
            }
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = new Windows.UI.Xaml.Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(doubleAnimation, targetName);
            Storyboard.SetTargetProperty(doubleAnimation, property);
            Animation.Children.Add(doubleAnimation);
        }
    }
}
