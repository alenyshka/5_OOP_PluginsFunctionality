using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton.Decorations;

namespace XML.serializaton.Factories
{
    class PinFactory : Factory
    {
        public override DecorationClass FactoryMethod()
        {
            return new Pin();
        }
    }
}
