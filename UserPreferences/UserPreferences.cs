namespace AlbumWordAddin.UserPreferences
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("UserPreferences")]
    public class UserPreferences
    {
        string _folderImportStart;
        string _folderImportEnd;
        int _maxPicturesPerFile;
        int _margin;
        int _spacing;
        protected bool Modified;
        bool _confirmFileOverwrite;
        string _includeFiles;
        string _excludeFolders;
        string _newDocumentTemplate;

        [XmlElement("FolderImportStart")]
        public string FolderImportStart
        {
            get { return _folderImportStart; }
            set { Modified = true; _folderImportStart = value; }
        }

        [XmlElement("FolderImportEnd")]
        public string FolderImportEnd
        {
            get { return _folderImportEnd; }
            set { Modified = true; _folderImportEnd = value; }
        }

        [XmlElement("MaxPicturesPerFile")]
        public int MaxPicturesPerFile
        {
            get { return _maxPicturesPerFile; }
            set { Modified = true; _maxPicturesPerFile = value; }
        }

        [XmlElement("Margin")]
        public int Margin
        {
            get { return _margin; }
            set { Modified = true; _margin = value; }
        }

        [XmlElement("Spacing")]
        public int Spacing
        {
            get { return _spacing; }
            set { Modified = true; _spacing = value; }
        }

        [XmlElement("ConfirmFileOverwrite")]
        public bool ConfirmFileOverwrite
        {
            get { return _confirmFileOverwrite; }
            set { Modified = true; _confirmFileOverwrite = value; }
        }

        [XmlElement("IncludeFiles")]
        public string IncludeFiles
        {
            get { return _includeFiles; }
            set { Modified = true; _includeFiles = value; }
        }

        [XmlElement("ExcludeFolders")]
        public string ExcludeFolders
        {
            get { return _excludeFolders; }
            set { Modified = true; _excludeFolders = value; }
        }

        [XmlElement("NewDocumentTemplate")]
        public string NewDocumentTemplate
        {
            get { return _newDocumentTemplate; }
            set { Modified = true; _newDocumentTemplate = value; }
        }
    }

    public static class ObjectSerializationExtensions
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static T Deserialize<T>(this string xmltext) where T : new()
        {
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                return (T) xmlserializer.Deserialize(new StringReader(xmltext));
            }
            catch
            {
                return new T();
            }
        }
    }
}
