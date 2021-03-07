using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //Generic Constraint - Generic Kısıtlar
    //class:referans tip
    //IEntity: IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new(): new() lenebilir olmalı. gibi bu interface i kullanan yerlere kısıtlar koymuş olduk
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //Her nesne için aynı yapıyı sürekli oluşturmak yerine. T Generic type ile çalışmalıyız
        List<T> GetAll(Expression<Func<T,bool>> filter =null);//Nesneyi belirli filtreye göre getirmek için LINQ ile filtreleme işlemi
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //List<T> GeAllByCategory(int categoryId); //Bu koda ihtiyaç kalmadı. Expression ile yukarıda çözmüş olduk
    }
}
