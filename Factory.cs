using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton.Decorations;

namespace XML.serializaton.Factories
{
    public abstract class Factory
    {
        public abstract DecorationClass FactoryMethod();
    }
}
