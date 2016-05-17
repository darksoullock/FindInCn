using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models.DB;
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
            if (password == null || password==string.Empty)
            {
                MailHelper.SendPasswd(email, Guid.NewGuid().ToString());
                return new JavaScriptSerializer().Serialize(new { status = "sent" });
            }

            var user = new User() { Email = email, Name = email.Split('@').First() };
            Session["User"] = user;
            var jss = new JavaScriptSerializer();
            var result = jss.Serialize(new { status = "ok", username = user.Name});
            return result;
        }

        public void AjaxLogout()
        {
            Session["User"] = null;
        }
    }
}