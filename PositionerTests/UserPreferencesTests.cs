

namespace PositionerTests
{
    using AlbumWordAddin.UserPreferences;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public  class UserPreferencesTests
    {
        [TestMethod]
        public  void TestReadUserPreferences()
        {
            var prefs=new PersistedUserPreferences();
        }

        [TestMethod]
        public void TestReadSaveRereadUserPreferences()
        {
            int margin;
            {
                var prefs = new PersistedUserPreferences();
                margin = prefs.Margin;
                prefs.Margin = margin + 1;
                Assert.AreEqual(margin + 1, prefs.Margin, "Set Value");
                prefs.Save();
            }
            {
                var prefs = new PersistedUserPreferences(true);
                Assert.AreEqual(margin + 1, prefs.Margin, "Reread Value");
                prefs.Margin = margin;
                prefs.Save();
            }
            {
                var prefs = new PersistedUserPreferences(true);
                Assert.AreEqual(margin, prefs.Margin, "Reset Value");
            }
        }
    }
}

