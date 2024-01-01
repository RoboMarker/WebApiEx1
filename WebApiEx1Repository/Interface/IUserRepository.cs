using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Input;

namespace WebApiEx1Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetById(int UserId);
        Task<IEnumerable<User>> GetAll();


        bool Add(User user);

        bool Update(User user);

        bool Delete(User user);
    }
}
