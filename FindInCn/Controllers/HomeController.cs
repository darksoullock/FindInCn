using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models;
using FindInCn.Shared.Models.DB;
using FindInCn.Shared.Repositories.DbRepositories;
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
        RemoteRepository remoteRepository = new RemoteRepository();
        AccountRepository accountRepository = new AccountRepository();


        public ActionResult Index()
        {
            var c = new Shared.Models.DB.CnContext();
            return View();
        }

        public ActionResult Search(string q)
        {
            ViewBag.q = System.Net.WebUtility.HtmlEncode(q);
            return View();
        }

        public ActionResult AjaxSearch(string q)
        {
            var shops = remoteRepository.GetRemoteShops();
            var result = RemoteHelper.Search(shops, new SearchOptions() { Name = q });

            return PartialView(result.ToArray());
        }

        public ActionResult StoreInfo(int id)
        {
            var shop = remoteRepository.GetRemoteShop(id);
            return View(shop);
        }

        public string AddToFavorites(string url, int shopId)
        {
            var user = Session["user"] as User;
            if (user==null)
            {
                return "noauth";
            }

            var item = new FavoriteItem() { ItemUrl = url, UserId = user.Id, ShopId = shopId };
            accountRepository.AddToFavorite(item);
            return "ok";
        }

        public ActionResult Favorites()
        {
            var user = Session["user"] as User;
            if (user == null)
            {
                return Redirect("/");
            }

            return View(accountRepository.GetFavoritesByUserIdAsQueryable(user.Id));
        }
    }
}