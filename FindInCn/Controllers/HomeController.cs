using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models;
using FindInCn.Shared.Models.DB;
using FindInCn.Shared.Repositories.RemoteRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindInCn.Controllers
{
    public class HomeController : Controller
    {
        RemoteRepository repository = new RemoteRepository();

        public ActionResult Index()
        {
            var c = new Shared.Models.DB.CnContext();
            return View();
        }

        public ActionResult Search(string q)
        {
            var shops = repository.GetRemoteShops();
            var result = RemoteHelper.Search(shops, new SearchOptions() { Name = q });

            return View(result.ToArray());
        }

        public ActionResult StoreInfo(int id)
        {
            var shop = repository.GetRemoteShop(id);
            ViewBag.ShowCategories = false;
            return View(shop);
        }
    }
}