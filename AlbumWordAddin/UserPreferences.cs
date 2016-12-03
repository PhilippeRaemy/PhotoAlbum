using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AlbumWordAddin
{
    using System;
    using System.Reflection;

    [XmlRoot("UserPreferences")]
    public class UserPreferences:IDisposable
    {
        readonly bool _fromConfig;

        readonly string _prefFileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AlbumWordAddin",
            "UserPreferences.xml"
        );

        public UserPreferences()
        {
        }

        public UserPreferences(bool fromConfig = true)
        {
            _fromConfig = fromConfig;
            if (!_fromConfig) return;
            var prefs = new StreamReader(_prefFileName).Deserialize<UserPreferences>();
            foreach (var prop in typeof(UserPreferences).GetProperties(BindingFlags.Public) )
            {
                prop.SetValue(this, prop.GetValue(prefs));
            }
        }

        [XmlElement("FolderImportStart" )] public static string FolderImportStart  { get; set; }
        [XmlElement("FolderImportEnd"   )] public static string FolderImportEnd    { get; set; }
        [XmlElement("MaxPicturesPerFile")] public static int    MaxPicturesPerFile { get; set; }
        [XmlElement("Margin"            )] public static int    Margin             { get; set; }
        [XmlElement("Padding"           )] public static int    Padding            { get; set; }
        public void Dispose()
        {
            if (!_fromConfig) return;

            new StreamWriter(_prefFileName).Write(this.Serialize());
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

        public static T Deserialize<T>(this StreamReader stream) where T : new()
        {
            var xmlserializer = new XmlSerializer(typeof(T));
            return (T) xmlserializer.Deserialize(stream);
        }
    }
}
