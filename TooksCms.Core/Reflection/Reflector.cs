using System;
using System.Reflection;

namespace TooksCms.Core.Reflection
{
    public static class Reflector
    {

        /// <summary>
        /// Loads and returns an instance of the specified class.
        /// The method expects to find the Assembly in the Bin directory of the application.
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static object CreateObject(string assemblyname, string classname)
        {
            AssemblyName name = new AssemblyName(assemblyname);
            Assembly assembly = Assembly.Load(name);

            try
            {
                object obj = assembly.CreateInstance(classname, true,
                                                     BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance,
                                                     null, null, null, null);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading dynamic assembly or class", ex);
            }
        }

        /// <summary>
        /// Loads and returns an instance of the specified class.
        /// The method expects to find the Assembly in the Bin directory of the application.
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static object CreateObject(string assemblyname, string classname, object[] args)
        {
            AssemblyName name = new AssemblyName(assemblyname);
            Assembly assembly = Assembly.Load(name);

            try
            {
                object obj = assembly.CreateInstance(classname, true,
                                                     BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance,
                                                     null, args, null, null);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading dynamic assembly or class", ex);
            }
        }



        /// <summary>
        /// Loads and returns an instance of the specified class from 
        /// specified Assembly Folder path in registry.
        /// The method expects to find the Assembly in the specified directory.
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="assemblyname"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static object CreateObject(string assemblyPath, string assemblyname, string classname)
        {
            AssemblyName name = new AssemblyName(assemblyname);

            string assemblyToBeLoaded = assemblyPath + "\\" + name + ".dll";
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyToBeLoaded);
                object obj = assembly.CreateInstance(classname, true,
                                                     BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance,
                                                     null, null, null, null);

                return obj;
            }
            catch
            {
                //throw new Exception("Error loading dynamic assembly or class", ex);
                //we are forcing assembly to load from bin in case its fails to load
                //from Assembly Folder Path.
                return CreateObject(assemblyname, classname);
            }
        }
    }
}
