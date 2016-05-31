using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindInCn.Shared.Repositories.RemoteRepositories;

namespace FindInCn.Tests.Shops
{
    [TestClass]
    public class AliExpress
    {
        [TestMethod]
        public void AliExpressItemTest()
        {
            string dir = System.IO.Directory.GetCurrentDirectory();
            var shop = new RemoteRepository().GetRemoteShop("AliExpress");
            var item = shop.GetItem($@"file:\\{dir}\TestData\dwoudnt1.dah");
            Assert.AreEqual(item.ImageUrl, "http://g02.a.alicdn.com/kf/HTB1lHNGLXXXXXaYXFXXq6xXFXXXV/Милый-кот-чехол-для-Iphone-6-Iphone-6-6-s-Plus-5S-SE-чехол-Ripndipp-3D.jpg");
            Assert.AreEqual(item.Title, "Милый кот чехол для Iphone 6 / Iphone 6 6 s Plus / 5S SE чехол Ripndipp 3D животные мягкий кремния рок карманные котенок для Iphone6 купить на AliExpress");
        }
    }
}
