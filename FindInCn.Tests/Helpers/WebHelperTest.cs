using FindInCn.Shared.Helpers;
using FindInCn.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Tests.Helpers
{
    [TestClass]
    public class WebHelperTest
    {
        [TestMethod]
        public void ReturnsValue()
        {
            string dir = System.IO.Directory.GetCurrentDirectory();
            string data = WebHelper.GetHttpResponse($@"file:\\{dir}\TestData\text.txt");
            Assert.AreEqual(data, "text123");
        }

    }
}
