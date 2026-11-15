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
    public partial class FrmKasaTakip : Form
    {
        public FrmKasaTakip()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmKasaTakip_Load(object sender, EventArgs e)
        {
            gelirgetir();
            gidergetir();
        }
        public void gelirgetir()
        {
            //gelir gelsin
            var gelir = from x in db.TBLKASA.OrderBy(x => x.Tarih).Where(x=>x.TUR=="Gelir")
                        select new
                        {
                            x.ID,
                            x.ACIKLAMA,
                            x.Tarih,
                            x.TUTAR,
                        };
            dataGridView1.DataSource = gelir.ToList();
            txbTplmGelir.Text = db.TBLKASA.OrderBy(x=>x.Tarih).Sum(y => y.TUTAR).ToString();//Tutarların Toplamı Getirir
        }
        public void gidergetir()
        {
            //gider gelsin
            var gider = from x in db.TBLKASA.Where(x => x.TUR == "Gider")
                        select new
                        {
                            x.ID,
                            x.ACIKLAMA,
                            x.Tarih,
                            x.TUTAR,
                        };
            dataGridView2.DataSource = gider.ToList();
            txbTplmGider.Text = db.TBLKASA.Sum(y => y.TUTAR).ToString();//Tutarların Toplamı Getirir
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gelir kaydet
            TBLKASA t = new TBLKASA();
            t.TUTAR= Convert.ToDecimal(txtGelirTutar.Text);
            t.ACIKLAMA= txtGelirAciklama.Text;
            t.TUR= "Gelir";
            t.Tarih= Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            db.TBLKASA.Add(t);
            db.SaveChanges();
            MessageBox.Show("Gelir Kaydedildi");
            gelirgetir();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //gider kaydet
            TBLKASA t = new TBLKASA();
            t.TUTAR = Convert.ToDecimal(txtGiderTutar.Text);
            t.ACIKLAMA = txtGiderAciklama.Text;
            t.TUR = "Gider";
            t.Tarih = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
            db.TBLKASA.Add(t);
            db.SaveChanges();
            MessageBox.Show("Gider Kaydedildi");
            gidergetir();



        }

        private void txtGelirAra_TextChanged(object sender, EventArgs e)
        {
            //açıklama ara gelir
           
            if (txtGelirAra.Text!="")
            {
                var gelirbul = from x in db.TBLKASA.Where(x => x.TUR == "Gelir" && x.ACIKLAMA.ToLower().Contains(txtGelirAra.Text.ToLower()))
                             select new
                             {
                                 x.ID,
                                 x.ACIKLAMA,
                                 x.Tarih,
                                 x.TUTAR,
                             };
                txbTplmGelir.Text = gelirbul.Sum(y => y.TUTAR).ToString();//Tutarların Toplamı Getirir
                dataGridView1.DataSource = gelirbul.ToList();
            }
            else
            {
                gelirgetir();
            }

        }

        private void txtGiderAra_TextChanged(object sender, EventArgs e)
        {
            if (txtGiderAra.Text != "")
            {
                var giderbul = from x in db.TBLKASA.Where(x => x.TUR == "Gider" && x.ACIKLAMA.ToLower().Contains(txtGelirAra.Text.ToLower()))
                               select new
                               {
                                   x.ID,
                                   x.ACIKLAMA,
                                   x.Tarih,
                                   x.TUTAR,
                               };
                txbTplmGelir.Text = giderbul.Sum(y => y.TUTAR).ToString();//Tutarların Toplamı Getirir
                dataGridView1.DataSource = giderbul.ToList();
            }
            else
            {
                gelirgetir();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime ilktarih=Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
            DateTime sontarih=Convert.ToDateTime(dateTimePicker3.Value.ToShortDateString());
            var gelirbul = from x in db.TBLKASA.Where(x => x.TUR == "Gelir" && x.Tarih >= ilktarih && x.Tarih <= sontarih)
                           select new
                           {
                               x.ID,
                               x.ACIKLAMA,
                               x.Tarih,
                               x.TUTAR,
                           };

            txbTplmGelir.Text = gelirbul.Sum(y => y.TUTAR).ToString();//Tutarların Toplamı Getirir
            dataGridView1.DataSource = gelirbul.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show( "Silmek istediğinizden emin misiniz", "Getir Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var silinecekgelir = db.TBLKASA.Find(int.Parse(lblıd.Text));
                db.TBLKASA.Remove(silinecekgelir);
                db.SaveChanges();
                MessageBox.Show("Geliriniz silindi");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindex = dataGridView1.SelectedCells[0].RowIndex;

            int SecilenID = Convert.ToInt32(dataGridView1.Rows[secilensatirindex].Cells[0].Value.ToString());

            lblıd.Text = SecilenID.ToString();

        }
    }
}
