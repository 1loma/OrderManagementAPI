using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Order_Management_API.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; } // PK

        [MaxLength(50)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }


        [MaxLength(13)]
        public string? Phone { get; set; }


        public ICollection<Order> Orders { get; set; } = new List<Order>(); // one-to-many relationship with orders
    }
}
