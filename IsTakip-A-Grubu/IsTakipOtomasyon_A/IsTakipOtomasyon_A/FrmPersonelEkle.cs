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
    public partial class FrmPersonelEkle : Form
    {
        public FrmPersonelEkle()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void button2_Click(object sender, EventArgs e)
        {

            TBLPERSONEL yeni = new TBLPERSONEL();
            yeni.ADI = tbadi.Text;
            yeni.SOYADI = tbsoyadi.Text;
            yeni.ADRES = tbadres.Text;
            yeni.MAAS = Convert.ToDecimal(tbmaas.Text);
            yeni.DOGUMTARIHI = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            yeni.DEPARTMAN = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            yeni.EMAIL = tbemail.Text;
            yeni.TEL = tbtel.Text;
            yeni.DOGUMYERI = tbdogyeri.Text;
            yeni.DURUM = true;
            yeni.MEDENI = radioButton5.Checked ? false : true;

            if (label12.Text == "*")
            {
                yeni.RESIM = "resimyok.jpg";
            }
            else
            {
                yeni.RESIM = label12.Text;
                //seçilen resim proje içerisindeki Debug/Resimler klasörüne eklenmeli
                pictureBox1.Image.Save(Application.StartupPath + "\\Resimler\\" + openFileDialog1.SafeFileName,System.Drawing.Imaging.ImageFormat.Jpeg);
            }


            db.TBLPERSONEL.Add(yeni);
            db.SaveChanges();

            MessageBox.Show("Yeni Personel Kaydı Tamamlandı");

            //PersonelTakipteki Persgetir metodunu çağıralım
            ((FrmPersonelTakip)Application.OpenForms["FrmPersonelTakip"]).personelgetir();

            //bu formu kapat
            this.Close();
        }

        private void FrmPersonelEkle_Load(object sender, EventArgs e)
        {
            // Departman listesi gelsin
            var depler = from x in db.TBLDEPARTMAN
                         where x.DURUM == true
                         select x;

            comboBox1.DataSource = depler.ToList();
            comboBox1.ValueMember = "DEPID";
            comboBox1.DisplayMember = "DEPARTMAN";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                    label12.Text = openFileDialog1.SafeFileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
