using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //Her Nesne için EntityRepository oluşturmak yerine core da generic bir entity ve database context ile çalışabilen
    //bir yapı tasrladık. EntityFramework teknolojisini kullandığımız için onu klasörleyerek bağımsızlaştırdık.
    //Başka bir yapıya geçerken farklı bir klasör açarak ona uygun teknoloji için kodlamalar yapılarak SOLID yapı sağlanmış olur
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
        //IEntityRepositoryden inherit ederek. Tüm entitylere uygun CRUD operasyon sınıfı yazmış olacağız
    {
        public void Add(TEntity entity)
        {
            //IDispossable pattern implementation of c#, Garbage Collector runner
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); // gelen product nesnesinin referansını yakala
                addedEntity.State = EntityState.Added;//Hangi işlemi yapacağını bildir
                context.SaveChanges();//değişiklikleri kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            //Turnary operatör ile filtre durumuna göre çalıştırma
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
