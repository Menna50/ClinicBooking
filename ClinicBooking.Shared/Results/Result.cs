using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; } 
        public int StatusCode { get; }
        //success Case
        protected Result(bool isSuccess,int statusCode,Error error)
        {
            // يمكنك تعطيل هذه التحققات مؤقتًا
            // if (isSuccess && (!string.IsNullOrEmpty(error) || statusCode >= 400))
            // {
            //     throw new InvalidOperationException("Successful result cannot have an error message or error status code.");
            // }
            // if (!isSuccess && string.IsNullOrEmpty(error))
            // {
            //     throw new InvalidOperationException("Failure result must have an error message.");
            // }
            // if (!isSuccess && statusCode < 400)
            // {
            //     throw new InvalidOperationException("Failure result must have an error status code (>= 400).");
            // }
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Error = error ;
        }
       
        public static Result Success(int statusCode) => new Result(true,statusCode,null);
        public static Result Failure(int statusCode,Error error) => new Result(false,statusCode, error);
    }
}
