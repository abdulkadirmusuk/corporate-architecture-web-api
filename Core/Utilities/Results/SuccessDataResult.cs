using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message):base(data,true,message)
        {

        }
        public SuccessDataResult(T data):base(data,true)
        {

        }
        public SuccessDataResult(string message):base(default,true,message)
        {
            //geriye data döndürmek istemiyoum dersem, data yerine default vermeliyim. default demek , T türünde data demektir
        }
        public SuccessDataResult():base(default,true)
        {

        }
    }
}
