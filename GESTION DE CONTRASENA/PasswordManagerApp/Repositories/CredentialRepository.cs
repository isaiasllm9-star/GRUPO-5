using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Database;
using PasswordManagerApp.Models;

namespace PasswordManagerApp.Repositories
{
    public class CredentialRepository
    {
        private readonly AppDbContext _db;

        public CredentialRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Credential> GetAllWithCategories()
        {
            return _db.Credentials.Include(c => c.Category).ToList();
        }

        public Credential? GetById(int id)
        {
            return _db.Credentials.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
        }

        public void Add(Credential credential)
        {
            _db.Credentials.Add(credential);
            _db.SaveChanges();
        }

        public List<Credential> SearchBySiteName(string siteName)
        {
            return _db.Credentials
                .Include(c => c.Category)
                .Where(c => c.SiteName.ToLower().Contains(siteName.ToLower()))
                .ToList();
        }

        public void Update(Credential credential)
        {
            _db.Credentials.Update(credential);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var cred = _db.Credentials.Find(id);
            if (cred != null)
            {
                _db.Credentials.Remove(cred);
                _db.SaveChanges();
            }
        }

        public int GetTotalCount()
        {
            return _db.Credentials.Count();
        }

        public Dictionary<string, int> GetCountByCategory()
        {
            return _db.Credentials
                .Include(c => c.Category)
                .GroupBy(c => c.Category.Name)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
