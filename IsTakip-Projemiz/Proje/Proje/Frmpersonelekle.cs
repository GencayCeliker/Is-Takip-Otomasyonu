using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class Frmpersonelekle : Form
    {
        public Frmpersonelekle()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db=new DBIstakikEntities1();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TBLPersonel yeni=new TBLPersonel(); 
            yeni.ADI=tbad.Text;
            yeni.SOYADI=tbsad.Text;
            yeni.ADRES=tbadres.Text;
            yeni.EMAIL=tbmail.Text;
            yeni.DOGUMTARIHI = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            yeni.DOGUMYERI=tbdgmyeri.Text;
            yeni.TELEFON=tbtel.Text;
            yeni.MEDENI=radioButton1.Checked ? true : false;
            yeni.DURUM = true;
            if (lblresim.Text != "*")
            {
                yeni.RESIM = lblresim.Text;
                //Seçilen resim debug ersimler dosyasına kopyalanmalı yada eklenmeli

                pictureBox3.Image.Save(Application.StartupPath + "\\Resimler\\" + openFileDialog1.SafeFileName, 
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                yeni.RESIM = "resimyok.jpg";
            }
            yeni.DPMID=Convert.ToInt32(comboBox1.SelectedValue.ToString());
            yeni.MAAS = Convert.ToDecimal(tbmaas.Text);

            

            db.TBLPersonel.Add(yeni);
            db.SaveChanges();
            MessageBox.Show("Yeni Personel Eklendi");
            //Başka Formdan method çalıştırdık
            ((FrmPersonelTakip)Application.OpenForms["FrmPersonelTakip"]).personelgetir();
            this.Close();
        }

      

        private void Frmpersonelekle_Load(object sender, EventArgs e)
        {
            var departman = from x in db.TblDepartman
                            where x.Durum==true select x;
            comboBox1.DataSource= departman.ToList();
            comboBox1.DisplayMember = "DEPARTMANADI";
            comboBox1.ValueMember = "DEPID";

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog()==DialogResult.OK)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
