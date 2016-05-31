using FindInCn.Shared.Repositories.RemoteRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Tests.Shops
{
    [TestClass]
    public class GearBest
    {
        [TestMethod]
        public void GearBestItemTest()
        {
            string dir = System.IO.Directory.GetCurrentDirectory();
            var shop = new RemoteRepository().GetRemoteShop("GearBest");
            var item = shop.GetItem($@"file:\\{dir}\TestData\qvgsp2dq.2x3");
            Assert.AreEqual(item.ImageUrl, "http://gloimg.gearbest.com/gb/pdm-product-pic/Electronic/2016/03/08/goods-img/1457456222172166057.jpg");
            Assert.AreEqual(item.Title, "Fat Cat 8 inch Flexible Octopus Tripod-6.12 and Free Shipping| GearBest.com");
        }
    }
}
