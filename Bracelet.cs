using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML.serializaton;
using XML.serializaton.Decorations;
using System.Windows.Forms;


namespace Bracelet
{
    public class Bracelet : DecorationClass
    {
        public int Length { get; set; }
        public bool Clasp { get; set; }
        public Bracelet() { }
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
            form.label4.Text = "Lenght:";
            form.label5.Text = "Clasp:";
            form.textBox5.Visible = false;
            form.radioButton1.Visible = true;
            form.radioButton2.Visible = true;
        } 
    }
}
