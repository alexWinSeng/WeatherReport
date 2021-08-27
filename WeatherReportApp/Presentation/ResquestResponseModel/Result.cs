
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.ResquestResponseModel
{
    public class Result<T>
        where T : BaseResponse, new()
    {
        public Result()
        {
            ErrorCode = Constants.ResponseCodes.Success;
            Data = new T()
            {
                
            };
        }

        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
