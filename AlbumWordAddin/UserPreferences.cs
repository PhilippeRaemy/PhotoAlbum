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
            var file=new FileInfo(_prefFileName);
            // ReSharper disable once AssignNullToNotNullAttribute
            new DirectoryInfo(file.DirectoryName).Create();
            if (!file.Exists) return;
            var reader = new StreamReader(_prefFileName);
            var prefs = reader.ReadToEnd().Deserialize<UserPreferences>();
            reader.Close();
            foreach (var prop in typeof(UserPreferences).GetProperties() )
            {
                prop.SetValue(this, prop.GetValue(prefs));
            }
        }

        [XmlElement("FolderImportStart" )] public string FolderImportStart  { get; set; }
        [XmlElement("FolderImportEnd"   )] public string FolderImportEnd    { get; set; }
        [XmlElement("MaxPicturesPerFile")] public int    MaxPicturesPerFile { get; set; }
        [XmlElement("Margin"            )] public int    Margin             { get; set; }
        [XmlElement("Padding"           )] public int    Padding            { get; set; }
        public void Dispose()
        {
            if (!_fromConfig) return;
            var writer = new StreamWriter(_prefFileName);
            writer.Write(this.Serialize());
            writer.Close();
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
