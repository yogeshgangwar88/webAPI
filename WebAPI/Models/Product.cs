using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        public Product()
        {
            this.Wishlists = new HashSet<Wishlist>();
        }
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public float Price { get; set; } = 0;
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }

    }
}
