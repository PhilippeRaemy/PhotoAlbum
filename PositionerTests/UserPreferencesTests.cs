

namespace PositionerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AlbumWordAddin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public  class UserPreferencesTests
    {
        [TestMethod]
        public  void TestReadUserPreferences()
        {
            var prefs=new UserPreferences(true);
        }

        [TestMethod]
        public void TestReadSaveRereadUserPreferences()
        {
            int margin;
            using (var prefs = new UserPreferences(true))
            {
                margin = prefs.Margin;
                prefs.Margin = margin + 1;
                Assert.AreEqual(margin + 1, prefs.Margin);
            }
            using (var prefs = new UserPreferences(true))
            {
                Assert.AreEqual(margin + 1, prefs.Margin);
                prefs.Margin = margin;
            }
            using (var prefs = new UserPreferences(true))
            {
                Assert.AreEqual(margin, prefs.Margin);
            }
        }
    }
}

