using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T> //T için IDataResult İmplementasyonusun demektir
    {
        public DataResult(T data,bool success,string message) : base(success,message)
        {
            //Result inherit almamızın nedeni kod tekrarını önlemek içindir. DataResult çalıştıktan sonra git base e success ve message bilgisini geç demektir
            Data = data;
        }
        public DataResult(T data, bool success):base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
