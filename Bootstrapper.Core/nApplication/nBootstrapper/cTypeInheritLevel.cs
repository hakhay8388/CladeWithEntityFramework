using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nBootstrapper
{
    public class cTypeInheritLevel
    {
        public Type Type { get; private set; }
        public int InheritLevel { get; private set; }

        public cTypeInheritLevel(Type _Type, int _InheritLevel)
        {
            Type = _Type;
            InheritLevel = _InheritLevel;
        }
    }
}
