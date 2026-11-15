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
    public partial class FrmPersonelTakip : Form
    {
        public FrmPersonelTakip()
        {
            InitializeComponent();
        }
        DbIstakip25_AEntities db = new DbIstakip25_AEntities();
        private void FrmPersonelTakip_Load(object sender, EventArgs e)
        {
            if (db.TBLPERSONEL.Count() > 0)
            {
                personelgetir();

                //form açıldığında ilk kayıt aktif olsun
                var ilkayit = db.TBLPERSONEL.OrderByDescending(x => x.PERSID).FirstOrDefault();

                //bulunan personeli kontrollere taşıyalım
                lbladi.Text = ilkayit.ADI;
                lblsoyadi.Text = ilkayit.SOYADI;
                lblmaas.Text = ilkayit.MAAS.ToString();
                lbltel.Text = ilkayit.TEL;
                lbladres.Text = ilkayit.ADRES;
                lblemail.Text = ilkayit.EMAIL;
                lbldogumtarihi.Text = ilkayit.DOGUMTARIHI.ToString();
                lbldepartman.Text = ilkayit.TBLDEPARTMAN.DEPARTMAN;
                lbldogumyeri.Text = ilkayit.DOGUMYERI;
                radioButton1.Checked = Convert.ToBoolean(ilkayit.MEDENI);
                radioButton2.Checked = !Convert.ToBoolean(ilkayit.MEDENI);
                pictureBox1.ImageLocation = Application.StartupPath + "\\Resimler\\" + ilkayit.RESIM;
                lblID.Text = ilkayit.PERSID.ToString();
                


                // Departman listesi gelsin
                var depler = from x in db.TBLDEPARTMAN
                             where x.DURUM == true
                             select x;

                comboBox1.DataSource = depler.ToList();
                comboBox1.ValueMember = "DEPID";
                comboBox1.DisplayMember = "DEPARTMAN";


            }
        }
        public void personelgetir()
        {
            var degerler = from x in db.TBLPERSONEL
                           where x.DURUM == true
                           select new
                           {
                               x.PERSID,
                               x.ADI,
                               x.SOYADI,
                               x.TBLDEPARTMAN.DEPARTMAN
                           };
            dataGridView1.DataSource = degerler.OrderByDescending(x => x.PERSID).ToList();
            dataGridView1.Font = new Font("verdana", 12, FontStyle.Bold);
            dataGridView1.ForeColor = Color.DarkGreen;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "IDDD";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //seçilen kayıt kontrollere gelsin!!

            //seçilen satır indexi
            var secilenindex = dataGridView1.SelectedCells[0].RowIndex;

            //seçilen kaydın ID si
            int secilenpersonelID = Convert.ToInt32(dataGridView1.Rows[secilenindex].Cells[0].Value.ToString());

            //bu personeli bul
            var bukim = db.TBLPERSONEL.Find(secilenpersonelID);

            //bulunan personeli kontrollere taşıyalım
            lbladi.Text = bukim.ADI;
            lblsoyadi.Text = bukim.SOYADI;
            lblmaas.Text = bukim.MAAS.ToString();
            lbltel.Text = bukim.TEL;
            lbladres.Text = bukim.ADRES;
            lblemail.Text = bukim.EMAIL;
            lbldogumtarihi.Text = bukim.DOGUMTARIHI.ToString();
            lbldepartman.Text = bukim.TBLDEPARTMAN.DEPARTMAN;
            lbldogumyeri.Text = bukim.DOGUMYERI;
            radioButton1.Checked = Convert.ToBoolean(bukim.MEDENI);
            radioButton2.Checked = !Convert.ToBoolean(bukim.MEDENI);
            pictureBox1.ImageLocation = Application.StartupPath + "\\Resimler\\" + bukim.RESIM;
            lblID.Text = bukim.PERSID.ToString();
            //radioButton1.Checked = (bool)bukim.MEDENI ? true : false;
            //radioButton2.Checked = (bool)bukim.MEDENI ? true : false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmPersonelEkle pe = new FrmPersonelEkle();
            pe.TopLevel = false;
            Form1 frm = Application.OpenForms["Form1"] as Form1;
            frm.panel1.Controls.Add(pe);
            pe.Dock = DockStyle.Fill;
            pe.Show();
            pe.BringToFront();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var degerler = db.TBLPERSONEL.Where(x => x.ADI.ToLower().Contains(textBox1.Text.ToLower()) || x.SOYADI.ToLower().Contains(textBox1.Text.ToLower()));

                var istediklerim = from x in degerler
                                   where x.DURUM == true
                                   select new
                                   {
                                       x.PERSID,
                                       x.ADI,
                                       x.SOYADI,
                                       x.TBLDEPARTMAN.DEPARTMAN
                                   };

                dataGridView1.DataSource = istediklerim.OrderByDescending(x => x.PERSID).ToList();
            }
            else
            {
                personelgetir();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int arananID = int.Parse(comboBox1.SelectedValue.ToString());

            var istediklerim = from x in db.TBLPERSONEL
                               where x.DURUM == true && x.DEPARTMAN == arananID
                               select new
                               {
                                   x.PERSID,
                                   x.ADI,
                                   x.SOYADI,
                                   x.TBLDEPARTMAN.DEPARTMAN
                               };

            dataGridView1.DataSource = istediklerim.OrderByDescending(x => x.PERSID).ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(lbladi.Text + "  " + lblsoyadi.Text + "  isimli Personeli silmek istediğinize emin misiniz?", "PERSONEL SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap==DialogResult.Yes)
            {
                //burada silme değil DURUM false yapılacaktır!!! Bu da bir güncelleme işlemidir!

                var silinecekpersonel = db.TBLPERSONEL.Find(int.Parse(lblID.Text));

                silinecekpersonel.DURUM = false;

                db.SaveChanges();

                MessageBox.Show("Personel Kaydı Silindi");

                personelgetir();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPersonelGuncelle pe = new FrmPersonelGuncelle();
            pe.GelenPersonelID = Convert.ToInt32(lblID.Text);
            pe.TopLevel = false;
            Form1 frm = Application.OpenForms["Form1"] as Form1;
            frm.panel1.Controls.Add(pe);
            pe.Dock = DockStyle.Fill;
            pe.Show();
            pe.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmPersoneleOdeme pe = new FrmPersoneleOdeme();
            pe.GelenPersonelID = Convert.ToInt32(lblID.Text);
            pe.TopLevel = false;
            Form1 frm = Application.OpenForms["Form1"] as Form1;
            frm.panel1.Controls.Add(pe);
            pe.Dock = DockStyle.Fill;
            pe.Show();
            pe.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmtopluOdeme f=new FrmtopluOdeme();
            f.ShowDialog();
        }
    }
}
