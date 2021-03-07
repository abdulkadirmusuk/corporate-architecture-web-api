using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; //Injection

        public ProductManager(IProductDal productDal)
        {
            //Burası interface sayesinde bir yere bağımlı olmaz. EntityFramework, Depper, NHibernate gibi başka ORM leri kullanabilir
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //Business Codes - DataAccess Layer Comminucations
            //Bir iş sınıfı başka katmanları new yapmaz. Injection ile abstract sınıf belirtilerek değişken türde nesneler izin verilmiş olur.
            return _productDal.GetAll();
        }
    }
}
