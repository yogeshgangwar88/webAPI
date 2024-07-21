using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DTOs
{
    public class productDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; } = 0;
       // public string ImageName { get; set; }
        [NotMapped]
        public  IFormFile? File { get; set; }
    }
}
