using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton.Factories;
using XML.serializaton.Decorations;

namespace Bracelet
{
    class BraceletFactory : Factory
    {
        public override DecorationClass FactoryMethod()
        {
            return new Bracelet();
        }
    }
}
