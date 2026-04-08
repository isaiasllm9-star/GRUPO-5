using PasswordManagerApp.Database;
using PasswordManagerApp.Models;

namespace PasswordManagerApp.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _db.Categories.Find(id);
        }
    }
}
