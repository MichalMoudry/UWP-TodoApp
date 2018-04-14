using Windows.ApplicationModel.Resources;

namespace TodoApp.Helpers
{
    internal class ResourceLoaderHelper
    {
        /// <summary>
        /// Method for obtaining instance of ResourceLoader class for view independent use.
        /// </summary>
        /// <returns>Instance of ResourceLoader class.</returns>
        public static ResourceLoader GetResourceLoader()
        {
            return ResourceLoader.GetForViewIndependentUse("Resources");
        }
    }
}