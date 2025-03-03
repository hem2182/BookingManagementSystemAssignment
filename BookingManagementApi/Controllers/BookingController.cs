using BookingManagementApi.Models;
using BookingManagementDataLayer;
using BookingManagementDataLayer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookingManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicyReactApp")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingSystemDb _bookingSystemDb;
        private readonly IWebHostEnvironment _env;

        public BookingController(IWebHostEnvironment env, IBookingSystemDb bookingSystemDb)
        {
            _env = env;
            _bookingSystemDb = bookingSystemDb;
        }

        [HttpGet("GetBookingDetails")]
        public IEnumerable<BookingDetails> Get()
        {
            return _bookingSystemDb.GetBookingDetailsData();
        }

        [HttpPost("ImportMembers")]
        [Consumes("multipart/form-data")]
        public IActionResult ImportMembers(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
            {
                return BadRequest("Only CSV files are allowed.");
            }

            // Create uploads directory if it does not exist
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            int recordsProcessed = 0;
            try
            {
                recordsProcessed = _bookingSystemDb.InsertMemberData(filePath);
            }
            catch (Exception ex)
            {
                return BadRequest($"Upload Data Failed. Error: {ex.Message}");
            }

            return Ok(new { Message = "File uploaded successfully!", RecordsProcessed = recordsProcessed });
        }

        [HttpPost("ImportInventory")]
        [Consumes("multipart/form-data")]
        public IActionResult ImportInventory(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
            {
                return BadRequest("Only CSV files are allowed.");
            }

            // Create uploads directory if it does not exist
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            int recordsProcessed = 0;
            try
            {
                recordsProcessed = _bookingSystemDb.InsertInventoryData(filePath);
            }
            catch (Exception ex)
            {
                return BadRequest($"Upload Data Failed. Error: {ex.Message}");
            }

            return Ok(new { Message = "File uploaded successfully!", RecordsProcessed = recordsProcessed });
        }

        [HttpPost("Book")]
        public IActionResult Book(int memberId, int inventoryId)
        {
            try
            {
                var message = _bookingSystemDb.BookInventoryForMember(memberId, inventoryId);
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return BadRequest($"Booking Failed. Error: {ex.Message}");
            }
        }

        [HttpPatch("Cancel")]
        public IActionResult Cancel(int bookingId)
        {
            try
            {
                var message = _bookingSystemDb.CancelBooking(bookingId);
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return BadRequest($"Booking Cancellation Failed. Error: {ex.Message}");
            }
        }
    }
}
