using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton.Factories;
using XML.serializaton.Decorations;

namespace Necklace
{
    class NecklaceFactory : Factory
    {
        public override DecorationClass FactoryMethod()
        {
            return new Necklace();
        }
    }
}
