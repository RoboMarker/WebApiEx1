using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiEx1Repository.Context;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Interface;
using WebApiEx1Repository.Repository;
using WebApiEx1Repository.ViewModels;
using WebApiEx1RepositoryTest.MockData;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;

namespace WebApiEx1RepositoryTest.Repository
{
    public class UsersRepositoryTest : IDisposable
    {
        private readonly IUserRepository _userRepository;
        private readonly Mock<AppDbContext> _dbContextMock;
        private readonly Mock<DbSet<User>> _userDbSetMock;
        private readonly DbContextOptions<AppDbContext> _options;
        public UsersRepositoryTest()
        {
            _dbContextMock = new Mock<AppDbContext>();
            _userRepository =new UserRepository(_dbContextMock.Object);
            _userDbSetMock = new Mock<DbSet<User>>();
            // 使用内存中数据库配置选项
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(_options))
            {
                context.User.Add(MockData_ModelsData_User.GetUser(1.ToString()));
                context.User.Add(MockData_ModelsData_User.GetUser(2.ToString()));
                context.SaveChanges();
            }

        }


        [Fact]
        public async Task GetAll_ReturnUserList_WhenUserExists()
        {
            //int iUserId = 1;
            //var user = MockData_ModelsData_User.GetUser(iUserId.ToString());

            //_dbContextMock.Setup(x => x.User).Returns(_userDbSetMock.Object);
            //_userDbSetMock.Setup(x => x.FindAsync(iUserId)).ReturnsAsync(user);


            //// Act
            //var result = await _userRepository.GetById(iUserId);

            //Assert.NotNull(result);
            //Assert.Equal(iUserId, result.UserId);
            //Assert.Equal("sam", result.UserName);

            // Arrange
            using (var context = new AppDbContext(_options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.UserId);
                // 根据你的业务逻辑和预期结果添加更多断言
            }
        }

        [Fact]
        public async Task GetById_ReturnUser_WhenUserExists()
        {
            int iUserId = 1;
            //Arrange
            var user = MockData_ModelsData_User.GetUser(iUserId.ToString());


            var dbSetMock = DbContextMockHelper.CreateDbSetMock(user);
            _dbContextMock.Setup(x => x.User).Returns(dbSetMock.Object);

            var userRepository = new UserRepository(_dbContextMock.Object);
            //Act
            var actualUser = await _userRepository.GetById(iUserId);
            //Assert
            Assert.NotNull(actualUser);
        }

        [Fact]
        public async Task GetById_ReturnNull_WhenUserNotExists()
        {
            int iUserId = 3;
            //Arrange
            var user = MockData_ModelsData_User.GetUser(iUserId.ToString());

            _dbContextMock.Setup(x => x.User).Returns(_userDbSetMock.Object);
            _userDbSetMock.Setup(x => x.FindAsync(iUserId)).ReturnsAsync(user);
            //Act
            var actualUser = await _userRepository.GetById(iUserId);
            //Assert
            Assert.Null(actualUser);
        }

        [Fact]
        public async Task GetAll_ReturnNull_IsFail()
        {
            //int iUserId = 1;
            //var userlist = MockData_ModelsData_User.GetUserAll();

            //_dbContextMock.Setup(x => x.User).Returns(_userDbSetMock.Object);
            //_userDbSetMock.Setup(x => x.User).ReturnsAsync(_userDbSetMock.Object);


            //// Act
            //var result = await _userRepository.GetAll();

            //// Assert
            //Assert.NotNull(result);
            //Assert.Equal(userlist.Count(), result.Count);
        }

        [Fact]
        public async Task Add_ReturnTrue_WhenIsSuccess()
        {
            int iIsSunccess = 1;
 
            User user = new User();

            _dbContextMock.Setup(x => x.User.Add(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult =  _userRepository.Add(user);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task Add_ReturnFalse_WhenIsFailed()
        {
            int iIsSunccess = 0;

            User user = new User();

            _dbContextMock.Setup(x => x.User.Add(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult =  _userRepository.Add(user);

            Assert.False(actualResult);
        }


        [Fact]
        public async Task Update_ReturnTrue_WhenIsSuccess()
        {
            int iIsSunccess = 1;

            User user = new User();

            _dbContextMock.Setup(x => x.User.Update(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult = _userRepository.Add(user);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task Update_ReturnFalse_WhenIsFailed()
        {
            int iIsSunccess = 0;

            User user = new User();

            _dbContextMock.Setup(x => x.User.Update(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult = _userRepository.Add(user);

            Assert.False(actualResult);
        }

        [Fact]
        public async Task Delete_ReturnTrue_WhenIsSuccess()
        {
            int iIsSunccess = 1;

            User user = new User();

            _dbContextMock.Setup(x => x.User.Remove(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult = _userRepository.Add(user);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task Delete_ReturnFalse_WhenIsFailed()
        {
            int iIsSunccess = 0;

            User user = new User();

            _dbContextMock.Setup(x => x.User.Remove(user)).Verifiable();
            _dbContextMock.Setup(x => x.SaveChanges()).Returns(iIsSunccess);


            var actualResult = _userRepository.Add(user);

            Assert.False(actualResult);
        }

        public void Dispose()
        {
            // 在测试完成后清理资源（如果有必要）
        }
    }
}
