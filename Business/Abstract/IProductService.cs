using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        //IResult ile sadece mesaj bilgisi döner, IDataResult ile mesaj ile birlikte geriye bir data döneriz
        IDataResult<List<Product>> GetAll();//Geriye IDataResult ile product listesi döndürür.
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product); //void döndürmek yerine result döndür dedik.Utilities den gelir
    }
}
