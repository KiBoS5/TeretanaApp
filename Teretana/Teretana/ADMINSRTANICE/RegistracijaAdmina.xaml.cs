using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for RegistracijaAdmina.xaml
    /// </summary>
    public partial class RegistracijaAdmina : Window
    {
        public RegistracijaAdmina()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Admin](UsernameA, SifraA, ImeA, PrezimeA, AdresaA, EmailA, TelefonA) VALUES(@user, @sifra, @ime, @prezime, @adresa, @email, @telefon)";
            command.Parameters.AddWithValue("@user", user.Text);
            command.Parameters.AddWithValue("@sifra", sifra.Text);
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@adresa", adresa.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            command.Parameters.AddWithValue("@telefon", telefon.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                
            }
            ponistiUnosTxt();
        }
        private void ponistiUnosTxt()
        {
            user.Text = "";
            sifra.Text = "";
            ime.Text = "";
            prezime.Text = "";
            adresa.Text = "";
            email.Text = "";
            telefon.Text = "";
        }
    }
}
