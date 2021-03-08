using System;
using System.IO;

namespace UserPreferences
{
    public class PersistedUserPreferences: UserPreferences
    {
        static UserPreferences _userPreferences;

        static readonly string PrefFileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AlbumWordAddin",
            "UserPreferences.xml"
        );

        public PersistedUserPreferences(): this(false){}

        public PersistedUserPreferences(bool forceRead)
        {
            if (forceRead || _userPreferences == null)
            {
                var file = new FileInfo(PrefFileName);
                // ReSharper disable once AssignNullToNotNullAttribute
                new DirectoryInfo(file.DirectoryName).Create();
                if (!file.Exists) return;
                var reader = new StreamReader(PrefFileName);
                _userPreferences = reader.ReadToEnd().Deserialize<UserPreferences>();
                reader.Close();
            }
            CopyUserPreferences(_userPreferences, this);
        }

        static UserPreferences CopyUserPreferences(UserPreferences source, UserPreferences target)
        {
            foreach (var prop in typeof(UserPreferences).GetProperties())
            {
                prop.SetValue(target, prop.GetValue(source));
            }
            return target;
        }

        public void Save()
        {
            if (!Modified) return;
            var writer = new StreamWriter(PrefFileName);
            writer.Write(CopyUserPreferences(this, _userPreferences).Serialize());
            writer.Close();
            Modified = false;
        }
    }
}