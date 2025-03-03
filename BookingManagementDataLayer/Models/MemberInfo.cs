using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingManagementDataLayer.Models
{
    public class MemberInfo
    {
        [Key]
        [Optional]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int BookingCount { get; set; }
        public DateTime Date_Joined { get; set; }
    }

}
