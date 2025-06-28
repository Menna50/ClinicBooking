using ClinicBooking.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.MennaRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepo(AppDbContext appDbContext)
            {
            _appDbContext = appDbContext;                   
            }
        public async Task< IEnumerable<T>> GetAllAsync()
        {
            var set =  _appDbContext.Set<T>();
            int x = 0;
            return set;
        }
    }
   
}
