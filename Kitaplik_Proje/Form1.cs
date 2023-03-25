using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;

namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti=new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\asus\Desktop\Kitaplik111.mdb");
        void listele()
        {
            DataTable dt = new DataTable(); 
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void buttonliste_Click(object sender, EventArgs e)
        {
            listele();
            

        }
        string Durum = "";
        private void buttonkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            try
            {
                
                OleDbCommand komut1 = new OleDbCommand("insert into Kitaplar(KİtapAd,Yazar,Tur,Sayfa,Durum) values(@p1,@p2,@p3,@p4,@p5)", baglanti);
                komut1.Parameters.AddWithValue("@p1", textad.Text);
                komut1.Parameters.AddWithValue("@p2", textyazar.Text);
                komut1.Parameters.AddWithValue("@p3", combotur.Text);
                komut1.Parameters.AddWithValue("@p4", textsayfa.Text);
                komut1.Parameters.AddWithValue("@p5", Durum);

                komut1.ExecuteNonQuery();



                
                listele();

                MessageBox.Show("Kitap Sisteme Kaydetildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Durum = "0";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Durum = "1";
        }
        
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textkitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

            textad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            combotur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;

            }
            else
            {
                radioButton1.Checked = true;
            }
            
          
           


           

        }

        private void buttonsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("Delete From Kitaplar where Kitapid=@p1", baglanti);
            komut1.Parameters.AddWithValue("@p1",textkitapid.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void buttonguncel_Click(object sender, EventArgs e)
        {
            try
            {

                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update Kitaplar set Kitapad=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapid=@p6", baglanti);
                komut.Parameters.AddWithValue("@p1", textad.Text);
                komut.Parameters.AddWithValue("@p2", textyazar.Text);
                komut.Parameters.AddWithValue("@p3", combotur.Text);
                komut.Parameters.AddWithValue("@p4", textsayfa.Text);
                if (radioButton1.Checked == true)
                {
                    komut.Parameters.AddWithValue("@P5", Durum);
                }
                if (radioButton2.Checked == true)
                {
                    komut.Parameters.AddWithValue("@p5", Durum);
                }



                komut.Parameters.AddWithValue("@p6", textkitapid.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OleDbCommand komut = new OleDbCommand("select  * From Kitaplar where Kitapad=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", textkitapbul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da=new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource= dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("select  * From Kitaplar where Kitapad like'%"+textkitapbul.Text+"%'", baglanti);
            komut.Parameters.AddWithValue("@p1", textkitapbul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }
    }
}

