using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //invocation(method) ların ne zaman çalışacağını belirler. onBefore(önce) çalıştır demektir gibi vs...
        protected virtual void OnBefore(IInvocation invocation) { } // önce 
        protected virtual void OnAfter(IInvocation invocation) { } // sonra
        protected virtual void OnException(IInvocation invocation, System.Exception e) { } //çalışma anında
        protected virtual void OnSuccess(IInvocation invocation) { } //başarılı olduğunda
        public override void Intercept(IInvocation invocation) //Intercept edilecek method(invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
