using Interfaces;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using AutoMapper;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _userRepo;
        private readonly IMapper _automapper;
        public UserController(IUsers userRepo, IMapper _automapper)
        {
            this._userRepo = userRepo;   
            this._automapper = _automapper;
        }
        [HttpPost("Adduser")]
        public User Adduser([FromBody] UsersDto user)
        {
            var data=this._automapper.Map<User>(user);

            return this._userRepo.Register(data);
        }
        // GET: api/User/login
        [HttpPost("login")]
        public User Login(UsersDto userx)
        {

            return this._userRepo.Loginuser(userx);
        }
        [HttpGet]
        [Route("getallproduct")]
        public CustomResponse<Product> Getallproduct()
        {
            return this._userRepo.GetAllProduct();
        }
        // GET api/<UserController>/5
        [HttpGet]
        [Route("getproductbyid/{id}")]
        public Product Getproductbyid(int id)
        {
            return this._userRepo.GetProductbyid(id);
        }

        // POST api/<UserController>
      
        [HttpPost("addproduct")]
        public Product Addproduct([FromForm] productDto Product)
        {
            //var data = this._automapper.Map<User>(user);
            var dt = this._automapper.Map<Product>(Product);
            return this._userRepo.AddProduct(dt);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("EditProduct/{id}")]
        public Product EditProduct(int id, [FromBody] Product Product)
        {
          return  this._userRepo.EditProduct(Product);
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public Product Delete(int id)
        {
            var p=this.Getproductbyid(id);
           return this._userRepo.DeleteProduct(p);
        }
    }
}
