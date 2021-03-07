using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new InMemoryProductDal());//ProductManager nesnesinin çalıştığı tip değişebilir. Sadece çalışacağı nesnenin interface ini bildirmesi yeterlidir.
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
            
        }
    }
}
