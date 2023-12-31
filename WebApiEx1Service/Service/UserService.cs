using WebApiEx1Repository.Context;
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

        public async Task<User?> GetById(int UserId)
        {
            if (UserId <= 0) return null;
            var User = await _userRepository.GetById<User>(UserId.ToString());
            return User;
        }

        public async Task<List<UserVM>> Get(UserInput input)
        {
            var User = await _userRepository.Get<UserVM>(input);
            return User;
        }


        public async Task<IEnumerable<User>> GetUserAll()
        {
            // var userslist =  _GenrRepo.GetAll<User>();
            var userslist = await _userRepository.GetAll<User>();
            return userslist;
        }

        public async Task<bool> AddAsync(User user)
        {
            if (user == null) return false;

            var newUser = await _userRepository.GetById<User>(user.UserId.ToString());
            if (newUser != null) return false;

            bool bResult = await _userRepository.AddAsync(user);
           // _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> UpdateAsync(User updateUser)
        {
            if (updateUser == null) return false;

            var updateHaveUser = await _userRepository.GetById<User>(updateUser.UserId.ToString());
            if (updateHaveUser == null) return false;

            var newUser = new User();
            newUser.UserName = updateUser.UserName;
            newUser.UserId = updateUser.UserId;
            newUser.Sex = updateUser.Sex;
            newUser.Age = updateUser.Age;
            newUser.CityId = updateUser.CityId;
            newUser.CountryId = updateUser.CountryId;


            bool bResult = await _userRepository.UpdateAsync(newUser);
           // _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> DeleteAsync(int UserId)
        {
            if (UserId <= 0) return false;

            var newUser = await _userRepository.GetById<User>(UserId.ToString());
            if (newUser == null) return false;

            bool bResult = await _userRepository.DeleteAsync(newUser);
            //_msDBConn.Commit();
            return bResult;
        }
    }
}
