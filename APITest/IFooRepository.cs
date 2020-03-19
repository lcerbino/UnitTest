using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest
{
    public interface IFooRepository<T>
    {
         T GetById(int id);

         IList<T> GetAll();

         void AddEntity(T entity);

         bool UpdateEntity(T entity);

         bool DeleteEntity(int id);
    }
}
