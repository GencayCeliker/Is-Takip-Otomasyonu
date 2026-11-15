using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class FrmPersonelTakip : Form
    {
        public FrmPersonelTakip()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmPersonelTakip_Load(object sender, EventArgs e)
        {
            personelgetir();
            dataGridView1.Font=new Font("arial",10,FontStyle.Bold);
            dataGridView1.ForeColor = Color.DarkBlue;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].Visible = false;//id gizleme

            var bukim = db.TBLPersonel.OrderByDescending(x => x.PRSNLID).FirstOrDefault();
            lblad.Text = bukim.ADI;
            lblsoyad.Text = bukim.SOYADI;
            lbladres.Text = bukim.ADRES;
            lbltel.Text = bukim.TELEFON;
            lbldprtmn.Text = bukim.TblDepartman.DEPARTMANADI;
            lblmail.Text = bukim.EMAIL;
            lblmaas.Text = bukim.MAAS.ToString();
            lbldgmtrh.Text = bukim.DOGUMTARIHI.ToString();
            lbldgmyeri.Text = bukim.DOGUMYERI.ToString();
            radioButton1.Checked = (bool)bukim.MEDENI;
            radioButton2.Checked = !(bool)bukim.MEDENI;
            lblıd.Text = bukim.PRSNLID.ToString();

            pictureBox1.ImageLocation = Application.StartupPath + "\\Resimler\\" + bukim.RESIM;
            var depler = from x in db.TblDepartman
                         where x.Durum == true
                         select x;
            comboBox1.DataSource = depler.ToList();
            comboBox1.DisplayMember = "DEPARTMANADI";
            comboBox1.ValueMember = "DEPID";
        }

        //combobaxa load gelsin
        public void personelgetir()
        {
            if (db.TBLPersonel.Count()>0)
            {
                var calısanlar = from x in db.TBLPersonel
                                 where x.DURUM == true
                                 select new
                                 {
                                     x.PRSNLID,
                                     x.ADI,
                                     x.SOYADI,
                                     x.TblDepartman.DEPARTMANADI,
                                     x.MAAS
                                 };
              
                dataGridView1.DataSource = calısanlar.OrderByDescending(x=>x.PRSNLID).ToList();
            }
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;

            int secilenpersıd = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());

            var bukim = db.TBLPersonel.Find(secilenpersıd);

            lblad.Text = bukim.ADI;
            lblsoyad.Text= bukim.SOYADI;
            lbladres.Text = bukim.ADRES;
            lbltel.Text = bukim.TELEFON;
            lbldprtmn.Text = bukim.TblDepartman.DEPARTMANADI;
            lblmail.Text = bukim.EMAIL;
            lblmaas.Text=bukim.MAAS.ToString();
            lbldgmtrh.Text=bukim.DOGUMTARIHI.ToString();   
            lbldgmyeri.Text=bukim.DOGUMYERI.ToString();
            radioButton1.Checked = (bool)bukim.MEDENI;
            radioButton2.Checked = !(bool)bukim.MEDENI;
            pictureBox1.ImageLocation = Application.StartupPath + "\\Resimler\\" + bukim.RESIM;
            lblıd.Text = bukim.PRSNLID.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frmpersonelekle pe=new Frmpersonelekle();
            pe.TopLevel = false;
            FRMana frm1 = Application.OpenForms["FRMana"] as FRMana;
            frm1.panel1.Controls.Add(pe);
            pe.Dock= DockStyle.Fill;
            pe.Show();
            pe.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FRMPERSONELGUNCELLE pe = new FRMPERSONELGUNCELLE();
            pe.GelenPersonelId = int.Parse(lblıd.Text);
            pe.TopLevel = false;
            FRMana frm1 = Application.OpenForms["FRMana"] as FRMana;
            frm1.panel1.Controls.Add(pe);
            pe.Dock = DockStyle.Fill;
            pe.Show();
            pe.BringToFront();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                var arananlar = db.TBLPersonel.Where(x => x.ADI.ToLower().Contains(textBox1.Text.ToLower()) ||
            x.SOYADI.ToLower().Contains(textBox1.Text.ToLower()));


                var deger = from x in arananlar
                            where x.DURUM == true
                            select new
                            {
                                x.PRSNLID,
                                x.ADI,
                                x.SOYADI,
                                x.TblDepartman.DEPARTMANADI
                            };
                dataGridView1.DataSource = deger.OrderByDescending(x => x.PRSNLID).ToList();
            }
            else
            {
                personelgetir();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int depıd=int.Parse(comboBox1.SelectedValue.ToString());
            var deger = from x in db.TBLPersonel where x.DURUM == true && x.DPMID == depıd
                        where x.DURUM == true
                        select new
                        {
                            x.PRSNLID,
                            x.ADI,
                            x.SOYADI,
                            x.TblDepartman.DEPARTMANADI
                        };
            dataGridView1.DataSource = deger.OrderByDescending(x => x.PRSNLID).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(lblad.Text + " " + lblsoyad.Text + " 'i silmek istediğinizden emin misiniz", "Personel Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap==DialogResult.Yes)
            {
                var silinecekpersonel = db.TBLPersonel.Find(int.Parse(lblıd.Text));
                silinecekpersonel.DURUM = false;
                db.SaveChanges();
                MessageBox.Show("Personeliniz silindi");
                personelgetir();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
