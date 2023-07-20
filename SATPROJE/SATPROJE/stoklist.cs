using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SATPROJE
{
    public partial class stoklist : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public stoklist()
        {
            InitializeComponent();
        }
        void stokdoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=sat1.accdb");
            da = new OleDbDataAdapter("Select * from sat_stok", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "sat_stok");
            dataGridView1.DataSource = ds.Tables["sat_stok"];
            con.Close();
        }
        private void stokEkle_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO sat_stok (stok_isim, stok_sayi) VALUES ('" + textBox2.Text + "','" + textBox3.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            stokdoldur();

        }

        private void stoklist_Load(object sender, EventArgs e)
        {
            stokdoldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Verileri TextBox'lara aktarma
                textBox1.Text = row.Cells["stok_id"].Value.ToString();
                textBox2.Text = row.Cells["stok_isim"].Value.ToString();
                textBox3.Text = row.Cells["stok_sayi"].Value.ToString();
                // Diğer TextBox'ları da buraya ekleyebilirsiniz

                // İstediğiniz işlemleri yapabilirsiniz
            }
        }

        private void stokSil_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from sat_stok where stok_id=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            stokdoldur();
        }

        private void stokGuncelle_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE sat_stok set stok_isim='" + textBox2.Text + "',stok_sayi='" + textBox3.Text + "' ";
            cmd.ExecuteNonQuery();
            con.Close();
            stokdoldur();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }
    }
}
