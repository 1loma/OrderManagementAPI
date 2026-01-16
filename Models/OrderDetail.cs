using System.ComponentModel.DataAnnotations;

namespace Order_Management_API.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public int OrderId { get; set; } // FK to Order_Id
        public Order? Order { get; set; } // Navigator to Order

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(150)]
        public required string ShippingAddress { get; set; }

    }
}
