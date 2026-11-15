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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        private void button3_Click(object sender, EventArgs e)
        {
            var bul=db.TBLKULLANICI.Where(x=>x.DURUM==true && x.KULLANICIADI==tbkullanici.Text && x.SIFRE==tbsifre.Text).FirstOrDefault();

            if (bul!=null)
            {
                Form1 f = new Form1();
                f.gelenkullaniciID = bul.ID;
                this.Hide();               
                f.Show();
            }
            else
            {
                label3.Text = "Kullanıcı Adı veya Şifreniz Yanlış!\nTekrar Deneyiniz!!!";
            }
        }
    }
}
