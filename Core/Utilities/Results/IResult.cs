using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç
    public interface IResult
    {
        bool Success { get; } //işlem başarılı mı? kontrolü
        string Message { get; }//İşlem sonucu üretilen,web api den dışarıya bilgi veren mesaj
    }
}
