using MessagePack;
using Microsoft.Build.Framework;

namespace E_Sports.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid Trophy { get; set; }
        [Required]
        public double SpendLimit { get; set; }
        [Required]
        public double Spent { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
