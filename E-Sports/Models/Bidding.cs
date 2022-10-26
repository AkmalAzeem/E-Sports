using Microsoft.Build.Framework;

namespace E_Sports.Models
{
    public class Bidding
    {
        public Guid Id { get; set; }
        [Required]
        public string PlayeName { get; set; }
        [Required]
        public Guid PlayerId { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public Guid TeamId { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
