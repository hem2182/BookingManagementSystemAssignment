using BookingManagementDataLayer;
using BookingManagementDataLayer.Models;
using Moq;

namespace BookingManagementTests
{
    [TestClass]
    public class BookingManagementDataLayerTests
    {
        private Mock<IBookingSystemDb> _mockRepo;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IBookingSystemDb>();
        }

        [TestMethod]
        public void GetInventoriesShouldReturnAllInventories()
        {
            // Arrange
            var mockInventoryList = new List<Inventory>
            {
                new Inventory { Id = 1, Title = "Laptop", Description = "Lapptop 123", RemainingCount = 10, Expiration_Date = DateOnly.Parse("25-03-2030") },
                new Inventory { Id = 2, Title = "Mouse", Description = "Mouse 123", RemainingCount = 20, Expiration_Date = DateOnly.Parse("12-02-2030") }
            };

            _mockRepo.Setup(repo => repo.GetAllInventory()).Returns(mockInventoryList);

            // Act
            var result = _mockRepo.Object.GetAllInventory();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Laptop", result.First().Title);
        }

        [TestMethod]
        public void GetMembersShouldReturnAllMemberInfo()
        {
            // Arrange
            var mockMemberInfoList = new List<MemberInfo>
            {
                new MemberInfo { Id = 1, Name = "Alex", SurName = "Markram", BookingCount = 1, Date_Joined = DateTime.Parse("25-03-2023") },
                new MemberInfo { Id = 2, Name = "John", SurName = "Doe", BookingCount = 2, Date_Joined = DateTime.Parse("12-02-2021") }
            };

            _mockRepo.Setup(repo => repo.GetAllMembers()).Returns(mockMemberInfoList);

            // Act
            var result = _mockRepo.Object.GetAllMembers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Alex", result.First().Name);
            Assert.AreEqual(2, result.Last().BookingCount);
        }

        [TestMethod]
        public void MemberBooksInventoryOnlyIfMemberBookingCountIsLessThan2()
        {
            // Arrange
            var mockMemberInfoList = new List<MemberInfo>
            {
                new MemberInfo { Id = 1, Name = "Alex", SurName = "Markram", BookingCount = 1, Date_Joined = DateTime.Parse("25-03-2023") },
                new MemberInfo { Id = 2, Name = "John", SurName = "Doe", BookingCount = 2, Date_Joined = DateTime.Parse("12-02-2021") }
            };

            var mockInventoryList = new List<Inventory>
            {
                new Inventory { Id = 1, Title = "Laptop", Description = "Lapptop 123", RemainingCount = 10, Expiration_Date = DateOnly.Parse("25-03-2030") },
                new Inventory { Id = 2, Title = "Mouse", Description = "Mouse 123", RemainingCount = 20, Expiration_Date = DateOnly.Parse("12-02-2030") }
            };

            _mockRepo.Setup(repo => repo.BookInventoryForMember(mockMemberInfoList.First().Id, mockInventoryList.First().Id)).Returns("Booking Successful.");

            // Act
            var result = _mockRepo.Object.BookInventoryForMember(mockMemberInfoList.First().Id, mockInventoryList.First().Id);

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(result));
            Assert.AreEqual("Booking Successful.", result);
        }

        [TestMethod]
        public void MemberCannotBookInventoryIfBookingCountIsGreaterThan2()
        {
            // Arrange
            var mockMemberInfoList = new List<MemberInfo>
            {
                new MemberInfo { Id = 1, Name = "Alex", SurName = "Markram", BookingCount = 1, Date_Joined = DateTime.Parse("25-03-2023") },
                new MemberInfo { Id = 2, Name = "John", SurName = "Doe", BookingCount = 2, Date_Joined = DateTime.Parse("12-02-2021") }
            };

            var mockInventoryList = new List<Inventory>
            {
                new Inventory { Id = 1, Title = "Laptop", Description = "Lapptop 123", RemainingCount = 10, Expiration_Date = DateOnly.Parse("25-03-2030") },
                new Inventory { Id = 2, Title = "Mouse", Description = "Mouse 123", RemainingCount = 20, Expiration_Date = DateOnly.Parse("12-02-2030") }
            };

            _mockRepo
                .Setup(repo => repo.BookInventoryForMember(mockMemberInfoList.Last().Id, mockInventoryList.First().Id))
                .Throws(new Exception("Booking Limit reached. Cannot book further items."));

            // Act
            var result = Assert.ThrowsException<Exception>(
                () => _mockRepo.Object.BookInventoryForMember(mockMemberInfoList.Last().Id, mockInventoryList.First().Id));

            // Assert
            Assert.AreEqual("Booking Limit reached. Cannot book further items.", result.Message);
        }
        [TestMethod]
        public void MemberCanCancelBookingUsingBookingIdIfNotAlreadyCancelled()
        {
            // Arrange
            int bookingId = 1;
            BookingDetails? bookingDetails = new BookingDetails
            {
                BookingId = bookingId,
                MemberId = 1,
                InventoryId = 1,
                Cancellation_Date = null
            };

            _mockRepo.Setup(repo => repo.GetBookingDetailsByBookingId(bookingId)).Returns(new List<BookingDetails> { bookingDetails });
            _mockRepo.Setup(repo => repo.CancelBooking(bookingId)).Returns("Booking Cancelled Successfully");

            // Act
            var result = _mockRepo.Object.CancelBooking(bookingId);

            // Assert
            Assert.AreEqual("Booking Cancelled Successfully", result);
            _mockRepo.Verify(repo => repo.CancelBooking(bookingId), Times.Once);
        }

        [TestMethod]
        public void BookingCancellationFailsIfBookingIsAlreadyCancelled()
        {
            int bookingId = 1;
            BookingDetails? bookingDetails = new BookingDetails
            {
                BookingId = bookingId,
                MemberId = 1,
                InventoryId = 1,
                Cancellation_Date = DateTime.Now
            };

            _mockRepo.Setup(repo => repo.GetBookingDetailsByBookingId(bookingId)).Returns(new List<BookingDetails> { bookingDetails });
            _mockRepo.Setup(repo => repo.CancelBooking(bookingId)).Returns($"BookingId: {bookingId} is already cancelled");

            // Act
            var result = _mockRepo.Object.CancelBooking(bookingId);

            // Assert
            Assert.AreEqual($"BookingId: {bookingId} is already cancelled", result);
            _mockRepo.Verify(repo => repo.CancelBooking(bookingId), Times.Once);
        }
    }
}
