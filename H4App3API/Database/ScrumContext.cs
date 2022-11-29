using H4App3API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace H4App3API.Database
{
    public class ScrumContext : DbContext
    {
        public ScrumContext() { }

        public ScrumContext(DbContextOptions<ScrumContext> options) : base(options) { }

        public DbSet<Attachment> AttachmentsTable { get; set; }
        public DbSet<Card> CardTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Column> ColumnTable { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}
