using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingManagementDataLayer.Models
{
    public class Inventory
    {
        [Key]
        [Optional]
        public int Id { get;  set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateOnly Expiration_Date { get; set; }
    }
}
