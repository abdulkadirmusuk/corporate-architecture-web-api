using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //İş kurallarını burdan yöneteceğiz.
        public static IResult Run(params IResult[] logics)
        {
            //Her bir logic burada çalıştırılır. yanlış ise geriye döndürülür ve method hata verilerek işlem iptal edilir.
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;//kurala uymayan ı geri döndür
                }
            }
            return null;
        }
    }
}
