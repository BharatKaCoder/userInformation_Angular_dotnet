using System.ComponentModel.DataAnnotations;

namespace userInformation_Angular_dotnet.Models.DTO
{
    public class RegistrationUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
