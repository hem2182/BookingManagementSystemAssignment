using BookingManagementDataLayer.Models;
using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookingManagementDataLayer
{
    public class BookingSystemDb : IBookingSystemDb
    {
        private int MAX_BOOKINGS = 2;
        private readonly BookingSystemDbContext _dbContext;
        private const int ChunkSize = 1000; // Number of records per batch

        public BookingSystemDb(BookingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int InsertMemberData(string filePath)
        {
            int recordsCount = 0;
            if (!File.Exists(filePath))
            {
                //_logger.LogError("File {FilePath} not found.", filePath);
                return 0;
            }

            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var batch = new List<MemberInfo>();
                foreach (var record in csv.GetRecords<MemberInfo>())
                {
                    batch.Add(record);
                    recordsCount++;

                    if (batch.Count >= ChunkSize)
                    {
                        _dbContext.Members.AddRange(batch);
                        _dbContext.SaveChanges();
                        batch.Clear();
                    }
                }

                if (batch.Count > 0)
                {
                    _dbContext.Members.AddRange(batch);
                    _dbContext.SaveChanges();
                }

            }
            catch (HeaderValidationException ex)
            {
                throw new Exception($"CSV Format Error: {ex.Message}");
            }
            catch (TypeConverterException ex)
            {
                throw new Exception($"Data Conversion Error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected Error: {ex.Message}");
            }

            return recordsCount;
        }

        public int InsertInventoryData(string filePath)
        {
            int recordsCount = 0;
            if (!File.Exists(filePath))
            {
                return 0;
            }

            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var batch = new List<Inventory>();
                foreach (var record in csv.GetRecords<Inventory>())
                {
                    batch.Add(record);
                    recordsCount++;

                    if (batch.Count >= ChunkSize)
                    {
                        _dbContext.Inventories.AddRange(batch);
                        _dbContext.SaveChanges();
                        batch.Clear();
                    }
                }

                if (batch.Count > 0)
                {
                    _dbContext.Inventories.AddRange(batch);
                    _dbContext.SaveChanges();
                }

            }
            catch (HeaderValidationException ex)
            {
                throw new Exception($"CSV Format Error: {ex.Message}");
            }
            catch (TypeConverterException ex)
            {
                throw new Exception($"Data Conversion Error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected Error: {ex.Message}");
            }

            return recordsCount;
        }

        public IEnumerable<Inventory> GetAllInventory()
        {
            return _dbContext.Inventories;
        }

        public IEnumerable<MemberInfo> GetAllMembers()
        {
            return _dbContext.Members;
        }

        public IEnumerable<BookingDetails> GetBookingDetailsData()
        {
            return _dbContext.BookingDetails;
        }
        public IEnumerable<BookingDetails> GetBookingDetailsByBookingId(int bookingId)
        {
            return _dbContext.BookingDetails.Where(x => x.BookingId == bookingId);
        }

        public IEnumerable<BookingDetails> GetBookingDetailsByInventoryId(int inventoryId)
        {
            return _dbContext.BookingDetails.Where(x => x.InventoryId == inventoryId);
        }

        public IEnumerable<BookingDetails> GetBookingDetailsByMemberId(int memberId)
        {
            return _dbContext.BookingDetails.Where(x => x.MemberId == memberId);
        }

        public Inventory? GetInventoryById(int id)
        {
            return _dbContext.Inventories.Where(x => x.Id == id).FirstOrDefault();
        }

        public MemberInfo? GetMemberById(int id)
        {
            return _dbContext.Members.Where(x => x.Id == id).FirstOrDefault();
        }

        public string BookInventoryForMember(int memberId, int inventoryId)
        {
            // Member domain logic
            var memberInfoData = GetMemberById(memberId);
            if (memberInfoData == null)
                throw new Exception($"Invalid MemberId: {memberId}");

            if (memberInfoData.BookingCount == MAX_BOOKINGS)
                throw new Exception("Booking Limit reached. Cannot book further items.");

            // Inventory domain logic
            var inventoryData = GetInventoryById(inventoryId);
            if (inventoryData == null)
                throw new Exception($"Inventory Data not found for Id: {inventoryId}");

            if (inventoryData.RemainingCount == 0)
                throw new Exception($"Inventory with Id: {inventoryId} is currently out of stock.");

            if (inventoryData.Expiration_Date < DateOnly.FromDateTime(DateTime.Now.Date))
                throw new Exception($"Inventory Id: {inventoryId} expired.");

            // Insert the booking data
            _dbContext.BookingDetails.Add(new BookingDetails
            {
                Booking_Date = DateTime.UtcNow,
                MemberId = memberId,
                InventoryId = inventoryId,
                Cancellation_Date = null
            });
            _dbContext.SaveChanges();

            // Update the booking count for member
            memberInfoData.BookingCount = memberInfoData.BookingCount + 1;
            _dbContext.Members.Update(memberInfoData);
            _dbContext.SaveChanges();

            // Update the inventory remaining count
            inventoryData.RemainingCount = inventoryData.RemainingCount - 1;
            _dbContext.Inventories.Update(inventoryData);
            _dbContext.SaveChanges();

            return "Booking Successful.";
        }

        public string CancelBooking(int bookingId)
        {
            var bookingData = _dbContext.BookingDetails.Where(x => x.BookingId == bookingId).FirstOrDefault();
            if (bookingData == null)
                return $"Booking Details not found for BookingId: {bookingId}";

            if (bookingData.Cancellation_Date != null)
                return $"BookingId: {bookingId} is already cancelled";

            bookingData.Cancellation_Date = DateTime.UtcNow;
            _dbContext.BookingDetails.Update(bookingData);
            _dbContext.SaveChanges();

            var inventoryData = _dbContext.Inventories.Find(bookingData.InventoryId);
            if (inventoryData != null)
            {
                inventoryData.RemainingCount = inventoryData.RemainingCount + 1;
                _dbContext.Inventories.Update(inventoryData);
                _dbContext.SaveChanges();
            }

            var memberData = _dbContext.Members.Find(bookingData.MemberId);
            if (memberData != null)
            {
                memberData.BookingCount = memberData.BookingCount - 1;
                _dbContext.Members.Update(memberData);
                _dbContext.SaveChanges();
            }

            return "Booking Cancelled Successfully";
        }
    }
}
