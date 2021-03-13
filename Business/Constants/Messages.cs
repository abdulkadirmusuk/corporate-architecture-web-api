using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //static kullandık. uygulama boyunca bu instance ayakta olacak. new yapılmayacak demektir
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz"; 
        public static string MaintenanceTime = "Sistemde bakımı var! Daha sonra tekrar deneyiniz...";
        public static string ProductListed = "Ürünler listelendi";
    }
}
