using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        //SOLID
        //Open Closed Principle - Eklenen yeni bir şey mevcut sistemi bozmamalı
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());//ProductManager nesnesinin çalıştığı tip değişebilir. Sadece çalışacağı nesnenin interface ini bildirmesi yeterlidir.
            /*foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }*/
            /*foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }*/
            foreach (var product in productManager.GetByUnitPrice(50,100))
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }
}
