using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAppWithEF.Models;

namespace ChatAppWithEF.Data
{
    public class ChatAppDbContext : DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
             
    }
}
