using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class FrmFirmaUrunAl : Form
    {
        public FrmFirmaUrunAl()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmFirmaUrunAl_Load(object sender, EventArgs e)
        {
            var firmalar = from x in db.TBLFIRMA
                           where x.Durum == true
                           select new
                           {
                               x.FirmaID,
                               x.FirmaAdi,
                               x.Telefon,   
                           };
            dataGridView2.DataSource = firmalar.ToList();
            var urunler = from y in db.TBLURUNLER
                          where y.Durum == true
                          select new
                          {
                              y.Id,
                              y.UrunAdi,
                              y.AlisFiyati,
                              y.Marka,
                              y.StokAdeti

                          };
            dataGridView1.DataSource = urunler.ToList();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView2.SelectedCells[0].RowIndex;
            int secilenmusteriid = Convert.ToInt32(dataGridView2.Rows[secilensatirindeksi].Cells[0].Value.ToString());

            tbFirmaId.Text = secilenmusteriid.ToString();
            tbFirma.Text=db.TBLFIRMA.Find(secilenmusteriid).FirmaAdi;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;
            int secilenurunid = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());

           tburunId.Text= secilenurunid.ToString();
           tbUrun.Text= db.TBLURUNLER.Find(secilenurunid).UrunAdi;
        }

        

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numericAdet_ValueChanged(object sender, EventArgs e)
        {
            int urunid = Convert.ToInt32(tburunId.Text);
            tbTutar.Text = (db.TBLURUNLER.Find(urunid).AlisFiyati * Convert.ToDecimal(numericAdet.Value)).ToString();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ürünler tablosunda stok artıcak
            
            int urunid = Convert.ToInt32(tburunId.Text);
            var urun = db.TBLURUNLER.Find(urunid);
            urun.StokAdeti = urun.StokAdeti + Convert.ToInt16(numericAdet.Value);
            db.SaveChanges();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //firma ekle
            TBLFIRMAHAREKET yf= new TBLFIRMAHAREKET();
            yf.Fırma = Convert.ToInt32(tbFirmaId.Text);
            yf.Urun = Convert.ToInt32(tburunId.Text);
            yf.Adet = Convert.ToInt16(numericAdet.Value);
            yf.Tutar = Convert.ToDecimal(tbTutar.Text);
            yf.Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            yf.Aciklama = tbaciklama.Text;
            db.TBLFIRMAHAREKET.Add(yf);
            db.SaveChanges();
            //çek yada senet değilse kasaya gider olarak gitsin
            if (radioButton1.Checked == true || radioButton2.Checked==true)
            {
                TBLCEKSENET yk = new TBLCEKSENET();
                yk.Firma = Convert.ToInt32(tbFirmaId.Text);
                if (radioButton1.Checked == true)
                {
                    yk.Tur = "Çek";
                }
                if (radioButton2.Checked == true)
                {
                    yk.Tur = "Senet";
                }
                else
                {
                    TBLKASA k = new TBLKASA();
                    k.Tarih = Convert.ToDateTime(DateTime.Now);
                    k.TUTAR = k.TUTAR;
                    k.ACIKLAMA = tbFirma.Text + " Firmasından  - " + numericAdet.Value + " adet " + tbaciklama.Text + " alındı";
                    db.TBLKASA.Add(k);
                    db.SaveChanges();
                }

                yk.Tutar = Convert.ToDecimal(tbTutar.Text);
                yk.Aciklama = tbFirma.Text+" Firmasından  - " + numericAdet.Value+" adet "+tbaciklama.Text+" alındı";
                yk.YazmaTarihi = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
                yk.OdemeTarihi = Convert.ToDateTime(dateTimePicker3.Value.ToShortDateString());
                yk.Durum = false;
                db.TBLCEKSENET.Add(yk);
                db.SaveChanges();
                
            }
        }
    }
}
