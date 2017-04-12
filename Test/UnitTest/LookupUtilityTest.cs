using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Shared.Utilities.LookupUtility.Test.UnitTest
{
    [TestClass]
    public class LookupUtilityTest
    {
        private static LookupUtilityService util;
        private static Mock<ILookupRepository> mock;
        private static string list = "MyList";

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            
            mock = new Mock<ILookupRepository>();
            util = new LookupUtilityService(mock.Object);
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("ConfigKey", "ConfigValue");

            mock.Setup(s => s.LoadList(list)).Returns(dictionary);
        }

        [TestMethod]
        public void TestHappyPath()
        {
            Assert.AreEqual("ConfigValue", util.GetValue(list, "ConfigKey"));
            mock.Verify(util => util.LoadList(list), Times.Once);
        }

        [TestMethod]
        public void TestCache()
        {
            util.GetValue(list, "ConfigKey");
            mock.Verify(util => util.LoadList(list), Times.Once);
        }

        [TestMethod]
        public void TestNonExistingWithoutException()
        {
            Assert.AreEqual(null, util.GetValue(list, "NonExistingConfigKey"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNonExistingWithException()
        {
            util.GetValue(list, "NonExistingConfigKey", true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNonExistingList()
        {
            util.GetValue("NonExistingList", "ConfigKey");
        }
    }
}
