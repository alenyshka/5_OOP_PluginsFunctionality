using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton;
using System.Threading.Tasks;


namespace Necklace
{
    class NecklacePlugin : IPlugin
    {
          public void Run(Form1 form1)
        {
            form1.extraTypes.Add(typeof(Necklace));
            form1.comboBox1.Items.Add("Necklace");
            form1.factory.Add(new NecklaceFactory());
            form1.labels.Add(new Necklace());
        }
    }
}
