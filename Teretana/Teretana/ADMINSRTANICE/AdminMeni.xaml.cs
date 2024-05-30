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
using System.Windows.Shapes;

namespace Teretana.ADMINSRTANICE
{
    /// <summary>
    /// Interaction logic for AdminMeni.xaml
    /// </summary>
    public partial class AdminMeni : Window
    {
        public AdminMeni()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Zaposleni();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Oprema();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Lokacije();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Eventi();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Clanarina();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            admin.Content=new ADMINSRTANICE.Clanovi();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            RegistracijaAdmina dashboard = new ADMINSRTANICE.RegistracijaAdmina();
            dashboard.Show();
            this.Close();
        }
    }
}
