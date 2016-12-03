

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
            using (var prefs = new PersistedUserPreferences())
            {
                margin = prefs.Margin;
                prefs.Margin = margin + 1;
                Assert.AreEqual(margin + 1, prefs.Margin, "Set Value");
            }
            using (var prefs = new PersistedUserPreferences())
            {
                Assert.AreEqual(margin + 1, prefs.Margin, "Reread Value");
                prefs.Margin = margin;
            }
            using (var prefs = new PersistedUserPreferences())
            {
                Assert.AreEqual(margin, prefs.Margin, "Reset Value");
            }
        }
    }
}

