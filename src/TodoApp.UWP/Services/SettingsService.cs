using Microsoft.Toolkit.Uwp.Helpers;
using Windows.Storage;

namespace TodoApp.Services
{
    public class SettingsService
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Read a value from local settings.
        /// </summary>
        /// <param name="key">Key to the value.</param>
        /// <returns>Value from local settings.</returns>
        public T Read<T>(string key) => (T)_localSettings.Values[key];

        /// <summary>
        /// Metod for saving value to local settings.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key to the value.</param>
        /// <param name="value">Value.</param>
        public void Save<T>(string key, T value) => _localSettings.Values[key] = value;

        /// <summary>
        /// Method for setting values of settings or handling them.
        /// </summary>
        public void SetInitialSettings()
        {
            if (SystemInformation.Instance.IsFirstRun)
            {
                Save("themeSetting", "default");
                Save("reminderSetting", true);
            }
        }
    }
}