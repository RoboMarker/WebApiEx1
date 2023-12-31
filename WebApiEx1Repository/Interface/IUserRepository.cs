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
        Task<T?> GetById<T>(string UserId);
        Task<IEnumerable<User>> GetAll<User>();
        Task<List<UserVM>> Get<UserVM>(UserInput input);
        Task<bool> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        bool Update(User user);

        Task<bool> DeleteAsync(User user);
    }
}
