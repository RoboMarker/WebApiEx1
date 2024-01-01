using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1.Controllers;
using WebApiEx1Repository.Data;
using WebApiEx1RepositoryTest.MockData;
using WebApiEx1Service.Interface;

namespace WebApiEx1Test.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;
        public UserControllerTest()
        {
            _userServiceMock= new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkUser_WhenUserExists()
        {
            int UserId = 1;
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());

            _userServiceMock.Setup(ser => ser.GetById(UserId)).ReturnsAsync(user);

            var vResult = await _userController.Get(UserId);

            Assert.IsType<OkObjectResult>(vResult);
            Assert.NotNull(vResult);
            var userResult = (User)((OkObjectResult)vResult).Value;
            Assert.Equal(userResult.UserId, user.UserId);
            _userServiceMock.Verify(c => c.GetById(UserId), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            int UserId = -1;
            _userServiceMock.Setup(ser => ser.GetById(It.IsAny<int>())).ReturnsAsync((int UserId) => (User)null);
            // _userServiceMock.Setup(ser => ser.GetById(UserId)).ReturnsAsync(() => null);

            var vResult = await _userController.Get(UserId);

            Assert.IsType<NotFoundResult>(vResult);
            _userServiceMock.Verify(c => c.GetById(UserId), Times.Once);
        }

        [Fact]
        public async Task AddUser_ReturnTrue_WhenIsSuccess()
        {
            var user = MockData_ModelsData_User.AddUser();
            _userServiceMock.Setup(ser => ser.AddAsync(It.IsAny<User>())).ReturnsAsync(true);

            var vResult = await _userController.AddUser(user);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.AddAsync(user), Times.Once);
        }


        [Fact]
        public async Task UpdateUser_ReturnTrue_WhenIsSuccess()
        {
            var user = MockData_ModelsData_User.UpdateUserResult();
            _userServiceMock.Setup(ser => ser.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

            var vResult = await _userController.UpdateUser(user);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.UpdateAsync(user), Times.Once);

        }

        [Fact]
        public async Task DeleteUser_ReturnTrue_WhenIsSuccess()
        {
            int UserId = 1;
            _userServiceMock.Setup(ser => ser.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            var vResult = await _userController.DeleteUser(UserId);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.DeleteAsync(UserId), Times.Once);

        }

    }
}
