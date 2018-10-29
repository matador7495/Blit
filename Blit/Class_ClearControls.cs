using System;
using System.Windows.Forms;

namespace ClearClass
{
    class ClearControls
    {
        /// <summary>
        /// ///پاک کردن تمام تکس باکس در هر جای فرم
        /// </summary>
   
        public static void ClearTextBoxes(Control texbox)
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };
            func(texbox.Controls);
        }
        /// <summary>
        /// پاک کردن تکس باکس هایی که بروی فرم باشد
        /// در کل با 
        /// this
        /// (اسم اون کنترل)
        /// میتونیم راحت هرکدام رو خواستیم پاک کنیم
        /// </summary>
       
        public static void clearalltextbox(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    TextBox t = (TextBox)c;
                    t.Text = "";
                }
            }
        }
        /// <summary>
        /// پاک کردن تمام اشیا رو یک فرم
        /// فقط شامل کنترل های روی فرم میشه
        /// </summary>
       
        public static void ClearFormControls(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtbox = (TextBox)control;
                    txtbox.Text = string.Empty;
                }
                else if (control is CheckBox)
                {
                    CheckBox chkbox = (CheckBox)control;
                    chkbox.Checked = false;
                }
                else if (control is RadioButton)
                {
                    RadioButton rdbtn = (RadioButton)control;
                    rdbtn.Checked = false;
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)control;
                    dtp.Value = DateTime.Now;
                }
            }
        }
    }
}