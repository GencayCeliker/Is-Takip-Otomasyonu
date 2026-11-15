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
    public partial class FrmPersonelGuncelle : Form
    {
        public FrmPersonelGuncelle()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db = new DbIstakip25_AEntities();

        public int GelenPersonelID;

        private void FrmPersonelGuncelle_Load(object sender, EventArgs e)
        {
            // Departman listesi gelsin
            var depler = from x in db.TBLDEPARTMAN
                         where x.DURUM == true
                         select x;

            comboBox1.DataSource = depler.ToList();
            comboBox1.ValueMember = "DEPID";
            comboBox1.DisplayMember = "DEPARTMAN";

            //---------------------------------------

            lblID.Text = GelenPersonelID.ToString();
            //ID sini çektiğimiz personelin diğer verileri de kontrollere gelsin

            var guncellenecekpersonel = db.TBLPERSONEL.Find(GelenPersonelID);

            tbadi.Text = guncellenecekpersonel.ADI;
            tbsoyadi.Text = guncellenecekpersonel.SOYADI;
            tbtel.Text = guncellenecekpersonel.TEL;
            tbadres.Text = guncellenecekpersonel.ADRES;
            tbdogyeri.Text = guncellenecekpersonel.DOGUMYERI;
            tbemail.Text = guncellenecekpersonel.EMAIL;
            tbmaas.Text = guncellenecekpersonel.MAAS.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(guncellenecekpersonel.DOGUMTARIHI);
            radioButton5.Checked = guncellenecekpersonel.MEDENI == false ? true : false;
            radioButton6.Checked = guncellenecekpersonel.MEDENI == true ? true : false;
            comboBox1.SelectedValue = guncellenecekpersonel.DEPARTMAN;
            pictureBox1.ImageLocation = Application.StartupPath + "\\Resimler\\" + guncellenecekpersonel.RESIM;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pers = db.TBLPERSONEL.Find(int.Parse(lblID.Text));

            pers.ADI = tbadi.Text;
            pers.SOYADI = tbsoyadi.Text;
            pers.ADRES = tbadres.Text;
            pers.MAAS = Convert.ToDecimal(tbmaas.Text);
            pers.DOGUMTARIHI = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            pers.DEPARTMAN = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            pers.EMAIL = tbemail.Text;
            pers.TEL = tbtel.Text;
            pers.DURUM = true;
            pers.MEDENI = radioButton5.Checked ? false : true;

            if (label12.Text == "*")
            {
                pers.RESIM = "resimyok.jpg";
            }
            else
            {
                pers.RESIM = label12.Text;
                //seçilen resim proje içerisindeki Debug/Resimler klasörüne eklenmeli
                pictureBox1.Image.Save(Application.StartupPath + "\\Resimler\\" + openFileDialog1.SafeFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            db.SaveChanges();

            MessageBox.Show("Personel Kaydı Güncellendi");

            Form1 f = new Form1();
            f.button1_Click(this, EventArgs.Empty);

            //PersonelTakipteki Persgetir metodunu çağıralım
            ((FrmPersonelTakip)Application.OpenForms["FrmPersonelTakip"]).personelgetir();





            //bu formu kapat
            this.Close();

        }

        //güncellemede personel butonu çağrılşacak!!
        //    resim silinecek!!!
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
