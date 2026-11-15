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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int gelenkullaniciID;
        public void button1_Click(object sender, EventArgs e)
        {
            
            FrmPersonelTakip pf= new FrmPersonelTakip();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmKasaTakip pf = new FrmKasaTakip();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmMusteriTakip pf = new FrmMusteriTakip();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmCekSenetTakip pf = new FrmCekSenetTakip();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmFirmadanUrunAl pf = new FrmFirmadanUrunAl();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmStokTakip pf = new FrmStokTakip();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmVeriTabanı pf = new FrmVeriTabanı();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKullaniciEkle pf = new FrmKullaniciEkle();
            pf.TopLevel = false;
            panel1.Controls.Add(pf);
            pf.Dock = DockStyle.Fill;
            pf.Show();
            pf.TopMost = true;
            pf.BringToFront();
        }

        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text=gelenkullaniciID.ToString();

            var kimgelmis = db.TBLKULLANICI.Find(gelenkullaniciID);

            if (kimgelmis.YETKI == "A") menuStrip1.Visible = true;

            if (kimgelmis.YETKI=="K")
            {
                button1.Enabled = kimgelmis.PERSONEL.Value;
                button2.Enabled = kimgelmis.DEPARTMAN.Value;
                button3.Enabled = kimgelmis.CEKSENET.Value;
                button4.Enabled = kimgelmis.MUSTERI.Value;
                button5.Enabled = kimgelmis.STOKTAKIP.Value;
                button6.Enabled = kimgelmis.KASA.Value;
                button7.Enabled = kimgelmis.URUNLER.Value;
                button8.Enabled = kimgelmis.VERITABANI.Value;
            }           
        }
    }
}
