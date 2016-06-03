using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using XML.serializaton;

namespace XML.serializaton
{
    public static class PluginLoader
    {

        public static ICollection<IPlugin> LoadPlugins(string path, List<string> FileNames, List<string> loadPlugin)
        {
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
               // Form1 form = new Form1();
                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    string dllfile = dllFile.Substring(path.Length);
                    if ((FileNames.Contains(dllfile)) && !(loadPlugin.Contains(dllfile)))
                    {
                        AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                        Assembly assembly = Assembly.Load(an);//loads an assembly
                        assemblies.Add(assembly);
                        loadPlugin.Add(dllfile);
                    }
                }

                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes(); //Gets the types defined in this assembly.

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
                

                ICollection<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count );
                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);//Creates an instance of the specified type using the constructor that best matches the specified parameters
                    plugins.Add(plugin);
                }
                
                return plugins;                
            }
            

            return null;
        }
    }
}
