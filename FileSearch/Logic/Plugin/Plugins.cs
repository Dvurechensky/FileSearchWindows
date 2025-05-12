/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using System.Reflection;

namespace FileSearch.Logic.Plugin
{
    internal static class Plugins
    {
        private static IPluginFacade[] _plugins;

        public static IPluginFacade[] All()
        {
            return _plugins ?? (_plugins = DetectAll());
        }

        public static IEnumerable<IPluginFacade> Loaded()
        {
            return All().Where(p => p.PluginId != Guid.Empty);
        }

        private static IPluginFacade[] DetectAll()
        {
            var collection = new List<IPluginFacade>();
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            if (path == null) return new IPluginFacade[0];

            foreach (var file in new DirectoryInfo(path).GetFiles("FileSearch.Plugin.*.dll", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var assembly = Assembly.LoadFile(file.FullName);
                    var facades = assembly.GetTypes().Where(t => typeof(IPluginFacade).IsAssignableFrom(t));
                    foreach (var facade in facades)
                    {
                        var instance = (IPluginFacade)Activator.CreateInstance(facade);

                        var duplicatePluginId = collection.FirstOrDefault(p => p.PluginId == instance.PluginId);
                        if (duplicatePluginId != null)
                            collection.Add(new ErrorPlugin(instance.TabTitle, "The plugin ID is already registered by another plugin named '" + duplicatePluginId.TabTitle + "'."));
                        else
                            collection.Add(instance);
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    collection.Add(new ErrorPlugin(Path.GetFileNameWithoutExtension(file.FullName), ex.Message));
                }
                catch (Exception ex)
                {
                    collection.Add(new ErrorPlugin(Path.GetFileNameWithoutExtension(file.FullName), ex.Message));
                }
            }

            return collection.ToArray();
        }
    }
}
