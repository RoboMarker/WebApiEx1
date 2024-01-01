using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApiEx1Repository.Context;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Input;
using WebApiEx1Repository.Interface;

namespace WebApiEx1Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<User> GetById(int UserId)
        {
               var result=  await  _dbContext.User.Where(x => x.UserId== UserId).FirstOrDefaultAsync();
                return result;
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            return  _dbContext.User.ToList();
  
        }
        public  bool Add(User user)
        {
            if (user == null)
            {
                // 实体为null，删除失败
                return false;
            }
            _dbContext.User.Add(user);
            var result =  _dbContext.SaveChanges();
            return (result > 0)?true:false;
        }

        public bool Update(User user)
        {
            try
            {
                if (user == null)
                {
                    // 实体为null，删除失败
                    return false;
                }
                _dbContext.User.Update(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var result = _dbContext.SaveChanges();
            return (result > 0) ? true : false;
        }

        public bool Delete(User user)
        {
            if (user == null)
            {
                // 实体为null，删除失败
                return false;
            }
            _dbContext.User.Remove(user);
            var result = _dbContext.SaveChanges();
            return (result > 0) ? true : false;
        }
    }
}