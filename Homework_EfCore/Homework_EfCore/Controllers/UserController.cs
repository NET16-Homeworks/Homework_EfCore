using Microsoft.AspNetCore.Mvc;
using Homework_EfCore.Services;
using Homework_EfCore.Dtos;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;

        public UserController(UserServices userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/[controller]/{email:string}")]
        public async Task<UserDto> GetUserByEmail(string email)
        {
            return await _userService.Get(new User { Email = email });
        }

        [HttpGet]
        [Route("/[controller]/List")]
        public async Task<List<UserDto>> GetUsersList(string email)
        {
            return await _userService.GetList();
        }

        [HttpPost]
        [Route("/[controller]/Add")]
        public async Task<UserDto> AddUser([FromBody] User user)
        {
            return await _userService.Create(user);
        }

        [HttpDelete]
        [Route("/[controller]/Delete/{string:email}")]
        public async Task<UserDto> DeleteUser(string email)
        {
            return await _userService.Delete(new User { Email = email });
        }

        [HttpPut]
        [Route("/[controller]/Update/{string:email}")]
        public async Task<UserDto> UpdateUser([FromBody] User user)
        {
            return await _userService.Update(user);
        }
    }
}
