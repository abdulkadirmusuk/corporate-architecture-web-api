using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {

        public Result(bool success, string message):this(success) //this(success) demek iki ctor u da çalıştır demektir
        {
            Message = message; //readonly prop lar ctor da set edilebilir
        }
        public Result(bool success)
        {
            //geriye mesaj dönmeden success verme
            Success = success;
        }

        public bool Success { get; }//readonly

        public string Message { get; }//readonly

    }
}
