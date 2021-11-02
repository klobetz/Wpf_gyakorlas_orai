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
    /// Interaction logic for RegisztraciosPage.xaml
    /// </summary>
    public partial class RegisztraciosPage : Page
    {
        public RegisztraciosPage()
        {
            InitializeComponent();
        }

        private void btn_regisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_felhasznalonev.Text) || string.IsNullOrWhiteSpace(tb_jelszo.Text) || string.IsNullOrWhiteSpace(tb_jelszoujra.Text))
            {
                MessageBox.Show("A mezők kitöltése kötelező!!!");
            }
            else
            {
                if (tb_jelszo.Text==tb_jelszoujra.Text)
                {
                    using (var db = new Bejelentkezes_DBEntities())
                    {
                        var ellenorzesfelhasznalonev = db.Felhasznalok.FirstOrDefault(x=>x.Felhasznalonev == tb_felhasznalonev.Text);

                        if (ellenorzesfelhasznalonev!=null)
                        {
                            MessageBox.Show("létezik ilyen felhasználónév");
                        }
                        else
                        {
                            var felhasznalotabla = new Felhasznalok();
                            felhasznalotabla.Felhasznalonev = tb_felhasznalonev.Text;

                            //var jelszohash = EasyEncryption.SHA.ComputeSHA256Hash(tb_jelszo.Text);
                            felhasznalotabla.Jelszo = EasyEncryption.SHA.ComputeSHA256Hash(tb_jelszo.Text);

                            db.Felhasznalok.Add(felhasznalotabla);
                            db.SaveChanges();
                            MessageBox.Show("Sikeres a regisztrációd!");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nem egyforma a két jelszó");
                }                
            }
        }

        private void lb_vissza_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new BejelentkezoPage());
        }
    }
}
