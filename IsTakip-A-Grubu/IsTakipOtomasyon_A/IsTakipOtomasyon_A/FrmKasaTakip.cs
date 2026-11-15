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
    public partial class FrmKasaTakip : Form
    {
        public FrmKasaTakip()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void FrmKasaTakip_Load(object sender, EventArgs e)
        {
            kasagetir();
        }

        public void kasagetir()
        {
            //gelir ve giderleri ayrı ayrı çağıralım

            var gelirler = from x in db.TBLKASA
                           where x.TUR == "GELİR"
                           select new
                           {
                               x.ID,
                               x.TARIH,
                               x.ACIKLAMA,
                               x.TUTAR
                           };
            dataGridView1.DataSource = gelirler.OrderByDescending(x => x.ID).ToList();

            tbgelirtoplam.Text = gelirler.Sum(x => x.TUTAR).ToString() + "₺";

            //şimdi gider çağıralım

            var giderler = from x in db.TBLKASA
                           where x.TUR == "GİDER"
                           select new
                           {
                               x.ID,
                               x.TARIH,
                               x.ACIKLAMA,
                               x.TUTAR
                           };
            dataGridView2.DataSource = giderler.OrderByDescending(x => x.ID).ToList();
            tbgidertoplam.Text = giderler.Sum(x => x.TUTAR).ToString() + "₺";
            dataGridView1.Columns[0].Width = 50;
            dataGridView2.Columns[0].Width = 50;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //gelir kaydet
            TBLKASA yeni = new TBLKASA();
            yeni.TUR = "GELİR";
            yeni.TUTAR = Convert.ToDecimal(textBox1.Text);
            yeni.TARIH = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            yeni.ACIKLAMA = tbaciklamagelir.Text; ;
            db.TBLKASA.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Yeni GELİR Kaydı Eklendi");
            kasagetir();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //gider kaydet
            TBLKASA yeni = new TBLKASA();
            yeni.TUR = "GİDER";
            yeni.TUTAR = Convert.ToDecimal(textBox5.Text);
            yeni.TARIH = Convert.ToDateTime(dateTimePicker6.Value.ToShortDateString());
            yeni.ACIKLAMA = tbaciklamagider.Text; ;
            db.TBLKASA.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Yeni GİDER Kaydı Eklendi");
            kasagetir();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var degerler = db.TBLKASA.Where(x => x.TUR == "GELİR" && x.ACIKLAMA.ToLower().Contains(textBox2.Text.ToLower()));

                dataGridView1.DataSource = degerler.ToList();
            }
            else
            {
                kasagetir();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                var degerler = db.TBLKASA.Where(x => x.TUR == "GİDER" && x.ACIKLAMA.ToLower().Contains(textBox3.Text.ToLower()));

                dataGridView2.DataSource = degerler.ToList();
            }
            else
            {
                kasagetir();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime ilktarih = dateTimePicker2.Value.AddDays(-1);
            //tarihe göre arama
            var degerler = from x in db.TBLKASA
                           where x.TARIH >= ilktarih && x.TARIH <= dateTimePicker3.Value && x.TUR == "GELİR"
                           select new
                           {
                               x.ID,
                               x.TARIH,
                               x.ACIKLAMA,
                               x.TUTAR
                           };
            dataGridView1.DataSource = degerler.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime ilktarih = dateTimePicker4.Value.AddDays(-1);
            //tarihe göre arama
            var degerler = from x in db.TBLKASA
                           where x.TARIH >= ilktarih && x.TARIH <= dateTimePicker5.Value && x.TUR == "GİDER"
                           select new
                           {
                               x.ID,
                               x.TARIH,
                               x.ACIKLAMA,
                               x.TUTAR
                           };
            dataGridView2.DataSource = degerler.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(" Aktif GELİR Kaydını silmek istediğinize emin misiniz?", "GELİR SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                var silinecekkayit = db.TBLKASA.Find(int.Parse(label13.Text));
                db.TBLKASA.Remove(silinecekkayit);
                db.SaveChanges();
                MessageBox.Show("GELİR Kaydı Silindi");
                kasagetir();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            //seçilen kaydın ID si
            int secilenkayitID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            label13.Text = secilenkayitID.ToString();
        }
    }
}
