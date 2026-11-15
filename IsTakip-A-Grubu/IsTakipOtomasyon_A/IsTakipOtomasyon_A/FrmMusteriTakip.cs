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
    public partial class FrmMusteriTakip : Form
    {
        public FrmMusteriTakip()
        {
            InitializeComponent();
        }
        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        int AktifID = 0;
        private void FrmMusteriTakip_Load(object sender, EventArgs e)
        {
            //1-müşterileri getir
            musterigetir();

            //2- aktif olan müşterinin ödeme kayıtlarını getir
            //eğer tabloda müşteri kaydı varsa ilk kaydın ödeme kayıtlarını getirsin
            if (db.TBLMUSTERI.Where(x => x.DURUM == true).Count() > 0)
            {
                int ilkMusteriID = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());

                lblID.Text = ilkMusteriID.ToString();
                AktifID = ilkMusteriID;
                odemegetir(AktifID);
                borcgetir(AktifID);

                tbkalan.Text = (Convert.ToDecimal(tbtoplamborc.Text) - Convert.ToDecimal(tbtoplamodenen.Text)).ToString();
            }

        }

        public void musterigetir()
        {
            var musteriler = from x in db.TBLMUSTERI
                             where x.DURUM == true
                             select new
                             {
                                 x.ID,
                                 x.AD,
                                 x.SOYAD
                             };
            dataGridView1.DataSource = musteriler.ToList();
        }

        public void odemegetir(int id)
        {
            var odemeler = from x in db.TBLODEME
                           where x.MUSTERI == id
                           select new
                           {
                               x.ID,
                               x.ACIKLAMA,
                               x.TARIH,
                               x.TUTAR
                           };
            dataGridView2.DataSource = odemeler.OrderByDescending(x => x.TARIH).ToList();

            tbtoplamodenen.Text = odemeler.Sum(x => x.TUTAR).ToString();
        }

        public void borcgetir(int id)
        {
            var borclar = from x in db.TBLBORC
                          where x.MUSTERI == id
                          select new
                          {
                              x.ID,
                              x.ACIKLAMA,
                              x.TARIH,
                              x.TUTAR
                          };
            dataGridView3.DataSource = borclar.OrderByDescending(x => x.TARIH).ToList();

            tbtoplamborc.Text = borclar.Sum(x => x.TUTAR).ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            //seçilen kaydın ID si
            int secilenmusteriID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            lblID.Text = secilenmusteriID.ToString();

            var bukim = db.TBLMUSTERI.Find(secilenmusteriID);
            lbladres.Text = bukim.TEL;
            lblislem.Text = bukim.ISLEMSAYISI.ToString();
            odemegetir(secilenmusteriID);
            borcgetir(secilenmusteriID);

            //KALAN BORÇ
            tbkalan.Text = (Convert.ToDecimal(tbtoplamborc.Text) - Convert.ToDecimal(tbtoplamodenen.Text)).ToString();

            label6.Text = secilenmusteriID.ToString();

        }
        private void button6_Click(object sender, EventArgs e)
        {
            FrmHesabaYaz hy = new FrmHesabaYaz();
            hy.StartPosition = FormStartPosition.CenterScreen;
            hy.gelenID = int.Parse(label6.Text);
            hy.ShowDialog();
        }

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "VERESİYE SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                int secilensatirindex = dataGridView3.SelectedCells[0].RowIndex;

                int secilenkayitID = Convert.ToInt32(dataGridView3.Rows[secilensatirindex].Cells[0].Value.ToString());

                var silinecekkayit = db.TBLBORC.Find(secilenkayitID);

                db.TBLBORC.Remove(silinecekkayit);

                db.SaveChanges();

                MessageBox.Show("Veresiye Kaydı Silindi");

                borcgetir(int.Parse(label6.Text));

            }
        }
    }
}
