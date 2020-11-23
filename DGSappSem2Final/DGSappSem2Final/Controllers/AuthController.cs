using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DGSappSem2Final.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public ActionResult Login()
        {
            string user_name = Request.Form["username"];

            if (user_name.Trim() == "")
            {
                return Redirect("/");
            }

            using (var db = new Models.Chat.ChatContext())
            {

                ChatUser user = db.Users.FirstOrDefault(u => u.Name == user_name);

                if (user == null)
                {
                    user = new ChatUser { Name = user_name };

                    db.Users.Add(user);
                    db.SaveChanges();
                }

                Session["user"] = user;
            }

            return Redirect("/chat");
        }
    }
}