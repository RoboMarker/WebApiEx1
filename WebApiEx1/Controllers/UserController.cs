using Microsoft.AspNetCore.Mvc;
using WebApiEx1Repository.Data;
using WebApiEx1Repository.Input;
using WebApiEx1Service.Interface;

namespace WebApiEx1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _iUserService;

        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var UserList = await _iUserService.GetAll();
            if (UserList == null)
            {
                return NotFound();
            }
            return Ok(UserList);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int UserId)
        {
            var vUser = await _iUserService.GetById(UserId);
            if (vUser == null)
            {
                return NotFound();
            }
            return Ok(vUser);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("UserName,Age,Sex,Phone,CityName")] User user)
        {
            var IsCreated = await _iUserService.AddAsync(user);
            if (IsCreated)
            {
                return Ok(IsCreated);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var IsUpdate = await _iUserService.UpdateAsync(user);
            if (IsUpdate)
            {
                return Ok(IsUpdate);
            }
            return BadRequest();

        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            var IsDelete = await _iUserService.DeleteAsync(UserId);
            if (IsDelete)
            {
                return Ok(IsDelete);
            }
            return BadRequest();

        }
    }
}
