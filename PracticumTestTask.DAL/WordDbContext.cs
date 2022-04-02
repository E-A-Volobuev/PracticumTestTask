using Microsoft.EntityFrameworkCore;
using PracticumTestTask.Model.Model;

namespace PracticumTestTask.DAL
{
    public class WordDbContext : DbContext
    {
        public WordDbContext(DbContextOptions<WordDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<RequestUrl> RequestUrls { get; set; }
    }
}
