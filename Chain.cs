using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML.serializaton.Decorations
{
    public class Chain : DecorationClass
    {
        public int Length { get; set; }
        public bool Clasp { get; set; }
        public Chain() { }
        public override void GetValues(List<string> FieldList)
        {
            base.GetValues(FieldList);
            try
            {
                FieldList.Add(Convert.ToString(Length));
                FieldList.Add(Convert.ToString(Clasp));
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
            try
            {
                Length = Convert.ToInt32(FieldList[i++]);
                Clasp = Convert.ToBoolean(FieldList[i]);
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
            form.label4.Text = "Lenght:";
            form.label5.Text = "Clasp:";
            form.label6.Text = "enter true or false";
            form.textBox5.Visible = false;
            form.radioButton1.Visible = true;
            form.radioButton2.Visible = true;
        }
    }
}
