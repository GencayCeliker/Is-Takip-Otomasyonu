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
    public partial class FRMPERSONELGUNCELLE : Form
    {
        public FRMPERSONELGUNCELLE()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db=new DBIstakikEntities1();
        public int GelenPersonelId;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRMPERSONELGUNCELLE_Load(object sender, EventArgs e)
        {//önce combobax a departmanı taşı
            var departman = from x in db.TblDepartman
                            where x.Durum == true
                            select x;
            comboBox1.DataSource = departman.ToList();
            comboBox1.DisplayMember = "DEPARTMANADI";
            comboBox1.ValueMember = "DEPID";
            //seçilen personelin ıd aldık
            lblıd.Text= GelenPersonelId.ToString();
            //
            var guncelleme = db.TBLPersonel.Find(GelenPersonelId);

              tbad.Text= guncelleme.ADI;
              tbsad.Text= guncelleme.SOYADI;
              tbadres.Text= guncelleme.ADRES;
              tbdgmyeri.Text= guncelleme.DOGUMYERI;
              tbtel.Text= guncelleme.TELEFON;
              tbmail.Text= guncelleme.EMAIL;
            comboBox1.SelectedValue = guncelleme.DPMID;
              tbmaas.Text= guncelleme.MAAS.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(guncelleme.DOGUMTARIHI);
            radioButton1.Checked = (bool)guncelleme.MEDENI ? true: false;
            radioButton2.Checked = !(bool)guncelleme.MEDENI;
            pictureBox2.ImageLocation=Application.StartupPath +"\\Resimler\\"+guncelleme.RESIM;




        }

        private void button1_Click(object sender, EventArgs e)
        {
            var kayit = db.TBLPersonel.Find(GelenPersonelId);
            kayit.ADI=tbad.Text;
            kayit.SOYADI=tbsad.Text;
            kayit.DOGUMTARIHI=Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            kayit.DOGUMYERI = tbdgmyeri.Text;
            kayit.TELEFON = tbtel.Text;
            kayit.MAAS=Convert.ToDecimal(tbmaas.Text);
            kayit.DPMID=int.Parse(comboBox1.SelectedValue.ToString());
            kayit.MEDENI=radioButton1.Checked ? true : false;
            kayit.EMAIL=tbmail.Text;
            kayit.ADRES = tbadres.Text;
            if (lblresim.Text != "*")
            {
                kayit.RESIM = lblresim.Text;
                //Seçilen resim debug ersimler dosyasına kopyalanmalı yada eklenmeli

                pictureBox3.Image.Save(Application.StartupPath + "\\Resimler\\" + openFileDialog1.SafeFileName,
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            db.SaveChanges();
            MessageBox.Show("Personel Bilgileri Güncellendi");
            ((FrmPersonelTakip)Application.OpenForms["FrmPersonelTakip"]).personelgetir();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);

                    lblresim.Text = openFileDialog1.SafeFileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
