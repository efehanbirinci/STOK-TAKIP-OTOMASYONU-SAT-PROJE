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
    public partial class musterilist : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public musterilist()
        {
            InitializeComponent();
        }
        void musteridoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=sat1.accdb");
            da = new OleDbDataAdapter("Select * from sat_musteri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "sat_musteri");
            dataGridView1.DataSource = ds.Tables["sat_musteri"];
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void musterilist_Load(object sender, EventArgs e)
        {
            musteridoldur();
            // Veritabanından stok isimlerini çekerek ComboBox'ı doldurun
            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sat1.accdb"))
            {
                con.Open();

                string query = "SELECT stok_isim FROM sat_stok";

                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox1.Items.Clear();

                        while (reader.Read())
                        {
                            string stokIsim = reader["stok_isim"].ToString();
                            comboBox1.Items.Add(stokIsim);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO sat_musteri (musteri_ad, musteri_soyad,musteri_mail,musteri_tel,musteri_stok_isim) VALUES ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            musteridoldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Verileri TextBox'lara aktarma
                textBox1.Text = row.Cells["musteri_id"].Value.ToString();
                textBox2.Text = row.Cells["musteri_ad"].Value.ToString();
                textBox3.Text = row.Cells["musteri_soyad"].Value.ToString();
                textBox4.Text = row.Cells["musteri_mail"].Value.ToString();
                textBox5.Text = row.Cells["musteri_tel"].Value.ToString();
                comboBox1.Text = row.Cells["musteri_stok_isim"].Value.ToString();
                // Diğer TextBox'ları da buraya ekleyebilirsiniz

                // İstediğiniz işlemleri yapabilirsiniz
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from sat_musteri where musteri_id=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            musteridoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE sat_musteri SET musteri_id=?, musteri_ad=?, musteri_soyad=?, musteri_mail=?, musteri_tel=?, musteri_stok_isim=?";
            cmd.Parameters.AddWithValue("@musteri_id", textBox1.Text);
            cmd.Parameters.AddWithValue("@musteri_ad", textBox2.Text);
            cmd.Parameters.AddWithValue("@musteri_soyad", textBox3.Text);
            cmd.Parameters.AddWithValue("@musteri_mail", textBox4.Text);
            cmd.Parameters.AddWithValue("@musteri_tel", textBox5.Text);
            cmd.Parameters.AddWithValue("@musteri_stok_isim", comboBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            musteridoldur();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
