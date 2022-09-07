using Microsoft.EntityFrameworkCore;
using SimplyChatAPI.Models;

namespace SimplyChatAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .HasOne<User>(x=>x.Sender)
                .WithMany(m=>m.Messages)
                .HasForeignKey(x=>x.UserId);
        }
    }
}
