
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTakipOtomasyon_A
{
    public partial class FrmVeriTabanı : Form
    {
        public FrmVeriTabanı()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //sql server adını belirt
                string ServerName = @".\sqlexpress";
                string dbName = "DbIstakip25_A";

                //bağlantı nesnesi oluştur
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver= new Server(connection);

                //Yedekleme nesnesi oluştur
                Backup dbBackup = new Backup
                {
                    Action=BackupActionType.Database,
                    Database=dbName,
                    Incremental=false,
                    Initialize=true,
                };

                //Yedeğin kaydedileceği klasör
                string klasoryolu = @"E:\DBYedek";

                //Eğer bu klasör yoksa oluştur
                if(!Directory.Exists(klasoryolu)) Directory.CreateDirectory(klasoryolu);

                //Yedeği isimlendirelim
                string dosyaadi = Path.Combine(klasoryolu, $"DbIstakip25_A_{DateTime.Now:ddMMyyyy_HHmm}.bak");

                //Yedek dosyasını klasöre ekle
                dbBackup.Devices.AddDevice(dosyaadi,DeviceType.File);

                //Yedekleme işlemini başlat
                dbBackup.SqlBackup(dbserver);

                //Bilgilendir
                MessageBox.Show("Yedekleme işlemi tamamlandı\n" + dosyaadi,"BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //SQL server adını belirtiyoruz
                string ServerName = @".\sqlexpress";
                string dbName = "DbIstakip25_A";

                //bağlantı nesnesi oluşturuyoruz
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);

                //Kullanıcıdan .bak dosyasını seçtirelim
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "YEDEK DOSYASINI SEÇ (.bak) ",
                    Filter = "Yedek Dosyası (*.bak) | *.bak"
                };

                //Dosya seçilmezse işlemi iptal et
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                string bakdosyayolu = openFileDialog.FileName;

                //Restore (geri yükleme) nesnesi

                Restore dbRestore = new Restore
                {
                    Database = dbName,   //hangi vt geri yüklenecek
                    Action = RestoreActionType.Database,  //restore işlemi
                    ReplaceDatabase = true   //mevcut vt üzerine yaz
                };

                //geri yüklenecek .bak dosyasını ekle
                dbRestore.Devices.AddDevice(bakdosyayolu, DeviceType.File);

                //Veritabanındaki açık bağlantıları kapatmak için SingleUser moduna al
                string sqlDisconnectUsers = $@" ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

                dbserver.ConnectionContext.ExecuteNonQuery(sqlDisconnectUsers);

                // Geriyükleme işlemini başlat
                dbRestore.SqlRestore(dbserver);

                //Geri yükleme bitince tekrar çok kullanıcılı moda al
                string sqlMultiUser = $@"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                dbserver.ConnectionContext.ExecuteNonQuery(sqlMultiUser);

                //Bilgilendirme
                MessageBox.Show("Geri yükleme işlemi tamamlandı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hata Oluştu:\n" + ex.ToString(),
                    "HATA",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
