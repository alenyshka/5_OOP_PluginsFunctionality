using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XML.serializaton.Decorations
{
    public class Earrings : DecorationClass
    {
        public string Form { get; set; }
        public bool Gems { get; set; }
        public Earrings() { }
        public override void GetValues(List<string> FieldList)
        {
            base.GetValues(FieldList);
            FieldList.Add(Form);
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
            Form = FieldList[i++];
            Gems = Convert.ToBoolean(FieldList[i]);
        }
        public override void SetLabels(Form1 form)
        {
            base.SetLabels(form);
            form.label6.Visible = true;
            form.label4.Text = "Form:";
            form.label5.Text = "Gems:";
            form.label6.Text = "enter true or false";
            form.textBox5.Visible = false;
            form.radioButton1.Visible = true;
            form.radioButton2.Visible = true;
        }
    }
}
