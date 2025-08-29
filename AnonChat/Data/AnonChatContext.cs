using Microsoft.EntityFrameworkCore;
using AnonChat.Models;

namespace AnonChat.Data
{
    public class AnonChatContext : DbContext
    {


        public AnonChatContext(DbContextOptions<AnonChatContext> options) : base(options)
        {
        }
        public DbSet<MessageModel>? Message { get; set; }

        public DbSet<MatchModel>? Matche { get; set; }

        public DbSet<UserModel>? User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnonChatContext).Assembly);
        }
    }
}