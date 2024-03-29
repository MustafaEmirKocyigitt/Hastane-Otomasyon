﻿using System;
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
    public partial class FrmBilgiDüzenle : Form
    {
        public FrmBilgiDüzenle()
        {
            InitializeComponent();
        }
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public string TCno;
        private void FrmBilgiDüzenle_Load(object sender, EventArgs e)
        {
            MskTc.Text = TCno;
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Hastalar where HastaTc=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", MskTc.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();

        }

        private void BtnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("update Tbl_Hastalar  set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6", bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd2.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            cmd2.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            cmd2.Parameters.AddWithValue("@p6", MskTc.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
