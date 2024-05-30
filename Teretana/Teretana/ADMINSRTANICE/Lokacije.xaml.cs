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
    /// Interaction logic for Lokacije.xaml
    /// </summary>
    public partial class Lokacije : Page
    {
        public Lokacije()
        {
            InitializeComponent();
            prikaziLokacije();

        }
        private void prikaziLokacije()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Lokacije] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Lokacije");
            dataAdapter.Fill(dataTable);
            LDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Lokacije](NazivL, AdresaL, PTT, AdminID) VALUES(@naziv, @adresa, @ptt, @admin)";
            command.Parameters.AddWithValue("@naziv", naziv.Text);
            command.Parameters.AddWithValue("@adresa", adresa.Text);
            command.Parameters.AddWithValue("@ptt", ptt.Text);
            command.Parameters.AddWithValue("@admin", admin.Text);

            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziLokacije();
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
            command.CommandText = "DELETE FROM [Lokacije] WHERE IdLokacije = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziLokacije();
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
            command.CommandText = "UPDATE [Lokacije] SET NazivL = @naziv, AdresaL = @adresa, PTT = @ptt, AdminID = @admin WHERE IdLokacije = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Parameters.AddWithValue("@naziv", naziv.Text);
            command.Parameters.AddWithValue("@adresa", adresa.Text);
            command.Parameters.AddWithValue("@ptt", ptt.Text);
            command.Parameters.AddWithValue("@admin", admin.Text);

            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziLokacije();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }

        private void LDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id.Text = dr["IdLokacije"].ToString();
                naziv.Text = dr["NazivL"].ToString();
                adresa.Text = dr["AdresaL"].ToString();
                ptt.Text = dr["PTT"].ToString();
                admin.Text = dr["AdminID"].ToString();


            }
        }
        private void ponistiUnosTxt()
        {
            id.Text = "";
            naziv.Text = "";
            adresa.Text = "";
            ptt.Text = "";
            admin.Text= "";
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
