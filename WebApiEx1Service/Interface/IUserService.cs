using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Input;
using WebApiEx1Repository.ViewModels;

namespace WebApiEx1Service.Interface
{
    public interface IUserService
    {
        Task<User?> GetById(int UserId);

        Task<IEnumerable<User>> GetUserAll();

        Task<List<UserVM>> Get(UserInput input);

        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int UserId);
    }
}
