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
    /// Interaction logic for Clanovi.xaml
    /// </summary>
    public partial class Clanovi : Page
    {
        public Clanovi()
        {
            InitializeComponent();
            prikaziClanove();



        }

        private void prikaziClanove()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Clanovi] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Clanovi");
            dataAdapter.Fill(dataTable);
            ClDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime Datum = Convert.ToDateTime(datumu.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Clanovi](ImeC, PrezimeC, EmailC, Clanarina, DatumUplate, DatumIsteka, Zaposleni) VALUES(@ime, @prezime, @email, @clanarina, @datumu, @datumi, @zaposleni)";
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            int space_index = clanarina.Text.IndexOf(" ");
            string content = clanarina.Text.Substring(0,space_index+1);
            string datee = clanarina.Text.Substring(space_index);
            
            
            int date = Int32.Parse(datee);
            command.Parameters.AddWithValue("@clanarina", content);
            command.Parameters.AddWithValue("@datumu", Datum);
            command.Parameters.AddWithValue("@datumi", Datum.AddDays(date));
            command.Parameters.AddWithValue("@zaposleni", zaposleni.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziClanove();
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
            command.CommandText = "DELETE FROM [Clanovi] WHERE IdClana = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziClanove();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime Datum = Convert.ToDateTime(datumu.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Clanovi] SET ImeC = @ime, PrezimeC = @prezime, EmailC=@email , Clanarina = @clanarina, DatumUplate = @datumu, DatumIsteka=@datumi, Zaposleni=@zaposleni  WHERE IdClana = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            int space_indexx = clanarina.Text.IndexOf(" ");
            string conn = clanarina.Text.Substring(0,space_indexx+1);
            command.Parameters.AddWithValue("@clanarina", conn);
            command.Parameters.AddWithValue("@datumu", Datum);

            int space_index = clanarina.Text.IndexOf(" ");
            string datee = clanarina.Text.Substring(space_index);
            int date = Int32.Parse(datee);

            command.Parameters.AddWithValue("@datumi", Datum.AddDays(date));
            command.Parameters.AddWithValue("@zaposleni", zaposleni.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziClanove();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }

        private void ClDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id.Text = dr["IdClana"].ToString();
                ime.Text = dr["ImeC"].ToString();
                prezime.Text = dr["PrezimeC"].ToString();
                email.Text = dr["EmailC"].ToString();
                clanarina.Text = dr["Clanarina"].ToString();
                datumu.Text = dr["DatumUplate"].ToString();
                zaposleni.Text = dr["Zaposleni"].ToString();
            }
        }

        private void ponistiUnosTxt()
        {
            
            id.Text = "";
            ime.Text = "";
            prezime.Text = "";
            email.Text = "";
            clanarina.Text = "";
            datumu.Text = "";
            zaposleni.Text = "";
        }

        private void load(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT IdClanarine, TrajanjeClanarine FROM [Clanarina]";
            commandCbx.Connection = connection;
            SqlDataReader sqlReader = commandCbx.ExecuteReader();
            while (sqlReader.Read())
            {
                int id = sqlReader.GetInt32(0);
                string trajanje = sqlReader.GetString(1);
                
                string txt = id+" "+trajanje;
                clanarina.Items.Add(txt);
            }
        }

        private void load1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Zaposleni] IdZaposlenog";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Zaposleni");
            dataAdapterCbx.Fill(dataTableCbx);
            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                zaposleni.Items.Add(dataTableCbx.Rows[i]["IdZaposlenog"]);
            }
        }
    }
}
