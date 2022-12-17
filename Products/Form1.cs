using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Products
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Phone");
            comboBox1.Items.Add("Laptop");
            comboBox1.Items.Add("Technology Product");

            BindDatatable();
        }

        private void BindDatatable()
        {
            SQLiteConnection conn = new SQLiteConnection(@"data source=D:\Database\Products.db");
            conn.Open();
            string query = "select * from Product";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            if (dataGridView1.Columns.Contains("buttonColumn")) { } else
                {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Click";
                btn.Text = "Delete";
                btn.Name = "buttonColumn";
                btn.UseColumnTextForButtonValue = true;
                btn.DefaultCellStyle.ForeColor = Color.White;
                btn.DefaultCellStyle.BackColor = Color.Red;
            }
            //Add the Button Column.
            //DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            //buttonColumn.HeaderText = "";
            //buttonColumn.Width = 60;
            //buttonColumn.Name = "buttonColumn";
            //buttonColumn.Text = "Delete";
            //buttonColumn.DefaultCellStyle.ForeColor = Color.White;
            //buttonColumn.DefaultCellStyle.BackColor = Color.Red;
            //buttonColumn.UseColumnTextForButtonValue = true;
            //dataGridView1.Columns.Insert(7, buttonColumn);

            DataGridViewButtonColumn c = (DataGridViewButtonColumn)dataGridView1.Columns["buttonColumn"];
            c.FlatStyle = FlatStyle.Popup;
            c.DefaultCellStyle.ForeColor = Color.White;
            c.DefaultCellStyle.BackColor = Color.Red;
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Maroon;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string connection = @"data source=D:\Database\Products.db";
                SQLiteConnection sqlite_conn = new SQLiteConnection(connection);
                string stringQuery = "delete from Product where Barcode='" + row.Cells["Barcode"].Value + "'";
                sqlite_conn.Open();
                var SqliteCmd = new SQLiteCommand();
                SqliteCmd = sqlite_conn.CreateCommand();
                SqliteCmd.CommandText = stringQuery;
                SqliteCmd.ExecuteNonQuery();
                sqlite_conn.Close();
                BindDatatable();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtPrdDesc.Text = "";
            txtOriginalPrice.Text = "";
            txtMrkupPrice.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connection = @"data source=D:\Database\Products.db";
            SQLiteConnection sqlite_conn = new SQLiteConnection(connection);
            string stringQuery = "insert into Product (Barcode, ProductName, Price,Description,Supplier, Category) values ('" + txtProdCode.Text + "','" + txtProdName.Text + "','" + txtOriginalPrice.Text + "','" + txtPrdDesc.Text + "','" + txtMrkupPrice.Text + "','" + comboBox1.SelectedItem.ToString() + "');";
            sqlite_conn.Open();
            var SqliteCmd = new SQLiteCommand();
            SqliteCmd = sqlite_conn.CreateCommand();
            SqliteCmd.CommandText = stringQuery;
            SqliteCmd.ExecuteNonQuery();
            sqlite_conn.Close();
            BindDatatable();
            MessageBox.Show("Product Created Successful");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /* string connection = @"data source=D:\Database\Products.db";
             SQLiteConnection sqlite_conn = new SQLiteConnection(connection);
             string stringQuery = "delete from Product where Barcode='" + txtProdCode.Text + "'";
             sqlite_conn.Open();
             var SqliteCmd = new SQLiteCommand();
             SqliteCmd = sqlite_conn.CreateCommand();
             SqliteCmd.CommandText = stringQuery;
             SqliteCmd.ExecuteNonQuery();
             sqlite_conn.Close();
             BindDatatable();*/

            Order o = new Order();
            o.ShowDialog();
        }
    }
}