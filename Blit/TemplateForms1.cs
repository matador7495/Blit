using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blit
{
    public partial class TemplateForms1 : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public TemplateForms1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Bilit;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
