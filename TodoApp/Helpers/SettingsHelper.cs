using Microsoft.Toolkit.Uwp.Helpers;
using Windows.Storage;

namespace TodoApp.Helpers
{
    class SettingsHelper
    {
        private static SettingsHelper _instance;
        private ApplicationDataContainer _localSettings;
        private RoamingObjectStorageHelper _roamingSettings;

        protected SettingsHelper()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            _roamingSettings = new RoamingObjectStorageHelper();
        }

        public static SettingsHelper Instance()
        {
            if (_instance == null)
            {
                _instance = new SettingsHelper();
            }
            return _instance;
        }

        /// <summary>
        /// Read a value from local settings.
        /// </summary>
        /// <param name="key">Key to the value.</param>
        /// <returns>Value from local settings.</returns>
        public object ReadLocal(string key)
        {
            return _localSettings.Values[key];
        }

        /// <summary>
        /// Read a value from roaming settings.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key to the value.</param>
        /// <returns>Value from roaming settings.</returns>
        public T ReadRoaming<T>(string key)
        {
            return _roamingSettings.Read<T>(key);
        }

        /// <summary>
        /// Method for removing local setting.
        /// </summary>
        /// <param name="key">Key of the setting.</param>
        public void RemoveLocalSetting(string key)
        {
            _localSettings.Values.Remove(key);
        }

        /// <summary>
        /// Metod for saving value to local settings.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key to the value.</param>
        /// <param name="value">Value.</param>
        public void SaveAsLocal<T>(string key, T value)
        {
            _localSettings.Values[key] = value;
        }

        /// <summary>
        /// Metod for saving value to roaming settings.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key to the value.</param>
        /// <param name="value">Value.</param>
        public void SaveAsRoaming<T>(string key, T value)
        {
            _roamingSettings.Save(key, value);
        }

        /// <summary>
        /// Method for setting values of settings or handling them.
        /// </summary>
        public void SetInitialSettings()
        {

        }
    }
}
