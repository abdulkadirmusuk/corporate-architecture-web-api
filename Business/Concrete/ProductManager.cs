using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; //Injection
        //ILogger _logger;
        //!!(BİR MANAGER SINFI, KENDİSİNİN HARİÇ BAŞKA BİR DAL NESNESİNİ ENJEKTE EDEMEZ. ÖRN, BURADA CATEGORY DAL ENJEKTE EDİLEMEZ). onun yerine servis nesnesi enjecte edilir
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal/*,ILogger logger*/,ICategoryService categoryService)
        {
            //Burası interface sayesinde bir yere bağımlı olmaz. EntityFramework, Depper, NHibernate gibi başka ORM leri kullanabilir
            _productDal = productDal;
            //_logger = logger;
            _categoryService = categoryService;
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

            //Geleneksel yöntem _logger implementasyonu
            /*_logger.Log();
            try
            {
                //business codes...
                _productDal.Add(product);
                return new SuccessResult( Messages.ProductAdded);
            }
            catch (Exception exception)
            {
                _logger.Log();
            }
            return new ErrorResult();
            */

            //Bir manager nesnesinde aynı iş kuralı birden fazla methotta kullanılıyor ise. Onu private şeklinde ortak bir methoda almalıyız.
            //Aynı kural farklı methodların içinde tekrar edilmemelidir. dont repeat your self!

            //Bu iyi bir koddur ancak karmaşıklığa sebep olabilir
            /*if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                if (CheckIfProductNameExist(product.ProductName).Success)
                {
                    _productDal.Add(product);
                    return new SuccessResult(Messages.ProductAdded);
                }
            }*/

            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());

            if (result !=null)
            {
                return result;//Eğer hata varsa geri hata dön demektir
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();//Any : Bu kayıttan var mı?
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CheckIfCategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
