using ClearClass;
using Connection_Class;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmNote : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmNote()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, txtNote.Text, Encoding.UTF8);
                    MessageBox.Show("یادداشت با موفقیت ذخیره شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearControls.ClearTextBoxes(this);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}