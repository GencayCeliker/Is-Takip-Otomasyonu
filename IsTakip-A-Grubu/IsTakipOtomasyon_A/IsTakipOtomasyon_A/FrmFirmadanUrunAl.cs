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
    public partial class FrmFirmadanUrunAl : Form
    {
        public FrmFirmadanUrunAl()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void FrmFirmadanUrunAl_Load(object sender, EventArgs e)
        {
            listele();
        }

        public void listele()
        {
            var firmalar = from x in db.TBLFIRMA
                           where x.DURUM == true
                           select new
                           {
                               x.ID,
                               x.FIRMA,
                               x.TEL
                           };
            dataGridView1.DataSource = firmalar.ToList();

            var urunler = from x in db.TBLURUNLER
                          where x.DURUM == true
                          select new
                          {
                              x.URUNID,
                              x.URUNADI,
                              x.STOKADET,
                              x.MARKA
                          };
            dataGridView2.DataSource = urunler.ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            int secilenID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            tbfirmaadi.Text = db.TBLFIRMA.Find(secilenID).FIRMA;
            tbfirmaID.Text = secilenID.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView2.SelectedCells[0].RowIndex;

            int secilenID = Convert.ToInt32(dataGridView2.Rows[secilenindex].Cells[0].Value.ToString());

            tburunadi.Text = db.TBLURUNLER.Find(secilenID).URUNADI;
            tburunID.Text = secilenID.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int urunID = int.Parse(tburunID.Text);

            decimal alisfiyati = Convert.ToDecimal(db.TBLURUNLER.Find(urunID).ALISFIYATI);

            tbtutar.Text = (alisfiyati * numericUpDown1.Value).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tburunadi.Text == "" || tbfirmaadi.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Ürün, firma ve adet girmelisiniz!!!");
            }
            else
            {
                //1- firmahareketlerine kaydet

                TBLFIRMAHAREKETLERI yeni = new TBLFIRMAHAREKETLERI();
                yeni.URUN = int.Parse(tburunID.Text);
                yeni.ACIKLAMA = tbaciklama.Text;
                yeni.TUTAR = Convert.ToDecimal(tbtutar.Text);
                yeni.TARIH = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                yeni.ADET = int.Parse(numericUpDown1.Value.ToString());
                yeni.FIRMA = int.Parse(tbfirmaID.Text);
                db.TBLFIRMAHAREKETLERI.Add(yeni);

                // 2- urunler tablosunda stok miktarı artmalı

                var urunbul = db.TBLURUNLER.Find(int.Parse(tburunID.Text));

                urunbul.STOKADET = urunbul.STOKADET + int.Parse(numericUpDown1.Value.ToString());

                // 3 -çek senet işaretli ise ÇekSenet tablosuna kaydedilmeli

                if (radioButton1.Checked || radioButton2.Checked)
                {
                    TBLCEKSENET yk = new TBLCEKSENET();
                    yk.FIRMA = yeni.FIRMA;

                    if (radioButton2.Checked) yk.TUR = "ÇEK";
                    if (radioButton3.Checked) yk.TUR = "SENET";

                    yk.YAZMATARIHI = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());

                    yk.ODEMETARIHI = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());

                    yk.DURUMU = false;

                    yk.TUTAR = yeni.TUTAR;

                    yk.ACIKLAMA = tbfirmaadi.Text + "  firmasına " + yk.TUR + " yazıldı";

                    db.TBLCEKSENET.Add(yk);

                }

                else
                {
                    // o zaman bu kasaya gider kaydedilmeli
                    TBLKASA yg = new TBLKASA();
                    yg.TUR = "GİDER";
                    yg.TARIH = yeni.TARIH;
                    yg.TUTAR = yeni.TUTAR;
                    yg.ACIKLAMA = tbfirmaadi.Text + " firmasından " + yeni.ADET + " adet " + tburunadi.Text + " alındı";
                    db.TBLKASA.Add(yg);
                }
            }
            db.SaveChanges();
            MessageBox.Show("Alınan Ürün Kaydedildi");
            listele();
            tburunadi.Text = "";
            tburunID.Text = "";
            tbfirmaadi.Clear();
            tbfirmaID.Clear();           
            tbtutar.Clear();
            tbaciklama.Clear();
            groupBox4.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }
    }
}
