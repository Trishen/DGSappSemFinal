using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Chat
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("DefaultConnection")
        {
        }

        public static ChatContext Create()
        {
            return new ChatContext();
        }

        public DbSet<ChatUser> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}