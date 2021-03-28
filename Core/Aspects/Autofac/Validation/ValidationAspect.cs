using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Interception dan gelir. Aspect demektir(Methodun arasında bir yer de çalışacak şey Aspect dir)
    {
        //ADD methodu için anlatma referansı yapılmıştır..!!!
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //Validator type ver(Örn. ProductValidator)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//IValidator den inherit eden bir olmalıdır dedik
            {
                //throw new System.Exception(AspectMessages.WrongValidationType);
                throw new System.Exception("Bu bir doğrulama sınıfı değil!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) //MethodInterception dan override edilen method
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //reflection
            //_validator : ProductValidator, BaseType : AbstractValidator, GenericArgument: <Product>, [0] : ilk generic argüman demektir (ÖNEMLİ!!!)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //invocation: (Örn, Add Method için...) , Arguments: (Parametreler),birden fazla parametre olabilir o yüzden , entityType a denk gelen( yani Product) tipi bul demektir. 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); 
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); //validation tool merkezi bir noktadan yönetilmiş olur
            }
        }
    }
}
