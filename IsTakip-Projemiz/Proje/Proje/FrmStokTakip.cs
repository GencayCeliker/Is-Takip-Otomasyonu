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
    public partial class FrmStokTakip : Form
    {
        public FrmStokTakip()
        {
            InitializeComponent();
        }
        DBIstakikEntities1 db=new DBIstakikEntities1();
        public void verigetir()
        {//1- firma hareketleri getirm
            var firmaHareketleri = from y in db.TBLFIRMAHAREKET
                                  select new
                                  {
                                      y.ID,
                                      y.TBLURUNLER.UrunAdi,
                                      y.TBLFIRMA.FirmaAdi,
                                      y.Tarih,
                                      y.Adet,
                                      y.Tutar,
                                      
                                  };
            dataGridView1.DataSource = firmaHareketleri.OrderByDescending(x=>x.Tarih).ToList();
            decimal fhtoplam=Convert.ToDecimal(firmaHareketleri.Sum(x => x.Tutar));
            tbToplam.Text = fhtoplam.ToString()+" ₺";
            //2-Firma ödemeleri getir
            var firmaodemeleri = from z in db.TBLFIRMAODEME
                                 select new
                                 {
                                     z.Id,
                                     z.TBLFIRMA.FirmaAdi,
                                     z.Tarih,
                                     z.Tutar,
                                 };
            dataGridView2.DataSource = firmaodemeleri.OrderByDescending(x=>x.Tarih).ToList();
            decimal fotoplam=Convert.ToDecimal(firmaodemeleri.Sum(x => x.Tutar));
            tbOdenecekler.Text = fotoplam.ToString()+" ₺";
            //3-kalan borç
            tbkborc.Text = (fhtoplam - fotoplam).ToString() + " ₺";
        }

        private void FrmStokTakip_Load(object sender, EventArgs e)
        {
            verigetir();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //sadece tıklanan satırdaki firma verileri gelsin
            int secilenindex = dataGridView1.SelectedCells[0].RowIndex;
            int secilenid = int.Parse(dataGridView1.Rows[secilenindex].Cells["ID"].Value.ToString());
            var bul = db.TBLFIRMAHAREKET.Find(secilenid);
            int firmaid = Convert.ToInt32(bul.Fırma);
            var firmaHareketleri = from y in db.TBLFIRMAHAREKET
                                      where y.Fırma == firmaid
                                   select new
                                   {
                                       y.ID,
                                       y.TBLURUNLER.UrunAdi,
                                       y.TBLFIRMA.FirmaAdi,
                                       y.Tarih,
                                       y.Adet,
                                       y.Tutar,

                                   };
            dataGridView1.DataSource = firmaHareketleri.OrderByDescending(x => x.Tarih).ToList();
            decimal fhtoplam = Convert.ToDecimal(firmaHareketleri.Sum(x => x.Tutar));
            tbToplam.Text = fhtoplam.ToString() + " ₺";
            //2-Firma ödemeleri getir
            var firmaodemeleri = from z in db.TBLFIRMAODEME
                                    where z.Firma== firmaid
                                 select new
                                 {
                                     z.Id,
                                     z.TBLFIRMA.FirmaAdi,
                                     z.Tarih,
                                     z.Tutar,
                                 };
            dataGridView2.DataSource = firmaodemeleri.OrderByDescending(x => x.Tarih).ToList();
            decimal fotoplam = Convert.ToDecimal(firmaodemeleri.Sum(x => x.Tutar));
            tbOdenecekler.Text = fotoplam.ToString() + " ₺";
            //3-kalan borç
            tbkborc.Text = (fhtoplam - fotoplam).ToString() + " ₺";

        }

        private void button1_Click(object sender, EventArgs e)
        {
           FrmFirmaOdeme f=new FrmFirmaOdeme();
            f.ShowDialog();
        }
    }
}
