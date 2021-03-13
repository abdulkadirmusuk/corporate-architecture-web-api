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
            ProductTest(); 
            //CategoryTest(); Category için kodlarım

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
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


            /*foreach (var product in productManager.GetByUnitPrice(50, 100))
            {
                Console.WriteLine(product.ProductName);
            }*/

            var result = productManager.GetProductDetails();
            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " - " + product.CategoryName);
                }

            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}
