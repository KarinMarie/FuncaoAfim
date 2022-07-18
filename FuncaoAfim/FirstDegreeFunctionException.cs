using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncaoAfim
{
    class FirstDegreeFunctionException : Exception
    {
        public override string Message => "Em uma função linear, o A não pode ser igual a zero!";
    }
}
