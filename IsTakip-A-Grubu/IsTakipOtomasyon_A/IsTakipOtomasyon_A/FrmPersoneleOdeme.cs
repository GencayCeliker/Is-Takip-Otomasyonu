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
    public partial class FrmPersoneleOdeme : Form
    {
        public FrmPersoneleOdeme()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db=new DbIstakip25_AEntities();

        public int GelenPersonelID;
        private void FrmPersoneleOdeme_Load(object sender, EventArgs e)
        {
            lblID.Text=GelenPersonelID.ToString();

            odemegetir(GelenPersonelID);

            dataGridView1.Font = new Font("verdana", 12, FontStyle.Bold);
            dataGridView1.ForeColor= Color.Green;

            //gelen personelin ad soyad yazdıralım
            var personel = db.TBLPERSONEL.Find(GelenPersonelID);
          
            lblad.Text = personel.ADI;
           
            lblsoyad.Text = personel.SOYADI;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void odemegetir(int id)
        {
            var odemeler = from x in db.TBLPERSONELEODEME
                           where x.PERSONEL == id
                           select new
                           {
                               x.ODEMEID,
                               x.TUR,
                               x.TARIH,
                               x.ODENEN
                           };
            dataGridView1.DataSource = odemeler.OrderByDescending(x=>x.TARIH).ToList();

            //tüm ödenenlerin toplamını yaz

            decimal toplamodenen = Convert.ToDecimal(odemeler.Sum(x => x.ODENEN));

            tbtoplam.Text= toplamodenen.ToString() + " ₺";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text== "Ödeme Türü Seçiniz..." || comboBox1.SelectedIndex==-1 || tbodenen.Text=="0")
            {
                MessageBox.Show("Lütfen Ödeme Türü Seçiniz ve Tutar Giriniz");
            }
            else
            {
                TBLPERSONELEODEME yeni = new TBLPERSONELEODEME();

                yeni.ODENEN = Convert.ToDecimal(tbodenen.Text);

                yeni.TARIH = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());

                yeni.TUR = comboBox1.SelectedItem.ToString();

                yeni.PERSONEL = GelenPersonelID;

                db.TBLPERSONELEODEME.Add(yeni);

                db.SaveChanges();

                MessageBox.Show(lblad.Text + " " + lblsoyad.Text + " 'e " + tbodenen.Text + " ₺ " + comboBox1.SelectedItem.ToString() + "  ödemesi yapıldı", "ÖDEME", MessageBoxButtons.OK, MessageBoxIcon.Information);

                odemegetir(GelenPersonelID);

                // BU ÖDEME KAYDI KASAYA GİDER OLARAK AKTARILSIN

                TBLKASA yenigider= new TBLKASA();

                yenigider.TUR = "GİDER";

                yenigider.TUTAR = yeni.ODENEN;

                yenigider.TARIH = yeni.TARIH;

                yenigider.ACIKLAMA = lblad.Text + " " + lblsoyad.Text + " 'e " + tbodenen.Text + " ₺ " + comboBox1.SelectedItem.ToString() + "  ödemesi yapıldı";

                db.TBLKASA.Add(yenigider);

                db.SaveChanges();
            }
        }

        private void tbodenen_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textboxa sadece rakam ve geri tuşu işler
            if (!char.IsDigit(e.KeyChar) && e.KeyChar !=(char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tbodenen_Enter(object sender, EventArgs e)
        {
            tbodenen.SelectAll();
        }

        private void tbodenen_MouseClick(object sender, MouseEventArgs e)
        {
            tbodenen.SelectAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //seçilen kayıt kontrollere gelsin!!

            //seçilen satır indexi
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            //seçilen kaydın ID si
            int secilenodemeID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());


            label2.Text= secilenodemeID.ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult cevap=MessageBox.Show("Aktif Ödeme Kaydını Silmek İstediğinize Emin misiniz?", "ÖDEME SİL",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (cevap==DialogResult.Yes)
            {
                int silinecekID = int.Parse(label2.Text);

                var odeme = db.TBLPERSONELEODEME.Find(silinecekID);

                db.TBLPERSONELEODEME.Remove(odeme);

                db.SaveChanges();

                MessageBox.Show("Kayıt Silindi");

                odemegetir(GelenPersonelID);
            }
        }
    }
}
