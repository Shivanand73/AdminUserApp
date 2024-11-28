using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.IServices
{
    public interface IUserServics
    {
        public List<User> GetAll();
        public string InsertData(User user);
        public string Delete(int id);

        public User GetById(int id);

        public List<Category> GetCategoryAll();//for gate all category



    }
}
