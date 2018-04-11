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

        public object ReadLocal(string key)
        {
            return _localSettings.Values[key];
        }

        public T ReadRoaming<T>(string key)
        {
            return _roamingSettings.Read<T>(key);
        }

        public void RemoveLocalSetting(string key)
        {
            _localSettings.Values.Remove(key);
        }

        public void SaveAsLocal<T>(string key, T value)
        {
            _localSettings.Values[key] = value;
        }

        public void SaveAsRoaming<T>(string key, T value)
        {
            _roamingSettings.Save(key, value);
        }

        public void SetInitialSettings()
        {

        }
    }
}
