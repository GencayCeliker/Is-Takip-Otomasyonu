using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class FrmPersonelOdeme : Form
    {
        public FrmPersonelOdeme()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db=new DBIstakikEntities1();
        private void button1_Click(object sender, EventArgs e)
        {

        }
        public int gelenid;
        private void FrmPersonelOdeme_Load(object sender, EventArgs e)
        {
            label8.Text = gelenid.ToString();
            var bukim = db.TBLPersonel.Find(gelenid);
            label6.Text = bukim.ADI;
            label2.Text = bukim.SOYADI;
            //açılan personele ait ödeme kayıtlarını listelesin.
            odemeGetir();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==-1||txtodenen.Text=="0")
            {
                MessageBox.Show("Ödeme Türü Seçmelisiniz Veya Tutar Belirtmelisiniz");
            }
            else
            {
                TBLPersonelOdeme yeniodeme = new TBLPersonelOdeme();
                yeniodeme.TARIH = DateTime.Parse(dateTimePicker1.Text);
                yeniodeme.TUR = comboBox1.Text;
                yeniodeme.ACIKLAMA = textBox1.Text;
                yeniodeme.TUTAR = Convert.ToDecimal(txtodenen.Text);
                yeniodeme.PERSONELID = gelenid;
                db.TBLPersonelOdeme.Add(yeniodeme);
                db.SaveChanges();
                MessageBox.Show("Personele Ödeme Yapıldı");
            }
            odemeGetir();

        }

        private void txtodenen_KeyPress(object sender, KeyPressEventArgs e)
        {
            //buraya sadece rakam  girsin
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtodenen_Enter(object sender, EventArgs e)
        {
            txtodenen.SelectAll();
        }

        private void txtodenen_MouseClick(object sender, MouseEventArgs e)
        {
            txtodenen.SelectAll();
        }
        public void odemeGetir() 
        {
            var odemeler = from x in db.TBLPersonelOdeme
                         where x.PERSONELID == gelenid
                         select new
                         {
                             x.PERSONELID,
                             x.TUR,
                             x.ACIKLAMA,
                             x.TARIH,
                             x.TUTAR,
                             
                         };
            dataGridView1.DataSource = odemeler.OrderByDescending(x=>x.TARIH).ToList();
            
            textBox2.Text=odemeler.Sum(x=>x.TUTAR).ToString()+"₺";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;
            int secilenid = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());
            label8.Text=secilenid.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(" Aktif ödemeyi silmek istediğinizden emin misiniz", "Ödeme Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var silinecekodeme = db.TBLPersonelOdeme.Find(int.Parse(label8.Text));
                db.TBLPersonelOdeme.Remove(silinecekodeme);
                db.SaveChanges();
                MessageBox.Show("Ödeme silindi");
                odemeGetir();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
