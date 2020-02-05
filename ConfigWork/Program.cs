using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigWork
{
    class Program
    {
        static void Main(string[] args)
        {
            #region first
            //var settings = ConfigurationManager.AppSettings;
            //foreach (var key in settings.AllKeys)
            //{
            //    Console.WriteLine(settings.Get(key));
            //}
            #endregion

            #region second
            //var customValue = (ConfigurationManager.GetSection("customSection") as NameValueCollection)
            //        .Get("KeyFromCustomSection");
            //Console.WriteLine(customValue);
            #endregion

            #region third
            StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager
                .GetSection("StartupFolders");

            if(section != null)
            {
                Console.WriteLine( section.FolderItems[0].FolderType );
                Console.WriteLine( section.FolderItems[0].Path );
            }
            #endregion

            Console.ReadLine();
        }
    }

    public class StartupFoldersConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        public FoldersCollection FolderItems
        {
            get { return ( (FoldersCollection)(base["Folders"]) ); }
        }
    }

    [ConfigurationCollection(typeof(FolderElement))]
    public class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)(element)).FolderType;
        }

        public FolderElement this[int idx]
        {
            get { return (FolderElement)BaseGet(idx); }
        }
    }

    public class FolderElement : ConfigurationElement
    {

        [ConfigurationProperty("folderType", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string FolderType
        {
            get { return ((string)(base["folderType"])); }
            set { base["folderType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
