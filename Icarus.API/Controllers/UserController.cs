using Icarus.Model;
using Icarus.Model.User;
using Icarus.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpPost("login")]
        public General<LoginViewModel> Login([FromBody] LoginViewModel user)
        {
            return userService.Login(user);
        }

        [HttpPost]
        public General<UserViewModel> Insert([FromBody] UserViewModel newUser)
        {
            return userService.Insert(newUser);
        }

        [HttpPut("{id}")]
        public General<UserViewModel> Update([FromBody] UserViewModel user)
        {
            return userService.Update(user);
        }

        [HttpDelete("{id}")]
        public General<UserViewModel> Delete(int id)
        {
            return userService.Delete(id);
        }
    }
}
