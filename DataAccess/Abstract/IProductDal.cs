
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //IProductDal nesnesini kullanmmak için IEntityRepository interface ini product için yapılandırdık
        List<ProductDetailDto> GetProductDetails(); //Ürünün detaylarını getirecek detail nesnesi
    }
    //IEntityRepository interface i daha önce dataAccess katmanındaydı. Şimdi core a taşıdık. Core.DataAccess den referans aldık
}
