using Connection_Class;
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
    public partial class frml : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frml()
        {
            InitializeComponent();
        }
    }
}
