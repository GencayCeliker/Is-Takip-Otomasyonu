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
    public partial class FrmDepartman1 : Form
    {
        public FrmDepartman1()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db = new DBIstakikEntities1();
        private void button1_Click(object sender, EventArgs e)
        {

            TblDepartman yeni = new TblDepartman();
            yeni.DEPARTMANADI = tbad.Text;
            yeni.Durum = true;

            db.TblDepartman.Add(yeni);
            db.SaveChanges();

            MessageBox.Show("Yeni Departman Eklendi");
            DepartmanList();
        }
        private void DepartmanList()
        {
            if (db.TblDepartman.Count() > 0)
            {
                var calısanlar = from x in db.TblDepartman
                                 where x.Durum == true
                                 select new
                                 {
                                     x.DEPID,
                                     x.DEPARTMANADI,

                                 };
                dataGridView1.DataSource = calısanlar.ToList();
                dataGridView1.DataSource = calısanlar.OrderByDescending(x => x.DEPID).ToList();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDepartman1_Load(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("arial", 10, FontStyle.Bold);
            dataGridView1.ForeColor = Color.DarkBlue;

            var depler = from x in db.TblDepartman
                         where x.Durum == true
                         select x;
          
            DepartmanList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatirindeksi = dataGridView1.SelectedCells[0].RowIndex;

            int secilenpdepartman = Convert.ToInt32(dataGridView1.Rows[secilensatirindeksi].Cells[0].Value.ToString());

            var bukim = db.TblDepartman.Find(secilenpdepartman);
            lblıd.Text = bukim.DEPID.ToString();
            tbad.Text = bukim.DEPARTMANADI;
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

        private void button4_Click(object sender, EventArgs e)
        {
            tbad.Text = "";
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
            else
            {
                DepartmanList();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}