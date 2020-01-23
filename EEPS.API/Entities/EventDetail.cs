using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Entities
{
    [Table("EventDetails")]
    public class EventDetail
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [MaxLength(500)]
        public string EventName { get; set; }
        [Required]
        [MaxLength(500)]
        public string Venue { get; set; }
        [Required]
        public DateTime EventDateTime { get; set; }
        [Required]
        public int TotalExpectedGuest { get; set; }
        [Required]
        public int TableSize { get; set; }
        [Required]
        public int PercentOfEmptySeats { get; set; }
        public string FilePath { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int UserId { get; set; }

        public UserDetail User { get; set; }
        public CustomerDetail Customer { get; set; }

        public ICollection<GuestDetail> GuestDetails { get; set; }

    }
}
