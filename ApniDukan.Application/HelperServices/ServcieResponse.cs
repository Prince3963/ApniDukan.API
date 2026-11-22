using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApniDukan.Application.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }

        public static ServiceResponse<T> SuccessResponse(
            T data,
            string message)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Message = message,
                Success = true
            };
        }

        public static ServiceResponse<T> FailureResponse(string message)
        {
            return new ServiceResponse<T>
            {
                Data = default,
                Message = message,
                Success = false
            };
        }
    }
}
