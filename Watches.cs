using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XML.serializaton.Decorations
{
    public class Watches : DecorationClass
    {
        public string Type { get; set; }
        public string Mechanism { get; set; }
        public Watches() { }
        public override void GetValues(List<string> FieldList)
        {
            base.GetValues(FieldList);
            FieldList.Add(Type);
            FieldList.Add(Mechanism);
        }

        public override void SetValues(List<string> FieldList)
        {
            base.SetValues(FieldList);
            int i = 4;
            Type = FieldList[i++];
            Mechanism = FieldList[i];
        }
        public override void SetLabels(Form1 form1)
        {
            base.SetLabels(form1);
            form1.label4.Text = "Type:";
            form1.label5.Text = "Mechanism:";
        }
    }
}
