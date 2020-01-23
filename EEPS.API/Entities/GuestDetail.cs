using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Entities
{
    [Table("GuestDetails")]
    public class GuestDetail
    {
        [Key]
        public int GuestId { get; set; }
        [Required]
        public int GuestNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(500)]
        public string LastName { get; set; }
        public string SameTable { get; set; }
        public string NotSameTable { get; set; }
        public int AssignedTable { get; set; }
        [Required]
        public int EventId { get; set; }

        public EventDetail Event { get; set; }
    }
}
