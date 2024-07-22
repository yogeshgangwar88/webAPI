using  Dbcontext;
using  Interfaces;
using  Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using System.IO;
using WebAPI.Models;
using AutoMapper;

namespace Repository
{
    public class UserRepo : IUsers
    {
        private readonly AppDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment; //use for upload image
        private readonly IMapper _mapper;
        public UserRepo(AppDbContext dbcontext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            this._dbcontext = dbcontext;
            this._webHostEnvironment = webHostEnvironment;
            this._mapper = mapper;
        }

        public Product AddProduct(productDto product)
        {
            try
            {
                var prdata=_mapper.Map<Product>(product);
                this._dbcontext.Products.Add(prdata);
                this._dbcontext.SaveChanges();
               
                if (prdata.File?.Length>0)
                {
                   // var validimg = false;
                    var rootpath=this._webHostEnvironment.ContentRootPath;
                    var folderpath = Path.Combine(rootpath, "UserImages");
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                    }
                    //check extentension
                    var ext=Path.GetExtension(prdata.File.FileName);
                    var valid_ext = new string[] { ".jpg", ".jpeg", ".png" };
                    if (valid_ext.Contains(ext))
                    {
                        string filename = product.Id.ToString() + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ext;
                        prdata.ImageName = filename;
                        var filewithpath = Path.Combine(folderpath, filename);
                        var stream = new FileStream(filewithpath, FileMode.Create);
                        prdata.File.CopyTo(stream);
                        stream.Close();
                    }
                   
                    this._dbcontext.Entry(prdata).State = EntityState.Modified;
                    this._dbcontext.SaveChanges();
                }


                
                return prdata;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product DeleteProduct(Product product)
        {
            try
            {
                var p = this._dbcontext.Products.Find(product.Id);
                if (p != null)
                {
                    this._dbcontext.Products.Remove(p);
                    this._dbcontext.SaveChanges();
                }
                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product EditProduct(productDto product)
        {
            try
            {
                var prdata = _mapper.Map<Product>(product);
                var p = this._dbcontext.Products.Find(prdata.Id);
                if (p == null)
                {
                    p = new Product();
                }
                else
                {
                    if (prdata.File?.Length > 0)
                    {
                        //delete existing file
                        var rootpath = this._webHostEnvironment.ContentRootPath;
                        var folderpath = Path.Combine(rootpath, "UserImages");
                        if (!string.IsNullOrEmpty(p.ImageName))
                        {
                            string existingfile = Path.Combine(folderpath, p.ImageName);
                            System.IO.File.Delete(existingfile);
                        }
                       
                        //insert new file
                        var ext = Path.GetExtension(prdata.File.FileName);
                        var valid_ext = new string[] { ".jpg", ".jpeg", ".png" };
                        if (valid_ext.Contains(ext))
                        {
                            string filename = product.Id.ToString() + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ext;
                            prdata.ImageName = filename;
                            var filewithpath = Path.Combine(folderpath, filename);
                            var stream = new FileStream(filewithpath, FileMode.Create);
                            prdata.File.CopyTo(stream);
                            stream.Close();
                        }
                        p.ImageName = prdata.ImageName;
                    }

                    p.ProductName= prdata.ProductName;
                    p.Description= prdata.Description;
                    p.Price= prdata.Price;
                   
                    this._dbcontext.Entry(p).State = EntityState.Modified;
                    this._dbcontext.SaveChanges();
                }
                return p;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustomResponse<Product> GetAllProduct()
        {
            CustomResponse<Product> result = new CustomResponse<Product>();
            try
            {

                //lazy loading [children will not load initially ]
                var p = this._dbcontext.Products.ToList().OrderByDescending(x => x.Id);
                //for (int i = 0; i < p.Count; i++)
                //{
                //    p[i].Users = this._dbcontext.Users.Where(z => z.Id == p[i].Id).ToList();
                //}

                //Eager loading 
                var q=  this._dbcontext.Products.Include(u=>u.Wishlists).ToList();

                result.statusCode = 200;
                result.dataList = p.ToList();

                
                
            }
            catch (Exception e)
            {
                result.statusCode = 400;
                result.statusMessage=e.StackTrace;
               // throw;
            }
            return result;
        }

        public Product GetProductbyid(int id)
        {
            try
            {
                var p= this._dbcontext.Products.Find(id);
                if (p==null)
                {
                    p = new Product();
                }
                return p;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User Loginuser(UsersDto userx)
        {
         var    user =   this._dbcontext.Users.FirstOrDefault(x => x.Email == userx.Email && x.Password == userx.Password);
            if (user == null) {
                return new User();
            }
            return user;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public User Register(User user)
        {
            try
            {
                this._dbcontext.Users.Add(user);
                this._dbcontext.SaveChanges();
                return user;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
