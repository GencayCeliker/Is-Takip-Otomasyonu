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
    public partial class FrmtopluOdeme : Form
    {
        public FrmtopluOdeme()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        private void button3_Click(object sender, EventArgs e)
        {
            var personeller=db.TBLPERSONEL.Where(x=>x.DURUM==true).ToList();

            foreach (var x in personeller)
            {
                TBLPERSONELEODEME yeni = new TBLPERSONELEODEME();
                yeni.TUR =  comboBox1.SelectedItem.ToString();
                yeni.PERSONEL = x.PERSID;
                yeni.TARIH=Convert.ToDateTime(DateTime.Now.ToShortDateString());
                double tutar = (Convert.ToDouble(x.MAAS)) * (1/100);
                yeni.ODENEN =Convert.ToDecimal(tutar);
                db.TBLPERSONELEODEME.Add(yeni);
                db.SaveChanges();
                MessageBox.Show("Tüm personele Maaşlarının %10 u kadar ödeme yapıldı");               
                this.Close();
            }
        }
    }
}
