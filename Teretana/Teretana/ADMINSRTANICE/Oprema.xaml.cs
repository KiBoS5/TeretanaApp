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
    /// Interaction logic for Oprema.xaml
    /// </summary>
    public partial class Oprema : Page
    {
        public Oprema()
        {
            InitializeComponent();
            prikaziOpreme();
        }

        private void prikaziOpreme()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Oprema] ";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Oprema");
            dataAdapter.Fill(dataTable);
            ODataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime Datum = Convert.ToDateTime(datum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Oprema](NazivO, Opis, MaxTezina, DatumNabavke, Kolicina, IDLokacije) VALUES(@naziv, @opis, @max, @datum, @kolicina, @lokacija)";
            command.Parameters.AddWithValue("@naziv", naziv.Text);
            command.Parameters.AddWithValue("@opis", opis.Text);
            command.Parameters.AddWithValue("@max", max.Text);
            command.Parameters.AddWithValue("@datum", Datum);
            command.Parameters.AddWithValue("@kolicina", kolicina.Text);
            command.Parameters.AddWithValue("@lokacija", lokacija.Text);
            
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                prikaziOpreme();
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
            command.CommandText = "DELETE FROM [Oprema] WHERE IdOpreme = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziOpreme();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
            ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            DateTime Datum = Convert.ToDateTime(datum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Oprema] SET NazivO = @naziv, Opis = @opis, MaxTezina = @max, DatumNabavke = @datum, Kolicina = @kolicina, IDLokacije = @lokacija WHERE IdOpreme = @ID";
            command.Parameters.AddWithValue("@ID", id.Text);
            command.Parameters.AddWithValue("@naziv", naziv.Text);
            command.Parameters.AddWithValue("@opis", opis.Text);
            command.Parameters.AddWithValue("@max", max.Text);
            command.Parameters.AddWithValue("@datum", Datum);
            command.Parameters.AddWithValue("@kolicina", kolicina.Text);
            command.Parameters.AddWithValue("@lokacija", lokacija.Text);
            
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");

                prikaziOpreme();
            }
            ponistiUnosTxt();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }

        private void ODataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id.Text = dr["IdOpreme"].ToString();
                naziv.Text = dr["NazivO"].ToString();
                opis.Text = dr["Opis"].ToString();
                max.Text = dr["MaxTezina"].ToString();
                datum.Text = dr["DatumNabavke"].ToString();
                kolicina.Text = dr["Kolicina"].ToString();
                lokacija.Text = dr["IDLokacije"].ToString();
                
            }
        }
        private void ponistiUnosTxt()
        {

            id.Text = "";
            naziv.Text = "";
            opis.Text = "";
            max.Text = "";
            datum.Text = "";
            kolicina.Text = "";
            lokacija.Text = "";
        }

        private void load(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Lokacije] IdLokacije";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Lokacije");
            dataAdapterCbx.Fill(dataTableCbx);
            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                lokacija.Items.Add(dataTableCbx.Rows[i]["IdLokacije"]);
            }
        }
    }
}
