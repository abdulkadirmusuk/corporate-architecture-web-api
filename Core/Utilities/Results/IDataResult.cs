using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //void olmayan,geriye bir nesne döndürecek olan methodların result referansı
    public interface IDataResult<T> : IResult //hangi tip geriye dönecek ? <T>
    {
        //IResult implemente etmemizin sebebi. zaten yazılmış olan bir şeyi tekrar etmemek için
        T Data { get; } // IResult sayesinde geri success ve message değerleri döner. Çalışılacak tipe ait data ları da dönmek için bunu yazdık
    }
}
