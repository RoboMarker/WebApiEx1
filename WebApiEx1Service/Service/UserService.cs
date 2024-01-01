﻿using WebApiEx1Repository.Context;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Input;
using WebApiEx1Repository.Interface;
using WebApiEx1Repository.ViewModels;
using WebApiEx1Service.Interface;

namespace WebApiEx1Service.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

       // private readonly AppDbContext _context;
        public UserService( IUserRepository userRepository)
        {
            _userRepository = userRepository;


        }

        public async Task<User> GetById(int UserId)
        {
            if (UserId <= 0) return null;
            var User = await _userRepository.GetById(UserId);
            return User;
        }

        public async Task<List<UserVM>> Get(UserInput input)
        {
            var User = await _userRepository.Get<UserVM>(input);
            return User;
        }

        public async Task<IList<User>> GetAll()
        {
            var User = await _userRepository.GetAll();
            return User;
        }

        public async Task<bool> AddAsync(User user)
        {
            if (user == null) return false;

            var newUser = await _userRepository.GetById(user.UserId);
            if (newUser != null) return false;

            bool bResult =  _userRepository.Add(user);
            return bResult;
        }

        public async Task<bool> UpdateAsync(User updateUser)
        {
            if (updateUser == null) return false;

            var updateHaveUser = await _userRepository.GetById(updateUser.UserId);
            if (updateHaveUser == null) return false;

            var newUser = new User();
            newUser.UserName = updateUser.UserName;
            newUser.UserId = updateUser.UserId;
            newUser.Sex = updateUser.Sex;
            newUser.Age = updateUser.Age;
            newUser.Phone = updateUser.Phone;
            newUser.CityName = updateUser.CityName;


            bool bResult = _userRepository.Update(newUser);
           // _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> DeleteAsync(int UserId)
        {
            if (UserId <= 0) return false;

            var newUser = await _userRepository.GetById(UserId);
            if (newUser == null) return false;

            bool bResult =  _userRepository.Delete(newUser);
            //_msDBConn.Commit();
            return bResult;
        }
    }
}
