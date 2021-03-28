using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //Bu method class, method için çalışsın ve birden fazla kullanılabilsin ve inherit edilen yerde de çalışşsın demektir
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor//IInterceptor : castle dynamic proxy den gelir. (Autofac)
    {
        public int Priority { get; set; }//Hangi attritube önce çalışsın onu belirleyecek property

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
