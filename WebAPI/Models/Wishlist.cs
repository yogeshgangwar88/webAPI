using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Wishlist
    {
        public Wishlist()
        {
            this.Products= new HashSet<Product>();
        }
       // [ForeignKey("User")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
