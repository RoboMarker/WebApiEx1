using Microsoft.EntityFrameworkCore;
using Moq;
using System.Numerics;
using WebApiEx1Repository.Context;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Helper;
using WebApiEx1Repository.Interface;
using WebApiEx1Repository.Repository;
using WebApiEx1RepositoryTest.MockData;

namespace WebApiEx1RepositoryTest.Repository
{
    public class UsersRepositoryTest
    {
        private readonly IUserRepository _userRepository;
        private readonly Mock<AppDbContext> _dbContextMock;
        private readonly Mock<DbSet<User>> _userDbSetMock;
        private readonly DbContextOptions<AppDbContext> _options;
        private AppDbContext _context;
        public AppDbContext CreateDbContext()
        {
            var _options = new DbContextOptionsBuilder<AppDbContext>()
         .UseInMemoryDatabase(databaseName: "TestDatabase")
         .Options;
            return new AppDbContext(_options);
        }

        public void Insert(AppDbContext dbCtx)
        {
            dbCtx.User.Add(MockData_ModelsData_User.GetUser(1.ToString()));
            dbCtx.SaveChanges();
        }

        public UsersRepositoryTest()
        {
            _dbContextMock = new Mock<AppDbContext>();
            _userRepository = new UserRepository(_dbContextMock.Object);
            _userDbSetMock = new Mock<DbSet<User>>();

            _context = new AppDbContext(TestDBConnection.GetConnection());
            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            }
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task GetAll_ReturnUserList_IsSuccess()
        {
            LoadUsersData(_context);

            var result = await _userRepository.GetAll();
            var userlist = MockData_ModelsData_User.GetUserAll();

            Assert.NotEmpty(result);
            Assert.Equal(userlist.Count(), result.Count());

        }


        [Fact]
        public async Task GetAll_ReturnUserList_IsFail()
        {
            var result = await _userRepository.GetAll();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ReturnUser_WhenUserExists()
        {
            LoadUsersData(_context);
            int iUserId = 1;

            var result = await _userRepository.GetById(iUserId);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("sam", result.UserName);
        }

        [Fact]
        public async Task GetById_ReturnNull_WhenUserNotExists()
        {
            int iUserId = 0;
            var result = await _userRepository.GetById(iUserId);

            Assert.Null(result);
        }


        [Fact]
        public async Task Add_ReturnTrue_WhenIsSuccess()
        {
            LoadUsersData(_context);

            var newUser = MockData_ModelsData_User.AddUser();
            var result = _userRepository.Add(newUser);
            var userlist = MockData_ModelsData_User.GetUserAll();

            Assert.True(result);

            var addedUser = await _userRepository.GetById(newUser.UserId);
            Assert.NotNull(addedUser);
            Assert.Equal(newUser.UserId, addedUser.UserId);
        }

        [Fact]
        public async Task Add_ReturnFalse_WhenIsFailed()
        {
            var result = false;

            result = _userRepository.Add(null);

            Assert.False(result);
        }


        [Fact]
        public async Task Update_ReturnTrue_WhenIsSuccess()
        {
            LoadUsersData(_context);
            var user = MockData_ModelsData_User.GetUser(1.ToString());

            var existingUser = await _userRepository.GetById(user.UserId);
            var result = _userRepository.Update(existingUser);

            Assert.True(result);
        }

        [Fact]
        public async Task Update_ReturnFalse_WhenIsFailed()
        {
            LoadUsersData(_context);
            var nonExistingUserId = 0;

            var existingUser = await _userRepository.GetById(nonExistingUserId);
            Assert.Null(existingUser);
            var result = _userRepository.Update(existingUser);

            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ReturnTrue_WhenIsSuccess()
        {
            LoadUsersData(_context);
            var user = MockData_ModelsData_User.GetUser(1.ToString());

            var existingUser = await _userRepository.GetById(user.UserId);
            var result = _userRepository.Delete(existingUser);

            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ReturnFalse_WhenIsFailed()
        {
            LoadUsersData(_context);
            var nonExistingUserId = 0;

            var existingUser = await _userRepository.GetById(nonExistingUserId);
            Assert.Null(existingUser);
            var result = _userRepository.Delete(existingUser);

            Assert.False(result);
        }


        public void LoadUsersData(AppDbContext context)
        {
            var UserList = MockData_ModelsData_User.GetUserAll();
            foreach (var user in UserList)
            {
                context.User.Add(user);
                context.SaveChanges();
            }

        }
    }
}
