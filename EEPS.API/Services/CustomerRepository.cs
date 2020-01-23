using EEPS.API.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Services
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private EEPSDBContext _context;

        public CustomerRepository(EEPSDBContext context)
        {
            _context = context;
        }


        public void Dispose()
        {
            
        }
    }
}
