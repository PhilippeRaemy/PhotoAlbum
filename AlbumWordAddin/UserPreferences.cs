using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AlbumWordAddin
{
    using System;

    [XmlRoot("UserPreferences")]
    public class UserPreferences:IDisposable
    {
        public UserPreferences(bool fromConfig = true)
        {
            
        }


        [XmlElement("FolderImportStart" )] public static string FolderImportStart  { get; set; }
        [XmlElement("FolderImportEnd"   )] public static string FolderImportEnd    { get; set; }
        [XmlElement("MaxPicturesPerFile")] public static int    MaxPicturesPerFile { get; set; }
        [XmlElement("Margin"            )] public static int    Margin             { get; set; }
        [XmlElement("Padding"           )] public static int    Padding            { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
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
