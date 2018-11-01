using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Connection_Class
{
    /// <summary>
    /// This is a class Name "Connection_Class" to perform Insert, Update Delete Search options
    /// Show Data in DataGridView and also Perform SqlDataReader Options.
	/// می باشد datareader  و نمایش در دیتا گرید ویو و استفاده از  (Insert, Update Delete Search) است که برای دستورات  "Connection_Class" این یک کلاس با نام 
    /// ((HooMaN))
	/// </summary>
    public class Connection_Query
    {
        // string ConnectionString = "Data Source=.;Initial Catalog=Blit;Integrated Security=True";//method 1
        string ConnectionString = "server=.;database=Blit;trusted_connection=true";//method 2
        SqlConnection con;

        public void OpenConection()
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }

        /// <summary>
        /// نمونه کد
        /// q.ExecuteQueries("Insert into tblUsers(Uname, Pass) values('" + txtName.Text + "','" + txtTel.Text + "')"); //mehtod 1
        /// q.ExecuteQueries(string.Format("insert into tblusers values('{0}','{1}')", txtName.Text, txtTel.Text)); //method 2
        /// </summary>

        public void ExecuteQueries(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.Parameters.Clear();
            cmd.ExecuteNonQuery();
        }

        public SqlCommand ExecuteScaler(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.Parameters.Clear();
            cmd.ExecuteScalar();
            return cmd;
        }
        /// <summary>
        /// نمونه کد
        /// SqlDataReader dr = ClassObject.DataReader("Select * From Student");  
        /// dr.Read();  
        /// textBox1.Text = dr["Stdnt_Name"].tostring();
        /// </summary>

        public SqlDataReader DataReader(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// نمونه کد
        /// dataGridView1.datasource = ClassObject.ShowDataInGridView("Select * From Student")
        /// </summary>

        public object ShowData(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}