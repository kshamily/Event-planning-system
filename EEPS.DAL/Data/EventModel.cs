using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEPS.DAL.Data
{
    public class EventModel
    {

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Venue { get; set; }

        public DateTime EventDateTime { get; set; }

        public int? TotalExpectedGuest { get; set; }

        public int? TableSize { get; set; }

        public int? PercentOfEmptySeats { get; set; }
        public string FilePath { get; set; }

        public int CustomerID { get; set; }

        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
