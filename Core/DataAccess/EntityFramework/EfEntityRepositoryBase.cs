using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //Her Nesne için EntityRepository oluşturmak yerine core da generic bir entity ve database context ile çalışabilen
    //bir yapı tasrladık. EntityFramework teknolojisini kullandığımız için onu klasörleyerek bağımsızlaştırdık.
    //Başka bir yapıya geçerken farklı bir klasör açarak ona uygun teknoloji için kodlamalar yapılarak SOLID yapı sağlanmış olur
    public class EfEntityRepositoryBase<TEntity,TContext>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
    {

    }
}
