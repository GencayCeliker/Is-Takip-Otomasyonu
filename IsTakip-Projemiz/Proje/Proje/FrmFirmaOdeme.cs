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
    public partial class FrmFirmaOdeme : Form
    {
        public FrmFirmaOdeme()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmFirmaOdeme_Load(object sender, EventArgs e)
        {
            var firmalar=from x in db.TBLFIRMA
                           select new
                           {
                               x.FirmaID,
                               x.FirmaAdi
                           };
            dataGridView1.DataSource = firmalar.ToList();
        }
        int secilenid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenindex = dataGridView1.SelectedCells[0].RowIndex;
            secilenid = int.Parse(dataGridView1.Rows[secilenindex].Cells["FirmaID"].Value.ToString());
            var bul = db.TBLFIRMA.Find(secilenid);
            label2.Text = bul.FirmaAdi.ToUpper()+" Firmasına Ödeme Yapılacak !";
        }

        private void button1_Click(object sender, EventArgs e)
        {//1 firma ödeme kaydı oluşturma
            TBLFIRMAODEME yn=new TBLFIRMAODEME();
            yn.Firma = secilenid;
            yn.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            yn.Tutar = decimal.Parse(textBox1.Text);
            yn.Aciklama = textBox2.Text;
            db.TBLFIRMAODEME.Add(yn);
            MessageBox.Show("Firma Ödeme Yapıldı");
            //2 kasaya gider kaydı oluşturma
            TBLKASA gider=new TBLKASA();
            gider.ACIKLAMA=db.TBLFIRMA.Find(secilenid).FirmaAdi+" Firmasına Ödeme";
            gider.Tarih = yn.Tarih;
            gider.TUTAR = decimal.Parse(textBox1.Text);
            gider.TUR = "Gider";
            db.TBLKASA.Add(gider);
            db.SaveChanges();
            ((FrmStokTakip)Application.OpenForms["FrmStokTakip"]).verigetir();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
