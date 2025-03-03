using BookingManagementDataLayer.Models;
using System.Collections.Generic;

namespace BookingManagementDataLayer
{
    public interface IBookingSystemDb
    {
        // Member Info
        IEnumerable<MemberInfo> GetAllMembers();
        MemberInfo? GetMemberById(int id);

        // Inventory
        IEnumerable<Inventory> GetAllInventory();
        Inventory? GetInventoryById(int id);

        // Booking Details
        IEnumerable<BookingDetails> GetBookingDetailsData();
        IEnumerable<BookingDetails> GetBookingDetailsByBookingId(int bookingId);
        IEnumerable<BookingDetails> GetBookingDetailsByMemberId(int memberId);
        IEnumerable<BookingDetails> GetBookingDetailsByInventoryId(int inventoryId);

        // Insert data from importing file.
        int InsertMemberData(string filePath);
        int InsertInventoryData(string filePath);
        string BookInventoryForMember(int memberId, int inventoryId);
        string CancelBooking(int bookingId);
    }
}
