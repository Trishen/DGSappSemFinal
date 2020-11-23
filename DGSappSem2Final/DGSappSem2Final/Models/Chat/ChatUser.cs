using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Chat
{
    public class ChatUser
    {
        public ChatUser()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime created_at { get; set; }
    }
}