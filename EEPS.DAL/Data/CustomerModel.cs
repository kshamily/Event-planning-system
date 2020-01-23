using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.DAL.Data
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
    }
}
