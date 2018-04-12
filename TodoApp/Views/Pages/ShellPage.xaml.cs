using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using TodoApp.Helpers;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TodoApp.Views.Pages
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            ChangeAppBarBackground();
            DisplayInformation.GetForCurrentView().OrientationChanged += ShellPage_OrientationChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mainFrame.Navigate(typeof(ListsPage));
        }

        /// <summary>
        /// Method for changing background color of status/title bar.
        /// </summary>
        private void ChangeAppBarBackground()
        {
            if (TitleBarExtensions.IsTitleBarSupported)
            {
                TitleBarExtensions.SetBackgroundColor(this, new SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#0063B1")).Color);
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                ApplicationViewTitleBar titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            else if (StatusBarExtensions.IsStatusBarSupported && !TitleBarExtensions.IsTitleBarSupported)
            {
                StatusBarExtensions.SetBackgroundColor(this, new SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#0063B1")).Color);
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Type pageType = e.SourcePageType;
            if (pageType.Equals(typeof(ListsPage)))
            {
                pageTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("TodoLists");
                pageSymbol.Glyph = "\uE179";
            }
            else if (pageType.Equals(typeof(TodoPage)))
            {
                pageTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("Todo");
                pageSymbol.Glyph = "\uE762";
            }
            else if (pageType.Equals(typeof(SettingsPage)))
            {
                pageTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("Settings");
                pageSymbol.Glyph = "\uE115";
            }
        }

        /// <summary>
        /// Display OrientationChanged event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void ShellPage_OrientationChanged(DisplayInformation sender, object args)
        {
            if (StatusBarExtensions.IsStatusBarSupported &&
               (sender.CurrentOrientation.Equals(DisplayOrientations.Landscape) || sender.CurrentOrientation.Equals(DisplayOrientations.LandscapeFlipped)))
            {
                StatusBarExtensions.SetIsVisible(this, false);
            }
            else if (StatusBarExtensions.IsStatusBarSupported)
            {
                StatusBarExtensions.SetIsVisible(this, true);
            }
        }
    }
}