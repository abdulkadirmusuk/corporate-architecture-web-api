using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //IProductDal nesnesini kullanmmak için IEntityRepository interface ini product için yapılandırdık
    }
}
