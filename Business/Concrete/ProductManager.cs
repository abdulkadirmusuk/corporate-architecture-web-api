using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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

        [ValidationAspect(typeof(ProductValidator))]//Bu methodu product validator tipini kullanarak doğrula demektir. 
        public IResult Add(Product product)
        {
            //business codes
            //validation : buraya eklenecek  nesnenin(product) yapısal olarak uygun olup olmadığını kontrol etmeye doğrulama denir(örn: productname en az 3 karakter olmalı gibi ...)
            //business : iş kuralına uyuyuorsa ürün eklensin. örneğin 
            //fluent validation ile validation rule larımızı merkezi bir noktadan yöneteceğiz
            //Entity nesnelerinde gerekli alanları [Required] attribute ile geçmek riskli olabilir. SOLID e aykırı bir kullanımdır


            /*if (product.ProductName.Length<2)//validattion
            {
                //kötü kod
                return new ErrorResult(Messages.ProductNameInvalid);
            }*/

            //Cross Cutting Concerns(Log,Cache,Transaction, Authorization vs...)

            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            //Bir üstteki mekanizmayı cross cutting concerns altında validation olarak ortak bir yapıya kavuşturduk

            //ValidationTool.Validate(new ProductValidator(), product); //Bu koddan validation aspect attribute u ile kurtulmuş olduk


            _productDal.Add(product);
            return new Result(true,Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //Business Codes - DataAccess Layer Comminucations
            //Bir iş sınıfı başka katmanları new yapmaz. Injection ile abstract sınıf belirtilerek değişken türde nesneler izin verilmiş olur.
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>( _productDal.GetProductDetails());
        }
    }
}
