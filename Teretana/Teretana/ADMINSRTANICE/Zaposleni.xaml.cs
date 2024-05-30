using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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

namespace Teretana.ADMINSRTANICE
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class Zaposleni : Page
    {
        public Zaposleni()
        {
            InitializeComponent();
            prikaziZaposlene();
        }
        private void prikaziZaposlene()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Zaposleni] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Zaposleni");
            dataAdapter.Fill(dataTable);
            ZDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Zaposleni](UsernameZ, SifraZ, ImeZ, PrezimeZ, TipZ, AdresaZ, EmailZ, TelefonZ, Admin) VALUES(@user, @sifra, @ime, @prezime, @tip, @adresa, @email, @telefon, @admin)";
            command.Parameters.AddWithValue("@user", user.Text);
            command.Parameters.AddWithValue("@sifra", sifra.Text);
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@tip", tip.Text);
            command.Parameters.AddWithValue("@adresa", adresa.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            command.Parameters.AddWithValue("@telefon", telefon.Text);
            command.Parameters.AddWithValue("@admin", admin.Text);
            
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziZaposlene();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Zaposleni] WHERE IdZaposlenog = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziZaposlene();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Zaposleni] SET UsernameZ = @user, SifraZ = @sifra, ImeZ = @ime, PrezimeZ = @prezime, TipZ = @tip, AdresaZ = @adresa, EmailZ = @email, TelefonZ = @telefon, Admin = @admin WHERE IdZaposlenog = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Parameters.AddWithValue("@user", user.Text);
            command.Parameters.AddWithValue("@sifra", sifra.Text);
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@tip", tip.Text);
            command.Parameters.AddWithValue("@adresa", adresa.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            command.Parameters.AddWithValue("@telefon", telefon.Text);
            command.Parameters.AddWithValue("@admin", admin.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziZaposlene();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }

        private void ZDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id.Text = dr["IdZaposlenog"].ToString();
                user.Text = dr["UsernameZ"].ToString();
                sifra.Text = dr["SifraZ"].ToString();
                ime.Text = dr["ImeZ"].ToString();
                prezime.Text = dr["PrezimeZ"].ToString();
                tip.Text = dr["TipZ"].ToString();
                adresa.Text = dr["AdresaZ"].ToString();
                email.Text = dr["EmailZ"].ToString();
                telefon.Text = dr["TelefonZ"].ToString();
                admin.Text = dr["Admin"].ToString();
                
            }
        }
        private void ponistiUnosTxt()
        {

            id.Text = "";
            user.Text = "";
            sifra.Text = "";
            ime.Text ="";
            prezime.Text = "";
            tip.Text = "";
            adresa.Text = "";
            email.Text = "";
            telefon.Text = "";
            admin.Text = "";
        }

        private void load(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Admin] IdAdmina";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Admin");
            dataAdapterCbx.Fill(dataTableCbx);
            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                admin.Items.Add(dataTableCbx.Rows[i]["IdAdmina"]);
            }
        }
    }
}
