using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TodoApp.Services;

namespace TodoApp.Controls
{
    /// <summary>
    /// User control with app settings.
    /// </summary>
    public sealed partial class SettingsControl : UserControl
    {
        /// <summary>
        /// Private read-only field with <see cref="SettingsService"/> instance.
        /// </summary>
        private readonly SettingsService _settingsService;

        /// <summary>
        /// Private field with app theme value.
        /// </summary>
        private string _appTheme;

        public SettingsControl(SettingsService settingsService)
        {
            InitializeComponent();
            _settingsService = settingsService;
            _appTheme = _settingsService.Read<string>("themeSetting");
            if (_appTheme.Equals("default"))
            {
                RequestedTheme = ElementTheme.Default;
                appThemeComboBox.SelectedIndex = 2;
            }
            else if (_appTheme.Equals("light"))
            {
                RequestedTheme = ElementTheme.Light;
                appThemeComboBox.SelectedIndex = 1;
            }
            else if (_appTheme.Equals("dark"))
            {
                RequestedTheme = ElementTheme.Dark;
                appThemeComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Event handler for AppThemeComboBox combobox SelectionChanged event.
        /// </summary>
        /// <param name="sender">AppThemeComboBox combobox.</param>
        /// <param name="e">Event parameters.</param>
        private void AppThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string newThemeValue = (appThemeComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            if (!_appTheme.Equals(newThemeValue.ToLower()))
            {
                newThemeValue = newThemeValue.Equals("Use system setting") ? "default" : newThemeValue.ToLower();
                _settingsService.Save("themeSetting", newThemeValue);
            }
        }
    }
}
