using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace TodoApp.Services
{
    /// <summary>
    /// Service class for handling of navigation within this app (only one frame -> rootFrame).
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Root frame of this app.
        /// </summary>
        public Frame Frame { private get; set; }

        public void GoBack()
        {
            Frame.GoBack();
        }

        /// <summary>
        /// Method for navigating to a new page.
        /// </summary>
        /// <param name="page">Type of the page.</param>
        /// <param name="parameters">Parmeters that will be passed to the page. Optional parameter.</param>
        /// <param name="infoOverride">Optional page transition.</param>
        public void Navigate(Type page, object parameters = null, NavigationTransitionInfo infoOverride = null)
        {
            _ = Frame.Navigate(page, parameters, infoOverride);
        }
    }
}