using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        //Kategori nesnemi dış dünyaya açtım.(Get ve GetAll methodları ile)
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            //İş kodları yazılır...
            return new SuccessDataResult<List<Category>>( _categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            //select * from categories where CategoryId = categoryId işini yapmış olduk
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
