using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms.ComboBox;

namespace Hairpin
{
    class HairpinPlugins : IPlugin
    {
        public void Run(Form1 form1)
        {
            form1.extraTypes.Add(typeof(Hairpin));
            form1.comboBox1.Items.Add("Hairpin");
            form1.factory.Add(new HairpinFactory());
            form1.labels.Add(new Hairpin());            
        }
    }
}
