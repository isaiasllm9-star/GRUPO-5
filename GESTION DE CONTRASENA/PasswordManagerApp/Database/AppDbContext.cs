using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Models;

namespace PasswordManagerApp.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=passwords.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Trabajo" },
                new Category { Id = 2, Name = "Redes Sociales" },
                new Category { Id = 3, Name = "Bancos" },
                new Category { Id = 4, Name = "Streaming" },
                new Category { Id = 5, Name = "Otros" }
            );

            // One-to-many relationship
            modelBuilder.Entity<Credential>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Credentials)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
