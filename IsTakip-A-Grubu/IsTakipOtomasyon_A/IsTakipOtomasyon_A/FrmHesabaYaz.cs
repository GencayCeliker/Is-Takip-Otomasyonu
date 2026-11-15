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
    public partial class FrmHesabaYaz : Form
    {
        public FrmHesabaYaz()
        {
            InitializeComponent();
        }
        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        public int gelenID;
        private void FrmHesabaYaz_Load(object sender, EventArgs e)
        {
            label2.Text=gelenID.ToString();
            var kim = db.TBLMUSTERI.Find(gelenID);
            label4.Text = kim.AD + " " + kim.SOYAD + " hesabına yazılıyor";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TBLBORC yeni=new TBLBORC();
            yeni.ACIKLAMA = textBox2.Text;
            yeni.TUTAR=Convert.ToDecimal(textBox1.Text);
            yeni.TARIH=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            yeni.MUSTERI = gelenID;
            db.TBLBORC.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Hesaba Yazıldı");
            ((FrmMusteriTakip)Application.OpenForms["FrmMusteriTakip"]).borcgetir(gelenID);
            this.Close();
        }
    }
}
