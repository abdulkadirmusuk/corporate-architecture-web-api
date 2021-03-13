using Business.Abstract;
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

        public List<Category> GetAll()
        {
            //İş kodları yazılır...
            return _categoryDal.GetAll();
        }

        public Category GetById(int categoryId)
        {
            //select * from categories where CategoryId = categoryId işini yapmış olduk
            return _categoryDal.Get(c => c.CategoryId == categoryId);
        }
    }
}
