using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    public class MissingPersonNameException : Exception
    {
        public MissingPersonNameException() : base("Name is missing") { }
    }
}
