using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.MennaRepo
{
    public interface IGenericRepo<T> 
    {
     public  Task< IEnumerable<T>> GetAllAsync();
    }
}
