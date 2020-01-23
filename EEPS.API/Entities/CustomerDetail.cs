using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Entities
{
    [Table("CustomerDetails")]
    public class CustomerDetail
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Email { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }

        public UserDetail User { get; set; }
    }
}
