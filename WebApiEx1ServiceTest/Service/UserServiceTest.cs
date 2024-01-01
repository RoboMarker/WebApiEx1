

using Moq;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Interface;
using WebApiEx1RepositoryTest.MockData;
using WebApiEx1Service.Interface;
using WebApiEx1Service.Service;

namespace WebApiEx1ServiceTest.Service
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService( _userRepositoryMock.Object);
        }

        //Task<User> GetById(int UserId);
        [Theory]
        [InlineData(1)]
        public async void GetById_ReturnUser_IsSuccess(int UserId)
        {
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());
            _userRepositoryMock.Setup(m => m.GetById(UserId)).ReturnsAsync(user);

            var actualUser = await _userService.GetById(UserId);

            Assert.NotNull(actualUser);
            Assert.Equal(user.UserId, actualUser.UserId);
            Assert.Equal(user.UserName, actualUser.UserName);
        }

        [Fact]
        public async void GetUserAll_ReturnUser_IsSuccess()
        {
            var userlist = MockData_ModelsData_User.GetUserAll();

            _userRepositoryMock.Setup(m => m.GetAll()).ReturnsAsync(userlist);

            var actualUser = await _userService.GetAll();

            Assert.NotNull(actualUser);
            Assert.Equal(actualUser.Count(), userlist.Count());
        }

        [Fact]
        public async void AddAsync_ReturnUser_IsSuccess()
        {
            User userGet = null;
            User user = MockData_ModelsData_User.AddUser();

            _userRepositoryMock.Setup(m => m.GetById(user.UserId)).ReturnsAsync(userGet);
            _userRepositoryMock.Setup(m => m.Add(user)).Returns(true);

            var actualResult = await _userService.AddAsync(user);

            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_ReturnUser_IsSuccess()
        {
            var userResult = MockData_ModelsData_User.UpdateUserResult();

            _userRepositoryMock.Setup(m => m.GetById(userResult.UserId)).ReturnsAsync(userResult);
            _userRepositoryMock.Setup(m => m.Update(It.IsAny<User>())).Returns(true);

            var actualResult = await _userService.UpdateAsync(userResult);
            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);


        }

        [Fact]
        public async void DeleteAsync_ReturnUser_IsSuccess()
        {
            string sDelete_UserId = "2";
            var user = MockData_ModelsData_User.GetUser(sDelete_UserId);
            _userRepositoryMock.Setup(m => m.GetById(user.UserId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(m => m.Delete(user)).Returns(true);

            var actualResult = await _userService.DeleteAsync(int.Parse(sDelete_UserId));
            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Once);

        }

    }
}
