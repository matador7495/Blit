using System;
using System.Windows.Forms;
using Connection_Class;
using ClearClass;

namespace Blit
{
    public partial class frmGroups : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();

        public frmGroups()
        {
            InitializeComponent();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries(string.Format("insert into tblGroups values('{0}')", txtName.Text));
                query.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries("delete from tblGroups where ID=" + txtCode.Text);
                query.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}