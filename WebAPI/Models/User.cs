using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Models
{
    public class User
    {
       
       // [ForeignKey("Wishlist")]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public virtual Wishlist? Wishlist { get; set; }
    }
}
