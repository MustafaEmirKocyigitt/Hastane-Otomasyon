using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HastaneProjesi
{
    public partial class FrnSekreterDetay : Form
    {
        public FrnSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void FrnSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara;

            //Ad Soyad
            SqlCommand kmt1 = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTc=@p1", bgl.baglanti());
            kmt1.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr1 = kmt1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close(); 
            
            //Branşları Datagride Aktarma
            DataTable dt =new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;   

            //Doktorları Listeye Akatarma
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd +' '+DoktorSoyad) as Doktorlar,DoktorBrans from Tbl_Doktorlar",bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branş ComboBox
            SqlCommand kmt2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = kmt2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Tbl_Randevular", bgl.baglanti());
            DataTable dt3 = new DataTable();
            sqlDataAdapter.Fill(dt3);
            dataGridView3.DataSource = dt3;
            
            bgl.baglanti().Close();
            
        }

        public void Listele()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Tbl_Randevular", bgl.baglanti());
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGridView3.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update Tbl_Randevular set RandevuTarih=@p1,RandevuSaat=@p2,RandevuBrans=@p3,RandevuDoktor=@p4,RandevuDurum=@p5,HastaTc=@p6 where Randevuid=@p7", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@p1", MskTarih.Text);
            guncelle.Parameters.AddWithValue("@p2", MskSaat.Text);
            guncelle.Parameters.AddWithValue("@p3", CmbBrans.Text);
            guncelle.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            if (ChkDurum.Checked = true)
            {
                guncelle.Parameters.AddWithValue("@p5", 1);

            }
            else
            {
                guncelle.Parameters.AddWithValue("@p5", 0);

            }
            guncelle.Parameters.AddWithValue("@p6", MskTC.Text);
            guncelle.Parameters.AddWithValue("@p7", Txtid.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            sqlCommand.Parameters.AddWithValue("@p1", MskTarih.Text);
            sqlCommand.Parameters.AddWithValue("@p2", MskSaat.Text);
            sqlCommand.Parameters.AddWithValue("@p3", CmbBrans.Text);
            sqlCommand.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            sqlCommand.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
            Listele();
           
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            //Doktor ComboBox
            SqlCommand kmt3 = new SqlCommand("Select (DoktorAd+' '+DoktorSoyad) from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            kmt3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = kmt3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)",bgl.baglanti());
            sqlCommand.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            sqlCommand.ExecuteNonQuery();
            bgl.baglanti() .Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans brns = new FrmBrans();
            brns.Show();
        }

       
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView3.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView3.Rows[secilen].Cells[0].Value.ToString();
            MskTarih.Text = dataGridView3.Rows[secilen].Cells[1].Value.ToString();
            MskSaat.Text= dataGridView3.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text= dataGridView3.Rows[secilen].Cells[3].Value.ToString();
            CmbDoktor.Text= dataGridView3.Rows[secilen].Cells[4].Value.ToString();
            MskTC.Text = dataGridView3.Rows[secilen].Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Delete From Tbl_Randevular where Randevuid=@p1", bgl.baglanti());
            sqlCommand.Parameters.AddWithValue("@p1", Txtid.Text);
            sqlCommand.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }
    }
}
