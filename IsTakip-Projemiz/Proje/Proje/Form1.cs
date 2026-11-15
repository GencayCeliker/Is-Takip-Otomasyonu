using System;
using System.Windows.Forms;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
namespace Proje
{
    public partial class FRMana : Form
    {
        public FRMana()
        {
            InitializeComponent();
        }
       DBIstakikEntities1 db= new DBIstakikEntities1();
        public int kullaniciid;

        private void FRMana_Load(object sender, EventArgs e)
        {
            this.Text = kullaniciid.ToString();

            //3 gün içinde ödenecek çek/senetleri getir
            DateTime bugun = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime sontarih = bugun.AddDays(3);
            var degerler = from x in db.TBLCEKSENET
                           where x.OdemeTarihi >= bugun && x.OdemeTarihi <= sontarih && x.Durum == false
                           select x;
            if (degerler.Count()>0)
            {
              DialogResult cevap= MessageBox.Show("3 gün içinde ödenecek çek/senetleriniz bulunmaktadır.Lütfen çek/senet takibini görmek ister misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap==DialogResult.Yes)
                {
                    Frmceksenet4 gel = new Frmceksenet4();
                    gel.TopLevel = false;
                    panel1.Controls.Add(gel);
                    gel.Dock = DockStyle.Fill;
                    gel.Show();
                    gel.TopMost = true;
                    gel.BringToFront();
                    ((Frmceksenet4)Application.OpenForms["Frmceksenet4"]).sonucgunlistele();
                }
            }
            var bukim = db.TBLKULLANICI.Find(kullaniciid);
            button1.Enabled = Convert.ToBoolean(bukim.Personel);
            button2.Enabled = Convert.ToBoolean(bukim.Departman);
            button3.Enabled = Convert.ToBoolean(bukim.CekSenet);
            button4.Enabled = Convert.ToBoolean(bukim.Musteri);
            button5.Enabled = Convert.ToBoolean(bukim.StokTakip);
            button6.Enabled = Convert.ToBoolean(bukim.Kasa);
            button7.Enabled = Convert.ToBoolean(bukim.Urunler);
            button8.Enabled = Convert.ToBoolean(bukim.VeriTabni);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPersonelTakip gel = new FrmPersonelTakip();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDepartman1 gel = new FrmDepartman1();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmKasaTakip gel = new FrmKasaTakip();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmMusteriTakip gel = new FrmMusteriTakip();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frmceksenet4 gel = new Frmceksenet4();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmFirmaUrunAl gel = new FrmFirmaUrunAl();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmStokTakip gel = new FrmStokTakip();
            gel.TopLevel = false;
            panel1.Controls.Add(gel);
            gel.Dock = DockStyle.Fill;
            gel.Show();
            gel.TopMost = true;
            gel.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmVeriTabaniIslemleri gel = new FrmVeriTabaniIslemleri();
            gel.Show();
           
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKullaniciEkle f = new FrmKullaniciEkle();
            f.ShowDialog();
        }

        private void kitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}
