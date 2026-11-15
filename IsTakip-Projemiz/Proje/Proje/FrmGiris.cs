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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db= new DBIstakikEntities1();
        public int kullaniciid;
        private void button1_Click(object sender, EventArgs e)
        {
            var bul=db.TBLKULLANICI.Where(x=>x.KullaniciAdi==textBox1.Text && x.Sifre==textBox2.Text);
            if (bul.Count()>0)
            {
                MessageBox.Show("Giriş Başarılı");
                FRMana f=new FRMana();
                f.kullaniciid=bul.FirstOrDefault().Id;
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
            }
        }
    }
}
