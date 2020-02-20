using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WingsOn.Services
{
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException()
        {

        }

        public ValueNotFoundException(string entityName, object value)
            : base(string.Format("Could not find value {1} in {0}", entityName,value))
        {

        }
    }
}
