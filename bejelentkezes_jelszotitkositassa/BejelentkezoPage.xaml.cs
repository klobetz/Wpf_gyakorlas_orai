using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bejelentkezes_jelszotitkositassa
{
    /// <summary>
    /// Interaction logic for BejelentkezoPage.xaml
    /// </summary>
    public partial class BejelentkezoPage : Page
    {
        public BejelentkezoPage()
        {
            InitializeComponent();
        }

        private void lb_regisztracio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegisztraciosPage());
        }

        private void btn_belepes_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_felhasznalonev.Text)||string.IsNullOrWhiteSpace(tb_jelszo.Text))
            {
                MessageBox.Show("A mezők kitöltése kötelező!!!");
            }
            else
            {
                using (var db = new Bejelentkezes_DBEntities())
                {
                    var jelszohash = EasyEncryption.SHA.ComputeSHA256Hash(tb_jelszo.Text);
                    var ellenorzes = db.Felhasznalok.FirstOrDefault(x => x.Felhasznalonev == tb_felhasznalonev.Text && EasyEncryption.SHA.Equals(x.Jelszo, jelszohash));
                    
                    
                    //kodolás nélkül
                    //var ellenorzes = db.Felhasznalok.FirstOrDefault(x => x.Felhasznalonev == tb_felhasznalonev.Text && x.Jelszo == tb_jelszo.Text);

                    if (ellenorzes!=null)
                    {
                        NavigationService.Navigate(new UdvozloPage());
                    }
                    else
                    {
                        MessageBox.Show("Nem jó a felhasználó vagy a jelszó");
                        tb_felhasznalonev.Clear();
                        tb_jelszo.Clear();
                        tb_felhasznalonev.Focus();
                    }

                }
            }



            //NavigationService.Navigate(new UdvozloPage());
        }
    }
}
