using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML.serializaton.Decorations
{
    public class DecorationClass
    {
        public string Object { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Material { get; set; }

        public DecorationClass() { }

        public virtual void GetValues(List<string> FieldList)
        {
            FieldList.Add(Object);
            FieldList.Add(Name);
            FieldList.Add(Convert.ToString(Weight));
            FieldList.Add(Material);
            
        }

        public virtual void SetValues(List<string> FieldList)
        {
            int i = 0;
            Object = FieldList[i++];
            Name = FieldList[i++];
            Weight = Convert.ToInt32(FieldList[i++]);
            Material = FieldList[i++];
        }
        public virtual void SetLabels(Form1 form1)
        {
            form1.textBox1.Visible = true;
            form1.textBox2.Visible = true;
            form1.textBox3.Visible = true;
            form1.textBox4.Visible = true;
            form1.textBox5.Visible = true;
            form1.label1.Visible = true;
            form1.label2.Visible = true;
            form1.label3.Visible = true;
            form1.label4.Visible = true;
            form1.label5.Visible = true;
            form1.label6.Visible = false;
            form1.radioButton1.Visible = false;
            form1.radioButton2.Visible = false;
            form1.textBoxInfo.Visible = true;          
        }
    }
}
