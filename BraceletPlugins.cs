using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bracelet
{
    class BraceletPlugins : IPlugin
    {
        public void Run(Form1 form1)
        {
            form1.extraTypes.Add(typeof(Bracelet));
            form1.comboBox1.Items.Add("Bracelet");
            form1.factory.Add(new BraceletFactory());
            form1.labels.Add(new Bracelet());
        }
    }
}
