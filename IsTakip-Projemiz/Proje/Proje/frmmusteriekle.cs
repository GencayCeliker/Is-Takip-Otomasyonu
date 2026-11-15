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
    public partial class frmmusteriekle : Form
    {
        public frmmusteriekle()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.SelectAll();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            var yenikayit = new TBLMUSTERI();
            yenikayit.MusteriAd = textBox1.Text;
            yenikayit.MusteriSoyadı = textBox2.Text;
            yenikayit.Telefon = textBox3.Text;
            yenikayit.IlkIslemtarihi=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            yenikayit.Durum = true;
            db.TBLMUSTERI.Add(yenikayit);
            db.SaveChanges();
            MessageBox.Show("Yeni müşteri kaydedildi.");
           
            ((FrmMusteriTakip)Application.OpenForms["FrmMusteriTakip"]).musterigetir();
            this.Close();
        }
    }
}
