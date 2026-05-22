using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.DTOs;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IRepo Repo) : ControllerBase
    {
        private readonly IRepo _Repo = Repo;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequestDto request)
        {
            var firstName = NormalizeName(request.FirstName);
            var lastName = NormalizeName(request.LastName);

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName
            };

            await _Repo.AddUser(user);

            var response = new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return Ok(new
            {
                message = "User info saved successfully.",
                data = response
            });
        }

        private static string NormalizeName(string name)
        {
            return char.ToUpper(name[0]) + name[1..];
        }
    }
}
