using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest
{
    public interface ICommonData<T>
    {
        IList<T> GetAll();

        T GetById(int id);
    }
}
