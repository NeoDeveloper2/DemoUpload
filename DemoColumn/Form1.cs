using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoColumn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string APIName = "Employee";
            DataTable dt = new DataTable();
            DataTable dtColumnMapping = new DataTable();
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.GetDataTable("GetAllEmployee", dt, CommandType.StoredProcedure);

            SQLHelper sqlHelper1 = new SQLHelper();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@APIName", SqlDbType = SqlDbType.VarChar, Value= APIName}
            };
            sqlHelper1.GetDataTable("GetAllColumnMapping", dtColumnMapping, CommandType.StoredProcedure, sp);

            foreach (DataRow dr in dtColumnMapping.Rows)
            {
                string col = Convert.ToString(dr["BaseColumn"]);
                dt.Columns[col].ColumnName = Convert.ToString(dr["DestinationColumn"]);
                dt.AcceptChanges();
            }
            dataGridView1.DataSource = dt;
        }
    }
}
