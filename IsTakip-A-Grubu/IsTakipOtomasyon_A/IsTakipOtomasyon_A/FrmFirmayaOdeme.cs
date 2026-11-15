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
    public partial class FrmFirmayaOdeme : Form
    {
        public FrmFirmayaOdeme()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        private void FrmFirmayaOdeme_Load(object sender, EventArgs e)
        {
            var firmalar = from x in db.TBLFIRMA
                           where x.DURUM == true
                           select new
                           {
                               x.ID,
                               x.FIRMA,
                               x.TEL
                           };
            dataGridView1.DataSource= firmalar.OrderBy(x=>x.FIRMA).ToList();
        }

        int secilenID=0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

           secilenID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            var bul = db.TBLFIRMA.Find(secilenID);

            label2.Text = bul.FIRMA.ToUpper() + " Firmasına Ödeme Yapılacak ";

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (secilenID!=0)
            {
                //o zaman firmayaodeme tablosuna kaydet
                TBLFIRMAYAODEME fo = new TBLFIRMAYAODEME();
                fo.FIRMA = secilenID;
                fo.TUTAR = Convert.ToDecimal(textBox1.Text);
                fo.TARIH=Convert.ToDateTime(DateTime.Now.ToShortDateString());
                fo.ACIKLAMA = textBox2.Text;
                db.TBLFIRMAYAODEME.Add( fo );

                // bu ödeme kasaya gider olarak kaydedilmeli

                TBLKASA yg = new TBLKASA();
                yg.TUR = "GİDER";
                yg.TARIH = fo.TARIH;
                yg.TUTAR = fo.TUTAR;
                yg.ACIKLAMA = db.TBLFIRMA.Find(secilenID).FIRMA + " firmasına ödeme yapıldı ";
                db.TBLKASA.Add(yg);

                db.SaveChanges();
                MessageBox.Show("Ödeme Kaydınız Yapıldı");
                ((FrmStokTakip)Application.OpenForms["FrmStokTakip"]).verigetir();
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir firma seçiniz");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
