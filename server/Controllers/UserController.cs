using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IRepo Repo) : ControllerBase
    {
        private readonly IRepo _Repo = Repo;

        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> Post([FromBody] UserCreateDto request)
        {
            var validationError = ValidateName(request.FirstName, request.LastName);
            if (validationError is not null)
            {
                return BadRequest(validationError);
            }

            var firstName = NormalizeName(request.FirstName);
            var lastName = NormalizeName(request.LastName);

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName
            };

            await _Repo.AddUser(user);

            var response = new UserCreatedDto
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

        private static string? ValidateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                return "First and last name are required.";
            }

            if (firstName.Length > 20 || lastName.Length > 20)
            {
                return "First and last name must be 20 characters or fewer.";
            }

            if (!IsValidName(firstName) || !IsValidName(lastName))
            {
                return "First and last name may contain letters, hyphens, apostrophes, and spaces only.";
            }

            return null;
        }

        private static string NormalizeName(string name)
        {
            var trimmedName = name.Trim();
            return char.ToUpper(trimmedName[0]) + trimmedName[1..];
        }

        private static bool IsValidName(string name)
        {
            return name.All(character =>
                char.IsLetter(character) ||
                character == '-' ||
                character == '\'' ||
                character == ' ');
        }
    }
}
