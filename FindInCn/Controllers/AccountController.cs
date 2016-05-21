using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models.DB;
using FindInCn.Shared.Repositories.DbRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FindInCn.Controllers
{
    public class AccountController : Controller
    {
        public string AjaxLogin(string email, string password)
        {
            AccountRepository _db = new AccountRepository();
            var user = _db.GetUserByEmail(email);
            var jss = new JavaScriptSerializer();
            if (user == null)
            {
                return jss.Serialize(new { status = "noemail" });
            }

            if (password == null || password == string.Empty)
            {
                var pass = Guid.NewGuid().ToString();
                MailHelper.SendPasswd(email, pass);
                user.Password = pass;
                var passUntil = DateTime.Now.AddMinutes(10);
                _db.Save();
                return jss.Serialize(new { status = "sent" });
            }

            if (user.Password == password && user.PassExpiration > DateTime.Now)
            {
                Session["User"] = user;
                var result = jss.Serialize(new { status = "ok", username = user.Name });
                return result;
            }
            else
            {
                return jss.Serialize(new { status = "nopass" });
            }
        }

        public ActionResult ConfirmRegistration(int id, string key)
        {
            AccountRepository _db = new AccountRepository();
            var user = _db.GetUserById(id);
            if (user.Password == key && user.PassExpiration > DateTime.Now)
            {
                user.IsActive = true;
                _db.Save();
                Session["User"] = user;
            }

            return View();
        }

        public void AjaxLogout()
        {
            Session["User"] = null;
        }

        public string AjaxRegister(string name, string email)
        {
            AccountRepository _db = new AccountRepository();
            if (!email.Contains('@'))
            {
                return "err";
            }

            var user = _db.GetUserByEmail(email);
            if (user == null)
            {
                user = new User() { Name = name, Email = email, IsActive = false };
                _db.AddUser(user);
            }

            var passUntil = DateTime.Now.AddDays(3);
            var pass = Guid.NewGuid().ToString();
            user.PassExpiration = passUntil;
            user.Password = pass;
            MailHelper.SendPasswd(email, $"http://localhost:2133/Account/ConfirmRegistration?id={user.Id}&key={pass}");
            return "ok";
        }
    }
}