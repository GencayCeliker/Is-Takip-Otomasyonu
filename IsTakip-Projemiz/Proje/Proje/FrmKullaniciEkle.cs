using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class FrmKullaniciEkle : Form
    {
        public FrmKullaniciEkle()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            TBLKULLANICI yeni=new TBLKULLANICI();   
            yeni.AdSoyad = textBox1.Text;
            yeni.Sifre = textBox2.Text;
            yeni.Personel = cbPersonel.Checked;
            yeni.Departman = cbDepartman.Checked;
            yeni.CekSenet = cbCekSenet.Checked;
            yeni.Musteri = cbMusteri.Checked;
            yeni.StokTakip = cbStokTakip.Checked;
            yeni.Kasa = cbKasa.Checked;
            yeni.Urunler = cbUrunler.Checked;
            yeni.VeriTabni = cbVeriTabani.Checked;
            yeni.Yetki = "K";
            db.TBLKULLANICI.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Kullanıcı Eklendi");



        }
    }
}
