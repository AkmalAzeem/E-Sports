using Microsoft.Build.Framework;

namespace E_Sports.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
