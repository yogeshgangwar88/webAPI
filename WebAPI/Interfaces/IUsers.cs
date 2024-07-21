using Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;

namespace Interfaces
{
    public interface IUsers
    {
        public User Loginuser(UsersDto userx);
        public void Logout();
        public User Register(User user);
        public Product AddProduct(Product product);
        public CustomResponse<Product> GetAllProduct();
        public Product GetProductbyid(int id);

        public Product EditProduct(Product product);
        public Product DeleteProduct(Product product);
        
    }
}
