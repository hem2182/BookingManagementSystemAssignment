using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementDataLayer.Models
{
    public class BookingDetails
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("MemberInfo")]
        public int MemberId { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public DateTime Booking_Date { get; set; }
        public DateTime? Cancellation_Date { get; set; }

        public MemberInfo Member { get; set; } = null!;
        public Inventory Inventory { get; set; } = null!;
    }
}
