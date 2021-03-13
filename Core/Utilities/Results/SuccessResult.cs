using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
            //Bu yapı şu anlama gelmektedir.
            //SuccessResult new yapıldığı anda içine bir mesaj ister.
            //Daha sonra base olan sınıf yani (Result) çalışssın içine de true ve message değeri gitsin demektir
        }
        public SuccessResult():base(true)
        {
            //mesaj vermeden success için result döndür demektir
        }
    }
}
