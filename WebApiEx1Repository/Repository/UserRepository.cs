﻿using Dapper;
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
            //return new User();
        }

        public async Task<List<UserVM>> Get<UserVM>(UserInput input)
        {
            List<UserVM> result = new List<UserVM>();
            var parameter = new DynamicParameters();
            string sSelectCount = "select COUNT(*) total";
            string sSelect = "select * ";
            string sCmd = @" from [User] where 1=1 ";
            if (input.UserId > 0)
            {
                parameter.Add("UserId", input.UserId);
                sCmd += "and UserId=@UserId";
            }
            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                parameter.Add("UserName", input.UserName);
                sCmd += "and UserName=@UserName";
            }
            if (input.Age > 0)
            {
                parameter.Add("UserName", input.Age);
                sCmd += "and Age=@Age";
            }
            if (!string.IsNullOrWhiteSpace(input.CityId))
            {
                parameter.Add("UserName", input.CityId);
                sCmd += "and CityId=@CityId";
            }
            if (input.CountryId > 0)
            {
                parameter.Add("UserName", input.CountryId);
                sCmd += "and CountryId=@CountryId";
            }

           // var vTotal = await _msDBConn.ExecuteScalarAsync<int>(sSelectCount + sCmd, parameter);
            //result.TotalRecords = vTotal;
            //if (vTotal == 0)
            //    return result;

            //string sOrder = $@"
            //            order by UserId
            //            offset {(input.Page - 1) * input.PageSize} rows fetch next {input.PageSize} rows only;
            //            ";

            //sCmd = sSelect + sCmd + sOrder;
            //var UserData = await _msDBConn.QueryAsync<UserVM>(sCmd, parameter);
            //result.Data = UserData.ToList();
            //return result;
            return new List<UserVM>();
        }


        public async Task<IList<User>> GetAll()
        {
            var Userlist = _dbContext.User.ToList();
            return Userlist;
        }
        public  bool Add(User user)
        {
            _dbContext.User.Add(user);
            var result =  _dbContext.SaveChanges();
            return (result > 0)?true:false;
        }

        public bool Update(User user)
        {
            _dbContext.User.Update(user);
            var result = _dbContext.SaveChanges();
            return (result > 0) ? true : false;
        }

        public bool Delete(User user)
        {
            _dbContext.User.Remove(user);
            var result = _dbContext.SaveChanges();
            return (result > 0) ? true : false;
        }
    }
}