namespace AlbumWordAddin.UserPreferences
{
    using System;
    using System.IO;

    public class PersistedUserPreferences: UserPreferences, IDisposable
    {
        readonly string _prefFileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AlbumWordAddin",
            "UserPreferences.xml"
        );

        public PersistedUserPreferences()
        {
            var file=new FileInfo(_prefFileName);
            // ReSharper disable once AssignNullToNotNullAttribute
            new DirectoryInfo(file.DirectoryName).Create();
            if (!file.Exists) return;
            var reader = new StreamReader(_prefFileName);
            var prefs = reader.ReadToEnd().Deserialize<UserPreferences>();
            reader.Close();
            CopyUserPreferences(prefs, this);
        }

        public static UserPreferences CopyUserPreferences(UserPreferences source, UserPreferences target)
        {
            foreach (var prop in typeof(UserPreferences).GetProperties())
            {
                prop.SetValue(target, prop.GetValue(source));
            }
            return target;
        }

        public void Dispose()
        {
            if (!Modified) return;
            var writer = new StreamWriter(_prefFileName);
            writer.Write(CopyUserPreferences(this, new UserPreferences()).Serialize());
            writer.Close();
        }
    }
}