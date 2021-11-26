using System.ComponentModel.DataAnnotations;

namespace w4sd.Models
{
    public record UserDto(string UserName, string Password);
    public record UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
