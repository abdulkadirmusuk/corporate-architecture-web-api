using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    //static, tek bir instance oluşur. uygulama belleği sürekli onu kullanır.
    public static class ValidationTool
    {
        //IValidator abstract bir nesnedir. validate methodunun referansını tutar
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
