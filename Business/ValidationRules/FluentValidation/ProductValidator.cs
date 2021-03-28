using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        //Product için validasyon kodları burada yazılır
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);// product ın adı minimum 2 karakter olmalıdır gibi bir kural koyduk
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);// minimum fiyat 10 dan büyük olmalıdır ne zaman? kategori 1 olduğu zaman
            //TC kimlik no zorunludur. Ne zaman (TC vatandaşı olduğu zaman) gibi kurallar da yazılabilir
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");//ürün ismi a ile başlamalı diye bir kendimiz kural yazdık
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
