using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Entities
{
    [Table("UserDetails")]
    public class UserDetail
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(500)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(500)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
        [Required]
        [MaxLength(500)]
        public string Email { get; set; }
        [Required]
        [MaxLength(500)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string Role { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
