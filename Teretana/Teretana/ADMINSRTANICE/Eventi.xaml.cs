using System;
using System.Collections.Generic;
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
using System.Configuration;



namespace Teretana.ADMINSRTANICE
{
    /// <summary>
    /// Interaction logic for Eventi.xaml
    /// </summary>

    public partial class Eventi : Page
    {

        public Eventi()
        {
            InitializeComponent();
            prikaziEvente();
        }
        private void ponistiUnosTxt()
        {
            Id.Text = "";
            Naziv.Text = "";
            Popust.Text = "";
            Datum.Text= "";
            Vodja.Text = "";
        }
        private void prikaziEvente()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Eventi] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Eventi");
            dataAdapter.Fill(dataTable);
            EDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime datum = Convert.ToDateTime(Datum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Eventi](Naziv, Popusti, DatumEventa, VodjaEventa) VALUES(@naziv, @popust, @datum, @vodja)";
            command.Parameters.AddWithValue("@naziv", Naziv.Text);
            command.Parameters.AddWithValue("@popust", Popust.Text);
            command.Parameters.AddWithValue("@datum", datum);
            command.Parameters.AddWithValue("@vodja", Vodja.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziEvente();
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
            command.CommandText = "DELETE FROM [Eventi] WHERE IdEventa = @ID";
            command.Parameters.AddWithValue("@ID", Id.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziEvente();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime datum = Convert.ToDateTime(Datum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Eventi] SET Naziv = @naziv, Popusti = @popust, DatumEventa = @datum, @vodja = VodjaEventa WHERE IdEventa = @ID";
            command.Parameters.AddWithValue("@ID", Id.Text);
            command.Parameters.AddWithValue("@naziv", Naziv.Text);
            command.Parameters.AddWithValue("@popust", Popust.Text);
            command.Parameters.AddWithValue("@datum", datum);
            command.Parameters.AddWithValue("@vodja", Vodja.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziEvente();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }

        private void EDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                Id.Text = dr["IdEventa"].ToString();
                Naziv.Text = dr["Naziv"].ToString();
                Popust.Text = dr["Popusti"].ToString();
                Datum.Text = dr["DatumEventa"].ToString();
                Vodja.Text = dr["VodjaEventa"].ToString();
            }
        }

        private void load(object sender, RoutedEventArgs e)
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
                Vodja.Items.Add(dataTableCbx.Rows[i]["IdZaposlenog"]);
            }
        }
    }







}

