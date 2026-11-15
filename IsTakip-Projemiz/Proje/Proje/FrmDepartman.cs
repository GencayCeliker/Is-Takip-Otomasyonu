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
    public partial class FrmDepartman : Form
    {
        public FrmDepartman()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void FrmDepartman_Load(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("arial", 10, FontStyle.Bold);
            dataGridView1.ForeColor = Color.DarkBlue;
   

            DepartmanList();
        }

        private void DepartmanList()
        {
            var depler = db.TblDepartman
                          .Where(x => x.Durum == true)
                          .Select(x => new { x.DEPID, x.DEPARTMANADI })
                          .ToList();

            dataGridView1.DataSource = depler;

            // ComboBox'ı güvenli şekilde doldur
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged_2;
            comboBox1.DataSource = depler;
            comboBox1.DisplayMember = "DEPARTMANADI";
            comboBox1.ValueMember = "DEPID";
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_2;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbad.Text))
            {
                MessageBox.Show("Departman adı girin.");
                return;
            }

            TblDepartman yeni = new TblDepartman();
            yeni.DEPARTMANADI = tbad.Text;
            yeni.Durum = true; // görünmesi için gerekli

            db.TblDepartman.Add(yeni);
            db.SaveChanges();

            MessageBox.Show("Yeni Departman Eklendi");
            DepartmanList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(tbad.Text + " " + "  silmek istediğinizden emin misiniz", "Departman Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                var silinecekdepartman = db.TblDepartman.Find(int.Parse(lblıd.Text));
                silinecekdepartman.Durum = false;
                db.SaveChanges();
                MessageBox.Show("Departmanınız silindi");
                var depler = from x in db.TblDepartman
                             where x.Durum == true
                             select new
                             {
                                 x.DEPID,
                                 x.DEPARTMANADI
                             };
                dataGridView1.DataSource = depler.ToList();
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var kayit = db.TblDepartman.Find(int.Parse(lblıd.Text)); 
            kayit.DEPARTMANADI = tbad.Text;
            MessageBox.Show("Departmanınız Güncellendi");
            db.SaveChanges();
            var depler = from x in db.TblDepartman
                         where x.Durum == true
                         select new
                         {
                             x.DEPID,
                             x.DEPARTMANADI
                         };
            dataGridView1.DataSource = depler.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            tbad.Text = "";
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;

            int secilenpdepartman = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());

            var bukim = db.TblDepartman.Find(secilenpdepartman);
            lblıd.Text = bukim.DEPID.ToString();
            tbad.Text = bukim.DEPARTMANADI;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var arananlar = db.TblDepartman.Where(x => x.DEPARTMANADI.ToLower().Contains(textBox1.Text.ToLower()));


                var deger = from x in arananlar
                            where x.Durum == true
                            select new
                            {
                                x.DEPID,
                                x.DEPARTMANADI
                            };
                dataGridView1.DataSource = deger.OrderByDescending(x => x.DEPID).ToList();
            }
        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

            int depıd = int.Parse(comboBox1.SelectedValue.ToString());
            var deger = from x in db.TblDepartman
                        where x.Durum == true
                        select new
                        {
                            x.DEPID,
                            x.DEPARTMANADI

                        };
            dataGridView1.DataSource = deger.OrderByDescending(x => x.DEPID).ToList();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
