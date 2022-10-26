using System.ComponentModel.DataAnnotations;

namespace E_Sports.Models
{
    public class PlayerView
    {
        public Guid Id { get; set; }
        [Required]
        public string FisrtName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [Range(19, 50, ErrorMessage = "Age Must be between 19 to 50")]
        public int Age { get; set; }
        [Required]
        public Guid Trophy { get; set; }
        [Required]
        public Guid Team { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Baseprice { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
