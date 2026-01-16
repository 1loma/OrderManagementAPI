using System.ComponentModel.DataAnnotations;

namespace Order_Management_API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public required DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; } // FK to Cusomer_Id
        public Customer? Customer { get; set; } // Navigator to Customer


        public OrderDetail? OrderDetail { get; set; } // Navigator to orderDetails

    }
}
