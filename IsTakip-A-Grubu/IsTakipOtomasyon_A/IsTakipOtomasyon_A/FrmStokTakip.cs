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
    public partial class FrmStokTakip : Form
    {
        public FrmStokTakip()
        {
            InitializeComponent();
        }

        DbIstakip25_AEntities db=new DbIstakip25_AEntities();
        private void FrmStokTakip_Load(object sender, EventArgs e)
        {
            verigetir();
        }

        public void verigetir()
        {
            //1-firmahareketleri getir

            var hareketler = from x in db.TBLFIRMAHAREKETLERI
                             select new
                             {
                                 x.ID,
                                 x.TBLFIRMA.FIRMA,
                                 x.TBLURUNLER.URUNADI,
                                 x.TARIH,
                                 x.ADET,
                                 x.ACIKLAMA,
                                 x.TUTAR,
                             };
            dataGridView1.DataSource= hareketler.OrderByDescending(x=>x.TARIH).ToList();

            decimal toplamgider =Convert.ToDecimal(hareketler.Sum(x => x.TUTAR));

            //2- firmaya ödemeleri getir

            var odemeler = from x in db.TBLFIRMAYAODEME
                           select new
                           {
                               x.ID,
                               x.TBLFIRMA.FIRMA,
                               x.TARIH,
                               x.ACIKLAMA,
                               x.TUTAR
                           };
            dataGridView2.DataSource = odemeler.OrderByDescending(x=>x.TARIH).ToList();

            decimal toplamodeme = Convert.ToDecimal(odemeler.Sum(x => x.TUTAR));

            tbkalan.Text=(toplamgider-toplamodeme).ToString() + " ₺";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmFirmayaOdeme f = new FrmFirmayaOdeme();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
