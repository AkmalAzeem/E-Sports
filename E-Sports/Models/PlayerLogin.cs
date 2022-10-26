using Microsoft.Build.Framework;

namespace E_Sports.Models
{
    public class PlayerLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
