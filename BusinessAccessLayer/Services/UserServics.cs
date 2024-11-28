using BusinessAccessLayer.IServices;
using CommonLayer.Models;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessAccessLayer.Services
{
    public class UserServics : IUserServics
    {
        private readonly AppDBContex contex;

        public UserServics(AppDBContex contex)
        {
            this.contex = contex;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                users = contex.users.Join(
                    contex.categories,
                    us => us.CatId,
                   cat => cat.Id,
                   (us, cat) => new User
                   {
                       Id = us.Id,
                       Name = us.Name,
                       Phone = us.Phone,
                       Email = us.Email,
                       Password = us.Password,
                       IsAdmin = us.IsAdmin,
                       CatId = us.CatId,
                       Categorys = cat.Categorys,
                   }
                    ).ToList();

            }
            catch (Exception ex)
            {

            }

            return users;
        }

        public string InsertData(User user)
        {
            string result= string.Empty;
            try
            {
                if (user.Id==0)
                {
                    contex.Add(user);
                    contex.SaveChanges();
                    result ="successfully saved ";
                }
                if(user.Id != 0)
                {
                    contex.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    result = "faild1";
                }      
              
            } catch (Exception ex) {

                throw;
            }
            return result;
        }

        public List<Category> GetCategoryAll() 
        {
            List<Category> users = new List<Category>();
            users = contex.categories.ToList();
            users.Insert(0, new Category { Id = 0, Categorys = "Select Category" });
            return users;
        }

        public string Delete(int id)
        {
            string result = string.Empty;
            try
            {
                var emp = contex.users.Where(x => x.Id == id).FirstOrDefault();
                contex.Remove(emp);
                contex.SaveChanges();
                result = "deleted successfully";
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public User GetById(int id)
        {
            User user = new User();
            try
            {
                 user = contex.users.Where(x => x.Id == id).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
            }
            return user;
        }
    }
}
