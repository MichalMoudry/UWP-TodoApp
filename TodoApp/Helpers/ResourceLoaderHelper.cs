using Windows.ApplicationModel.Resources;

namespace TodoApp.Helpers
{
    internal class ResourceLoaderHelper
    {
        public static ResourceLoader GetResourceLoader()
        {
            return ResourceLoader.GetForViewIndependentUse("Resources");
        }
    }
}