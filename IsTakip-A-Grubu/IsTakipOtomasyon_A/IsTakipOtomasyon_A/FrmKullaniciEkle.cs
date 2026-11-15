using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTakipOtomasyon_A
{
    public partial class FrmKullaniciEkle : Form
    {
        public FrmKullaniciEkle()
        {
            InitializeComponent();
        }
        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        private void button3_Click(object sender, EventArgs e)
        {
            TBLKULLANICI yeni=new TBLKULLANICI();
            yeni.ADISOYADI = tbadsoyad.Text;
            yeni.KULLANICIADI=tbkuladi.Text;
            yeni.SIFRE = tbsifre.Text;
            yeni.MAIL=tbmail.Text;
            yeni.PERSONEL = cbpersonel.Checked;
            yeni.DEPARTMAN = cbdep.Checked;
            yeni.CEKSENET = cbceksenet.Checked;
            yeni.VERITABANI = cbvt.Checked;
            yeni.STOKTAKIP = cbstokı.Checked;
            yeni.URUNLER = cburun.Checked;
            yeni.KASA = cbkasa.Checked;
            yeni.MUSTERI = cbmust.Checked;
            yeni.DURUM = true;
            yeni.YETKI = "K";

            db.TBLKULLANICI.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Yeni kullanıcı eklendi");
        }
    }
}
