using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IsTakipOtomasyon_A
{
    public partial class FrmCekSenetTakip : Form
    {
        public FrmCekSenetTakip()
        {
            InitializeComponent();
        }
        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void FrmCekSenetTakip_Load(object sender, EventArgs e)
        {
            if (db.TBLCEKSENET.Count() > 0)
            {
                label6.Text = db.TBLCEKSENET.OrderByDescending(x => x.ID).FirstOrDefault().ID.ToString();
                label3.Text = db.TBLCEKSENET.OrderByDescending(x => x.ID).FirstOrDefault().TBLFIRMA.FIRMA;

            }
            ceksenetgoster();
        }

        public void ceksenetgoster()
        {
            var degerler = from x in db.TBLCEKSENET
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        int AktiffirmaID;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            int secilenceksenetID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            var bul = db.TBLCEKSENET.Find(secilenceksenetID);

            label6.Text = bul.ID.ToString();

            label3.Text = bul.TBLFIRMA.FIRMA;

            AktiffirmaID = bul.ID;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //aktif firma ödeneneler
            string tur = "";
            if (radioButton1.Checked) tur = "ÇEK";
            if (radioButton2.Checked) tur = "SENET";

            var degerler = from x in db.TBLCEKSENET
                           where x.FIRMA == AktiffirmaID && x.DURUMU == true
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //aktif firma ödenecekler
            string tur = "";
            if (radioButton1.Checked) tur = "ÇEK";
            if (radioButton2.Checked) tur = "SENET";

            var degerler = from x in db.TBLCEKSENET
                           where x.FIRMA == AktiffirmaID && x.DURUMU == false
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //bugün ödenecekler
            DateTime bugun = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            var degerler = from x in db.TBLCEKSENET
                           where DbFunctions.TruncateTime(x.ODEMETARIHI) == bugun && x.DURUMU == false
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();
            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //yarın ödenecekler
            DateTime yarin = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(1);
            var degerler = from x in db.TBLCEKSENET
                           where DbFunctions.TruncateTime(x.ODEMETARIHI) == yarin && x.DURUMU == false
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();
            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string tur = "";
            if (radioButton1.Checked) tur = "ÇEK";
            if (radioButton2.Checked) tur = "SENET";
            var degerler = from x in db.TBLCEKSENET
                           where x.DURUMU == true && x.TUR == tur
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();
            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tur = "";
            if (radioButton1.Checked) tur = "ÇEK";
            if (radioButton2.Checked) tur = "SENET";

            var degerler = from x in db.TBLCEKSENET
                           where x.DURUMU == false && x.TUR == tur
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ceksenetgoster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Ödendi yap
            DialogResult cevap = MessageBox.Show(label3.Text + " firmasına aik ÇEK/SENET kaydınız ÖDENDİ olarak Güncellensin mi?","ÖDEME YAP",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var bul = db.TBLCEKSENET.Find(int.Parse(label6.Text));
                bul.DURUMU = true;
                MessageBox.Show("Kaydınız Ödendi");
              
                // bu ödeme kasaya GİDER olarak kaydedilmeli

                TBLKASA yk = new TBLKASA();
                yk.TUR = "GİDER";
                yk.TARIH = Convert.ToDateTime(DateTime.Now.ToShortDateString()); ;
                yk.TUTAR = bul.TUTAR;
                yk.ACIKLAMA = label3.Text + "  firmasına " + bul.TUR + " ödemesi yapıldı";
                db.TBLKASA.Add(yk);
                db.SaveChanges();
                ceksenetgoster();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var bul = db.TBLCEKSENET.Find(int.Parse(label6.Text));
            bul.DURUMU = false;
            db.SaveChanges();
            MessageBox.Show("ÖDENMEDİ olarak Güncellendi,Kasa kaydınızı silmeyi unutmayınız!!!");
            ceksenetgoster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tur = "";
            if (radioButton1.Checked) tur = "ÇEK";
            if (radioButton2.Checked) tur = "SENET";

            DateTime ilktarih = dateTimePicker2.Value.AddDays(-1);
            //tarihe göre arama
            var degerler = from x in db.TBLCEKSENET
                           where x.ODEMETARIHI >= ilktarih && x.ODEMETARIHI <= dateTimePicker3.Value && x.TUR==tur
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.YAZMATARIHI,
                               x.ODEMETARIHI,
                               x.TUR,
                               x.TUTAR,
                               Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

            tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var degerler = from x in db.TBLCEKSENET
                            

                               select new
                               {
                                   x.ID,
                                   x.TBLFIRMA.FIRMA,
                                   x.YAZMATARIHI,
                                   x.ODEMETARIHI,
                                   x.TUR,
                                   x.TUTAR,
                                   Durum = x.DURUMU == false ? "ÖDENMEDİ" : "ÖDENDİ"
                               };
                dataGridView1.DataSource = degerler.OrderByDescending(x => x.ID).ToList();

                tbtoplam.Text = degerler.Sum(x => x.TUTAR).ToString() + "  ₺";
            }
            else
            {
                ceksenetgoster();
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var satir = dataGridView1.Rows[e.RowIndex];
            var durum = satir.Cells["Durum"].Value?.ToString();

            if (durum=="ÖDENDİ")
            {
                satir.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else
            {
                satir.DefaultCellStyle.BackColor = Color.LightPink;
            }
        }
    }
}
