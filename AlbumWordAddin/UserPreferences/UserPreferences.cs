namespace AlbumWordAddin.UserPreferences
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("UserPreferences")]
    public class UserPreferences
    {
        private string _folderImportStart;
        private string _folderImportEnd;
        private int _maxPicturesPerFile;
        private int _margin;
        private int _padding;
        protected bool Modified;
        private bool _confirmFileOverwrite;

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

        [XmlElement("Padding")]
        public int Padding
        {
            get { return _padding; }
            set { Modified = true; _padding = value; }
        }

        [XmlElement("ConfirmFileOverwrite")]
        public bool ConfirmFileOverwrite
        {
            get { return _confirmFileOverwrite; }
            set { Modified = true; _confirmFileOverwrite = value; }
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
