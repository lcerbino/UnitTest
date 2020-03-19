using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    public class WrongPersonAgeException :Exception
    {
        public WrongPersonAgeException(int age) : base($"Age {age} is invalid. Should be greater than 18.") { }
    }
}
