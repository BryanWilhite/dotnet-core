using Microsoft.VisualStudio.TestTools.UnitTesting;
using Songhay.LightBulbs.Models;
using Songhay.LightBulbs.Models.Extensions;
using System;
using System.Linq;

namespace Songhay.LightBulbs.Tests
{
    [TestClass]
    public class ModelsExtensionsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty("numberOfLightBulbs","12")]
        [TestProperty("bulbsOnByDefault", "true")]
        public void ShouldGetRoomWithLightBulbs()
        {
            #region test properties:

            var numberOfLightBulbs = Convert.ToInt32(this.TestContext.Properties["numberOfLightBulbs"]);
            var bulbsOnByDefault = Convert.ToBoolean(this.TestContext.Properties["bulbsOnByDefault"]);

            #endregion

            var room = (new Room()).WithLightBulbs(numberOfLightBulbs, bulbsOnByDefault);

            Assert.IsNotNull(room, "The expected room is not here.");
            Assert.IsTrue(room.LightBulbs.Any(), "The expected light bulbs are not here.");
            Assert.IsTrue(room.LightBulbs.Count() == numberOfLightBulbs, "The expected number of light bulbs is not here.");
            Assert.IsTrue(room.LightBulbs.All(i=>i.IsOn), "The expected on/off state of light bulbs is not here.");
        }
    }
}
