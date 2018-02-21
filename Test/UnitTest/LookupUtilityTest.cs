using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using BizTalkComponents.Utilities.LookupUtility.Repository;

namespace BizTalkComponents.Utilities.LookupUtility.Test.UnitTest
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
            dictionary.Add("default", "DefaultValue");
            mock.Setup(s => s.LoadList(list, default(TimeSpan))).Returns(dictionary);
        }

        [TestMethod]
        public void TestHappyPath()
        {
            Assert.AreEqual("ConfigValue", util.GetValue(list, "ConfigKey"));
            mock.Verify(_util => _util.LoadList(list, default(TimeSpan)), Times.Once);
        }

        [TestMethod]
        public void TestCache()
        {
            util.GetValue(list, "ConfigKey");
            mock.Verify(_util => _util.LoadList(list, default(TimeSpan)), Times.Once);
        }

        [TestMethod]
        public void TestNonExistingWithoutException()
        {
            Assert.AreEqual(null, util.GetValue(list, "NonExistingConfigKey"));
        }

        [TestMethod]
        public void TestListDefaultValue()
        {
            Assert.AreEqual("DefaultValue", util.GetValue(list, "NonExistingConfigKey",false,true));
        }

        [TestMethod]
        public void TestNonExistingWithDefaultValue()
        {
            Assert.AreEqual("DefaultValue", util.GetValue(list, "NonExistingConfigKey","DefaultValue"));
        }

        [TestMethod]
        public void TestExistingWithDefaultValue()
        {
            Assert.AreEqual("ConfigValue", util.GetValue(list, "ConfigKey", "DefaultValue"));
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

        [TestMethod]
        public void TestAgeListOK()
        {
            var ts = new TimeSpan(0, 30, 0);
            util.GetValue(list, "ConfigKey", maxAge: ts);
        }
    }
}
