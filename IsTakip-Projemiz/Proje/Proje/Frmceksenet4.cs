using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class Frmceksenet4 : Form
    {
        public Frmceksenet4()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db= new DBIstakikEntities1();
        public void ceksenetlistele()
        {
            var degerler = from x in db.TBLCEKSENET
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }
        int aktifirmaid;
        private void Frmceksenet4_Load(object sender, EventArgs e)
        {
            
            if (db.TBLCEKSENET.Count() > 0)
            {
                ceksenetlistele();
                lblıd.Text = db.TBLCEKSENET.OrderByDescending(x => x.Id).FirstOrDefault().Id.ToString();
                label2.Text = db.TBLCEKSENET.OrderByDescending(x => x.Id).FirstOrDefault().TBLFIRMA.FirmaAdi;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;
            int secilenmusteriid = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());
            var bul= db.TBLCEKSENET.Find(secilenmusteriid);
            lblıd.Text = bul.Id.ToString();
            label2.Text = bul.TBLFIRMA.FirmaAdi;
            aktifirmaid = Convert.ToInt32(bul.Firma);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //aktif firma ödenenler
            string tur = "";
            if (radioButton1.Checked==true)
            {
                tur = "Çek";
            }
            if (radioButton2.Checked==true)
            {
                tur = "Senet";
            }
            var degerler = from x in db.TBLCEKSENET
                           where x.Firma == aktifirmaid && x.Durum == true
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //aktif firma ödenecekler
            string tur = "";
            if (radioButton1.Checked == true)
            {
                tur = "Çek";
            }
            if (radioButton2.Checked == true)
            {
                tur = "Senet";
            }
            var degerler = from x in db.TBLCEKSENET
                           where x.Firma == aktifirmaid && x.Durum == false
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Today;
            var degerler = from x in db.TBLCEKSENET
                           where EntityFunctions.TruncateTime(x.OdemeTarihi) == bugun
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime yarin = Convert.ToDateTime(DateTime.Now.AddDays(1));
            var degerler = from x in db.TBLCEKSENET
                           where EntityFunctions.TruncateTime(x.OdemeTarihi) == yarin
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string tur = "";
            if (radioButton1.Checked == true)
            {
                tur = "Çek";
            }
            if (radioButton2.Checked == true)
            {
                tur = "Senet";
            }
            var degerler = from x in db.TBLCEKSENET
                           where x.Firma == aktifirmaid && x.Durum == true&& x.Tur== tur
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tur = "";
            if (radioButton1.Checked == true)
            {
                tur = "Çek";
            }
            if (radioButton2.Checked == true)
            {
                tur = "Senet";
            }
            var degerler = from x in db.TBLCEKSENET
                           where x.Firma == aktifirmaid && x.Durum == false && x.Tur == tur
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ceksenetlistele();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //çeksnet öde
            var bul = db.TBLCEKSENET.Find(Convert.ToInt32(lblıd.Text));
            bul.Durum = true;

            // ÖNEMLİ: KASAYA GİDER KAYDI EKLEME//
            //bu ödeme kasaya gider olarak gitsin
            TBLKASA gider = new TBLKASA();
            gider.Tarih = Convert.ToDateTime(DateTime.Now);
            gider.TUTAR = bul.Tutar;
            gider.ACIKLAMA = label2.Text + " Firmasına " + bul.Tur + " ödendi.";
            db.TBLKASA.Add(gider);
            db.SaveChanges();
            MessageBox.Show("Seçilen çek/senet kaydınız ödendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ceksenetlistele();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int aktifceksenetid = int.Parse(lblıd.Text);
            var bul = db.TBLCEKSENET.Find(aktifceksenetid);
            bul.Durum = false;
            db.SaveChanges();
            MessageBox.Show("Seçilen çek/senet kaydınız ödenmedi olarak değiştirildi. Kasa Bilgisini Güncelleyin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ceksenetlistele();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime ilktarih = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
            DateTime sontarih = Convert.ToDateTime(dateTimePicker3.Value.ToShortDateString());
            var degerler = from x in db.TBLCEKSENET
                           where x.OdemeTarihi >= ilktarih && x.OdemeTarihi <= sontarih
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }
        public void sonucgunlistele()
        {
            DateTime bugun = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime sontarih = bugun.AddDays(3);
            var degerler = from x in db.TBLCEKSENET
                           where x.OdemeTarihi >= bugun && x.OdemeTarihi <= sontarih && x.Durum == false
                           select new
                           {
                               x.Id,
                               x.TBLFIRMA.FirmaAdi,
                               x.YazmaTarihi,
                               x.OdemeTarihi,
                               x.Tur,
                               x.Tutar,
                               Durum = x.Durum == true ? "Ödendi" : "Ödenmedi",
                               x.Aciklama
                           };
            dataGridView1.DataSource = degerler.ToList();
            txbTplm.Text = degerler.OrderByDescending(x => x.Id).Sum(x => x.Tutar).ToString() + " ₺";
        }
    }
    
}
