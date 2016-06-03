using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XML.serializaton.Decorations
{
    public class Ring : DecorationClass
    {
        public string Type { get; set; }
        public bool Gems { get; set; }
        public Ring() { }
        public override void GetValues(List<string> FieldList)
        {
            base.GetValues(FieldList);
            FieldList.Add(Type);
            try
            {
                FieldList.Add(Convert.ToString(Gems));
            }
            catch
            {
                MessageBox.Show("Enter corrected date. The object doesn't added.");
            }
            
        }

        public override void SetValues(List<string> FieldList)
        {
            base.SetValues(FieldList);
            int i = 4;
            Type = FieldList[i++];
            try
            {
                Gems = Convert.ToBoolean(FieldList[i]);
            }
            catch
            {
                MessageBox.Show("Enter corrected date. The object doesn't added.");
            }
        }
        public override void SetLabels(Form1 form)
        {
            base.SetLabels(form);
            form.label6.Visible = true;
            form.label4.Text = "Type:";
            form.label5.Text = "Gems:";
            form.label6.Text = "enter true or false";
            form.textBox5.Visible = false;
            form.radioButton1.Visible = true;
            form.radioButton2.Visible = true;
        }
    }
}
