using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        //Kategori ile ilgili dış dünyaya neyi servis etmek istiyorsak onları burada kodlarız.
        //Daha doğrusu referanslarını bildiriz. class içinde implemente ederek içini doldururuz.
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int categoryId);
    }
}
