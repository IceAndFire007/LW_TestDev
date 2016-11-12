using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class LM_TestDev_WS_Test
    {
        #region -- Webservice initialisation


        private static LM_TestDev_WS_Reference.LM_TestDev_WSSoapClient _service;



        [ClassInitialize]
        public static void InitializeClass(TestContext ctx)
        {
            _service = new LM_TestDev_WS_Reference.LM_TestDev_WSSoapClient();
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            _service.Close();
        }
        #endregion

        [TestMethod]
        public void FibonacciTest()
        {
            Assert.AreEqual<decimal>(1, _service.Fibonacci(1), "Fibonacci(1) must to return 1");
            Assert.AreEqual<decimal>(1, _service.Fibonacci(2), "Fibonacci(2) must to return 1");
            Assert.AreEqual<decimal>(2, _service.Fibonacci(3), "Fibonacci(3) must to return 2");
            Assert.AreEqual<decimal>(3, _service.Fibonacci(4), "Fibonacci(4) must to return 3");
            Assert.AreEqual<decimal>(5, _service.Fibonacci(5), "Fibonacci(5) must to return 5");
            Assert.AreEqual<decimal>(8, _service.Fibonacci(6), "Fibonacci(6) must to return 8");
            Assert.AreEqual<decimal>(-1, _service.Fibonacci(-10), "Fibonacci(-10) must to return -1");
            Assert.AreEqual<decimal>(-1, _service.Fibonacci(1000), "Fibonacci(1000) must to return -1");
        }

        [TestMethod]
        public void XmlToJsonTest()
        {
            IDictionary<string, string> testValues = new Dictionary<string, string>();
            testValues.Add("<foo>bar</foo>", @"{
  ""foo"": ""bar""
}");
            testValues.Add("<foo>hello</bar>", "Bad Xml format");
            testValues.Add("<TRANS><HPAY><ID>103</ID><STATUS>3</STATUS><EXTRA><lS3DS>0</lS3DS><AUTH>031183</AUTH></EXTRA><INT_MSG/><MLABEL>501767XXXXXX6700</MLABEL><MTOKEN>project0l</MTOKEN></HPAY></TRANS>",
                @"{
  ""TRANS"": {
    ""HPAY"": {
      ""ID"": ""103"",
      ""STATUS"": ""3"",
      ""EXTRA"": {
        ""lS3DS"": ""0"",
        ""AUTH"": ""031183""
      },
      ""INT_MSG"": null,
      ""MLABEL"": ""501767XXXXXX6700"",
      ""MTOKEN"": ""project0l""
    }
  }
}");

            foreach (string input in testValues.Keys)
            {
                string expectedResult = testValues[input];
                Assert.AreEqual<string>(expectedResult, _service.XmlToJson(input));
            }
        }
    }
}
