using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CreateUserRequestDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(
            @"^[\p{L}'-]+$",
            ErrorMessage = "First name may contain letters, hyphens, and apostrophes only."
        )]
        [MinLength(2, ErrorMessage = "First name must be at least 2 letters.")]
        [MaxLength(20, ErrorMessage = "First name must be at most 20 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(
            @"^[\p{L}'-]+$",
            ErrorMessage = "Last name may contain letters, hyphens, and apostrophes only."
        )]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 letters.")]
        [MaxLength(20, ErrorMessage = "Last name must be at most 20 characters.")]
        public string LastName { get; set; } = string.Empty;
    }
}
