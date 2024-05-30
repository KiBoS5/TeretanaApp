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
    /// Interaction logic for Clanarina.xaml
    /// </summary>
    public partial class Clanarina : Page
    {
        public Clanarina()
        {
            InitializeComponent();
            prikaziClanarine();
        }

        private void prikaziClanarine()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Clanarina] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Clanarina");
            dataAdapter.Fill(dataTable);
            CDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Clanarina](TipClanarine, CenaC, TrajanjeClanarine) VALUES(@Tip, @Cena, @Trajanje)";
            command.Parameters.AddWithValue("@Tip", Tip.Text);
            command.Parameters.AddWithValue("@Cena", Cena.Text);
            command.Parameters.AddWithValue("@Trajanje", Trajanje.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziClanarine();
            }
            ponistiUnosTxt();
        }








        private void ponistiUnosTxt()
        {
            ID.Text = "";
            Tip.Text = "";
            Cena.Text = "";
            Trajanje.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Clanarina] WHERE IdClanarine = @ID";
            command.Parameters.AddWithValue("@ID", ID.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziClanarine();
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
            command.CommandText = "UPDATE [Clanarina] SET TipClanarine = @Tip,CenaC = @Cena, TrajanjeClanarine = @Trajanje WHERE IdClanarine = @ID";
            command.Parameters.AddWithValue("@ID", ID.Text);
            command.Parameters.AddWithValue("@Tip", Tip.Text);
            command.Parameters.AddWithValue("@Cena", Cena.Text);
            command.Parameters.AddWithValue("@Trajanje", Trajanje.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziClanarine();
            }
            ponistiUnosTxt();
        }

        private void CDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                DataGrid dg = sender as DataGrid;
                DataRowView dr = dg.SelectedItem as DataRowView;
                if (dr != null)
                {
                    ID.Text = dr["IdClanarine"].ToString();
                    Tip.Text = dr["TipClanarine"].ToString();
                    Cena.Text = dr["CenaC"].ToString();
                    Trajanje.Text = dr["TrajanjeClanarine"].ToString();
                    
                }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }
    }
}
    


