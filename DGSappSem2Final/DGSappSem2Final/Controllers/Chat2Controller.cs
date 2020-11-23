using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DGSappSem2Final.Controllers
{
    public class Chat2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //if (Session["user"] == null)
            //{
            //    return Redirect("/");
            //}

            var user1 = new Models.Chat.ChatUser
            {
                Id = 1,
                Name = "TestUser",
                created_at = DateTime.Now.Date
            };

            var user2 = new Models.Chat.ChatUser
            {
                Id = 1,
                Name = "TestUser",
                created_at = DateTime.Now.Date
            };

            var user3 = new Models.Chat.ChatUser {
                Id = 1,
                Name = "TestUser",
                created_at = DateTime.Now.Date
            };

            var currentUser = (Models.Chat.ChatUser)Session["user"];

            using (var db = new Models.Chat.ChatContext())
            {

                //ViewBag.allUsers = db.Users.Where(u => u.Name != currentUser.Name)
                //                 .ToList();                
                ViewBag.allUsers = new List<string> { "TestUser1", "TestUser2" };
            }


            ViewBag.currentUser = session;


            return View();
        }

        public JsonResult ConversationWithContact(int contact)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (Models.Chat.ChatUser)Session["user"];

            var conversations = new List<Models.Chat.Conversation>();

            using (var db = new Models.Chat.ChatContext())
            {
                conversations = db.Conversations.
                                  Where(c => (c.receiver_id == currentUser.Id
                                      && c.sender_id == contact) ||
                                      (c.receiver_id == contact
                                      && c.sender_id == currentUser.Id))
                                  .OrderBy(c => c.created_at)
                                  .ToList();
            }

            return Json(
                new { status = "success", data = conversations },
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult SendMessage()
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (Models.Chat.ChatUser)Session["user"];

            string socket_id = Request.Form["socket_id"];

            Conversation convo = new Conversation
            {
                sender_id = currentUser.Id,
                message = Request.Form["message"],
                receiver_id = Convert.ToInt32(Request.Form["contact"])
            };

            using (var db = new Models.Chat.ChatContext())
            {
                db.Conversations.Add(convo);
                db.SaveChanges();
            }

            return Json(convo);
        }
    }
}