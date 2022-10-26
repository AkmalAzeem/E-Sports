using Microsoft.Build.Framework;

namespace E_Sports.Models
{
    public class AdminLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
