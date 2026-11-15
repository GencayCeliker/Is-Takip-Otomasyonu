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
    public partial class FrmMusteriTakip : Form
    {
        public FrmMusteriTakip()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmMusteriTakip_Load(object sender, EventArgs e)
        {
            musterigetir();
            //eğer kayıtlı bir müşteri varsa ilk kayıt aktif olup onun borç ve ödeme kayıtları gelsin
            if (dataGridView1.Rows.Count > 0)
            {
                int ilkmusteriid = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
                var bukim = db.TBLMUSTERI.Find(ilkmusteriid);
                lblTlfn.Text = bukim.Telefon;
                label4.Text = bukim.IslemSayisi.ToString();
                label5.Text = Convert.ToDateTime(bukim.IlkIslemtarihi).ToShortDateString();
                odemeborcGetir(ilkmusteriid);
            }

        }
        public void musterigetir()
        {
            var musteriler = from x in db.TBLMUSTERI
                             where x.Durum == true
                             select new
                             {
                                 x.MusterıId,
                                 x.MusteriAd,
                                 x.MusteriSoyadı
                             };
            dataGridView1.DataSource = musteriler.OrderByDescending(x => x.MusterıId).ToList();

        }
        public void odemeborcGetir(int id)
        {//1 ödeme kayıtları gelsin 
            var musteri = from x in db.TBLMUSTERIODEME
                          where x.Musteri == id
                          select new
                          {
                              x.OdemeId,
                              x.Aciklama,
                              x.Tarih,
                              x.Tutar
                          };
            dataGridView2.DataSource = musteri.OrderByDescending(x => x.OdemeId).ToList();
            textBox3.Text = musteri.Sum(y => y.Tutar).ToString() + "₺";
            decimal topodeme = Convert.ToDecimal(musteri.Sum(y => y.Tutar));

            //2 borç kayıtları gelsin
            var borclar = from x in db.TBLMUSTERIBORC
                          where x.Musteri == id
                          select new
                          {
                              x.BorcId,
                              x.Aciklama,
                              x.Tarih,
                              x.Tutar
                          };
            dataGridView2.DataSource = borclar.OrderByDescending(x => x.BorcId).ToList();
            textBox2.Text = borclar.Sum(y => y.Tutar).ToString() + "₺";
            decimal topborc = Convert.ToDecimal(borclar.Sum(y => y.Tutar));
            textBox3.Text = (topborc - topodeme).ToString() + "₺";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;
            int secilenmusteriid = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());
            odemeborcGetir(secilenmusteriid);
            label2.Text = secilenmusteriid.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTlfn_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmmusteriekle gel = new frmmusteriekle();
            gel.ShowDialog();
            StartPosition = FormStartPosition.CenterScreen;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmMusteriBorc gel = new FrmMusteriBorc();
            gel.GelenMusteriId = int.Parse(label2.Text);
            gel.ShowDialog();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            musterigetir();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            DialogResult cevap = MessageBox.Show(" 'i silmek istediğinizden emin misiniz", "Ödemeyi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var silineceksatirindeks = dataGridView2.SelectedCells[0].RowIndex;

                int silinecekodemeıd = Convert.ToInt32(dataGridView2.Rows[silineceksatirindeks].Cells[0].Value.ToString());

                var silinecekodeme = db.TBLMUSTERIODEME.Find(silinecekodemeıd);

                db.TBLMUSTERIODEME.Remove(silinecekodeme);

                db.SaveChanges();

                MessageBox.Show("Ödeme kaydı silindi");

                odemeborcGetir(int.Parse(label2.Text));

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
        //Sağ Buton Silme İşlemi - Borç Kayıtları
        private void borcSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(" 'i silmek istediğinizden emin misiniz", "Borcu Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var silineceksatirindeks = dataGridView3.SelectedCells[0].RowIndex;

                int silinecekodemeıd = Convert.ToInt32(dataGridView3.Rows[silineceksatirindeks].Cells[0].Value.ToString());

                var silinecekodeme = db.TBLMUSTERIBORC.Find(silinecekodemeıd);

                db.TBLMUSTERIBORC.Remove(silinecekodeme);

                db.SaveChanges();

                MessageBox.Show("Borç kaydı silindi");

                odemeborcGetir(int.Parse(label2.Text));

            }
        }
    }
}
