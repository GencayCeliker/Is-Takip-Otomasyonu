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
    public partial class FrmMusteriBorc : Form
    {
        public FrmMusteriBorc()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }
        DBIstakikEntities1 dB = new DBIstakikEntities1();
        public int GelenMusteriId;
        private void button1_Click(object sender, EventArgs e)
        {
            TBLMUSTERIBORC yeni = new TBLMUSTERIBORC();
            yeni.Aciklama = textBox2.Text;
            yeni.Tutar =Convert.ToDecimal( textBox1.Text);
            yeni.Tarih=Convert.ToDateTime(DateTime.Now.ToShortDateString() );
            yeni.Musteri = GelenMusteriId;
            dB.TBLMUSTERIBORC.Add(yeni);
            MessageBox.Show("Hesaba Yazıldı");
            ((FrmMusteriTakip)Application.OpenForms["FrmMusteriTakip"]).odemeborcGetir(GelenMusteriId);
            //işlem saysısını 1 artır
            var musteri = dB.TBLMUSTERI.Find(GelenMusteriId);
            musteri.IslemSayisi += 1;

            dB.SaveChanges();

            this.Close();


        }

        private void FrmMusteriBorc_Load(object sender, EventArgs e)
        {
            label2.Text = GelenMusteriId.ToString();
        }
    }
}
