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
        [TestProperty("bulbsOnByDefault", "true")]
        [TestProperty("numberOfLightBulbs", "12")]
        public void ShouldGetRoomWithLightBulbs()
        {
            #region test properties:

            var bulbsOnByDefault = Convert.ToBoolean(this.TestContext.Properties["bulbsOnByDefault"]);
            var numberOfLightBulbs = Convert.ToInt32(this.TestContext.Properties["numberOfLightBulbs"]);

            #endregion

            var room = (new Room()).WithLightBulbs(numberOfLightBulbs, bulbsOnByDefault);

            Assert.IsNotNull(room, "The expected room is not here.");
            Assert.IsTrue(room.LightBulbs.Any(), "The expected light bulbs are not here.");
            Assert.IsTrue(room.LightBulbs.Count() == numberOfLightBulbs, "The expected number of light bulbs is not here.");
            Assert.IsTrue(room.LightBulbs.All(i => i.IsOn), "The expected on/off state of light bulbs is not here.");
        }

        [TestMethod]
        [TestProperty("numberOfPersons", "8")]
        public void ShouldGetRoomWithPersons()
        {
            #region test properties:

            var numberOfPersons = Convert.ToInt32(this.TestContext.Properties["numberOfPersons"]);

            #endregion

            var room = (new Room()).WithPersons(numberOfPersons);

            Assert.IsNotNull(room, "The expected room is not here.");
            Assert.IsTrue(room.Persons.Any(), "The expected persons are not here.");
            Assert.IsTrue(room.Persons.Count() == numberOfPersons, "The expected number of persons is not here.");
        }

        [TestMethod]
        [TestProperty("bulbsOnByDefault", "true")]
        [TestProperty("numberOfLightBulbs", "12")]
        [TestProperty("numberOfPersons", "8")]
        [TestProperty("expectedState", @"{ ""lightsOn"": [4,9] }")]
        public void ShouldSwitchWithAllPersons()
        {
            #region test properties:

            var bulbsOnByDefault = Convert.ToBoolean(this.TestContext.Properties["bulbsOnByDefault"]);
            var expectedState = JObject.Parse(this.TestContext.Properties["expectedState"].ToString());
            var numberOfLightBulbs = Convert.ToInt32(this.TestContext.Properties["numberOfLightBulbs"]);
            var numberOfPersons = Convert.ToInt32(this.TestContext.Properties["numberOfPersons"]);

            #endregion

            var room = (new Room())
                .WithLightBulbs(numberOfLightBulbs, bulbsOnByDefault)
                .WithPersons(numberOfPersons);

            Assert.IsNotNull(room, "The expected room is not here.");

            room.SwitchWithAllPersons();
        }
    }
}
