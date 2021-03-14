using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Burada başka bir sınıfın somut nesnelerini new() yapamayız veya kullanamayız. Çünkü iş kurallarında değişimler olabilir
        //Doğrudak product manager sınıfını kullanmak yerine onun implemente ettiği referans tutucu yani interface i (IProductService) ı burada kullanmalıyız
        //Burada IDal nesneleri ve business somutları new() leri kullanılmaz
        //IoC Container- Inversion of Control
        private IProductService _productService;

        public ProductsController(IProductService productService)//Gevşek bağlılık(Loosely coupled)
        {
            //IProductService manager ın referansını tutabiliyor. Buraya doğrudan somut bir nesne veremeyiz bağımlı oluruz. IoC container ile çözüm sağlarız
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }
    }
}
