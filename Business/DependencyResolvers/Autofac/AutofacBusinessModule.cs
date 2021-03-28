using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    //Bu proje ile ilgili olan autofac tanımlarını burada yaparız. genel tüm projelerde kullanılmasını istediğimiz
    //bir autofac konfigursayon için core katmanında bir yapılandırma yapılması gerekmektedir
    public class AutofacBusinessModule : Module //Autofac Module
    {
        //startup.cs de yapılan configureService methodunda IoC yapısını aslında burada kurmuş oluyoruz. 
        protected override void Load(ContainerBuilder builder)
        {
            //uygulama ayağa kalktığında çalışacak override method

            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); //IProductService istenirse geriye productManager instance ı döndür demek
            //içinde data tutulmayan nesneler için singleton kullanılır.Herkeste görünmesini/kullanmasını istediğimiz ortak yapılarda kullanılır
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            //Buraya logic şekilde hangi instance ların ne şartlar da üretileceğini yazabiliriz.

            //27-33 satırşaro arası : Reflection ile çalışma anında şu işleri yap demek...
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //register edilen tip için(assembly), gerçek zamanlı olrak implemente edilen interfaceleri bul, 
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() //onlar için yazmış olduğumuz AspectInterceporSelector sınıfını başlat (yani aspect kullan dedik)
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
