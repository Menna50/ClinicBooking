using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClinicBooking.Shared.Results
{
    public class ResultT<T> : Result
    {
       public T Data { get; }
        //Success case
        private ResultT(bool isSuccess, int statusCode,Error error, T data) : base(isSuccess, statusCode,error)
        {
            Data = data;
        }
        //failure case
        private ResultT(bool isSuccess, int statusCode, Error error) : base(isSuccess,statusCode,error)
        {
           
        }
       
        public static ResultT<T> Success(int statusCode,T data) => new ResultT<T>(true,statusCode,null);
        public static ResultT<T> Failure(int statusCode,Error error) => new ResultT<T>(false,statusCode,error);

    }
}
