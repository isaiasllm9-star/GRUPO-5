using System.ComponentModel.DataAnnotations;

namespace PasswordManagerApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Credential> Credentials { get; set; } = new List<Credential>();
    }
}
