using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models;
using FindInCn.Shared.Models.DB;
using FindInCn.Shared.Models.Remote;
using FindInCn.Shared.Repositories.DbRepositories;
using FindInCn.Shared.Repositories.RemoteRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

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

        public ActionResult Search(string q, string sort)
        {
            ViewBag.q = System.Net.WebUtility.HtmlEncode(q);
            ViewBag.sort = sort;
            return View();
        }

        public PartialViewResult AjaxSearch(string q, string sort)
        {
            var shops = remoteRepository.GetRemoteShops();
            var result = RemoteHelper.Search(shops, new SearchOptions() { Name = q });
            if (sort != null && sort != string.Empty)
            {
                result = result.OrderBy(i=>i.Name);
            }

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
            if (user == null)
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

            var repo = new RemoteRepository();
            var dbFavorites = accountRepository.GetFavoritesByUserIdAsQueryable(user.Id).ToArray();

            //TODO performance -- every time creating shops 
            RemoteItemDetails[] fmodel = dbFavorites.Select(i => repo.GetRemoteShop(i.ShopId).GetItem(i.ItemUrl)).ToArray();
            return View(fmodel);
        }

        public ActionResult ItemDetails()
        {
            return View();
        }

        public ActionResult AdvancedSearch()
        {
            return View();
        }

        public void DeleteFromFavorites(string url)
        {
            var user = Session["user"] as User;
            if (user == null)
            {
                return;
            }

            var repo = new RemoteRepository();
            accountRepository.RemoveFromFavorites(user.Id, url);
        }

        public ActionResult Compare(int id1, string url1, int id2, string url2)
        {
            var repo = new RemoteRepository();
            var item1 = repo.GetRemoteShop(id1).GetItem(url1);
            var item2 = repo.GetRemoteShop(id2).GetItem(url2);

            ViewBag.I1 = item1;
            ViewBag.I2 = item2;

            var p1 = item1.Properties;
            var p2 = item2.Properties;
            var result = new List<Tuple<string, string, string>>();
            foreach (var i in p1)
            {
                var item = new Tuple<string, string, string>(
                    i.Name, i.Value, p2.FirstOrDefault(j => i.Name == j.Name)?.Value);
                result.Add(item);
            }

            result.AddRange(p2.Where(i => p1.FirstOrDefault(j => i.Name == j.Name) == null)
                .Select(i => new Tuple<string, string, string>(i.Name, null, i.Value)));

            return View(result.OrderBy(i => i.Item2 == null || i.Item3 == null));
        }
    }
}