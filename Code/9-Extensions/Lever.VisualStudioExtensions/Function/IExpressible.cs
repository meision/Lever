using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision
{
    public interface IExpressible
    {
        void ParseExpression(string expression);
        string ToExpression();
    }
}
