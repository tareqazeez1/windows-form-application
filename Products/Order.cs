using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Products
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void BindDatatable()
        {
            SQLiteConnection conn = new SQLiteConnection(@"data source=D:\Database\Products.db");
            conn.Open();
            string query = "select * from Orders";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Maroon;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            BindDatatable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtProdName.Text = "";
            txtOriginalPrice.Text = "";
            txtPrdDesc.Text = "";
            textBox1.Text = "";
            txtMrkupPrice.Text = "";
            txtProdCode.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string connection = @"data source=D:\Database\Products.db";
            SQLiteConnection sqlite_conn = new SQLiteConnection(connection);
            string stringQuery = "insert into Orders (Barcode, ProductName, Description,Price, Category,Quantity) values ('" + txtProdCode.Text + "','" + txtProdName.Text + "','" + txtPrdDesc.Text + "','" + txtOriginalPrice.Text + "','" + textBox1.Text + "','"+txtMrkupPrice.Text+"');";
            sqlite_conn.Open();
            var SqliteCmd = new SQLiteCommand();
            SqliteCmd = sqlite_conn.CreateCommand();
            SqliteCmd.CommandText = stringQuery;
            SqliteCmd.ExecuteNonQuery();
            sqlite_conn.Close();
            BindDatatable();
            MessageBox.Show("Orders Created Successful");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           /* SQLiteConnection conn = new SQLiteConnection(@"data source=D:\Database\Products.db");
            string query = "select ProductName,Price,Description,Category from Product where Barcode='" + txtProdCode.Text + "'";
            SQLiteCommand Comm1 = new SQLiteCommand(query, conn);
            conn.Open();
            SQLiteDataReader DR1 = Comm1.ExecuteReader();
            if (DR1.Read())
            {
                txtProdName.Text = DR1.GetValue(0).ToString();
                txtOriginalPrice.Text = DR1.GetValue(1).ToString();
                txtPrdDesc.Text = DR1.GetValue(2).ToString();
                textBox1.Text = DR1.GetValue(3).ToString();
            }
            conn.Close();*/
        }
        private void GetDateFromProduct(string barcode)
        {
            try
            {

                DataTable dt = new DataTable();
                using (SQLiteConnection con = new SQLiteConnection(@"data source=D:\Database\Products.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(@"SELECT ProductsId,
       Barcode,
       Category,
       ProductName,
       Price,
       Description,
       Supplier
  FROM Product 



WHERE    [Barcode] = '" + barcode + "' ", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        SQLiteDataReader sdr = cmd.ExecuteReader();
                        dt.Load(sdr);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtPrdDesc.Text = row["Description"].ToString();
                        txtProdName.Text = row["ProductName"].ToString();
                        txtOriginalPrice.Text = row["Price"].ToString();
                        textBox1.Text = row["Category"].ToString();

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProdCode_Leave(object sender, EventArgs e)
        {
            if(txtProdCode.Text.Trim()!="")
            {
                GetDateFromProduct(txtProdCode.Text.Trim());
            }
        }
    }
}
