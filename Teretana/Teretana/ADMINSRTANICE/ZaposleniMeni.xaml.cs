using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for ZaposleniMeni.xaml
    /// </summary>
    public partial class ZaposleniMeni : Window
    {
        public ZaposleniMeni()
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
            DateTime Datum = Convert.ToDateTime(datum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Clanovi](ImeC, PrezimeC, EmailC, Clanarina, DatumUplate, DatumIsteka, Zaposleni) VALUES(@ime, @prezime, @email, @clanarina, @datumu, @datumi, @zaposleni)";
            command.Parameters.AddWithValue("@ime", ime.Text);
            command.Parameters.AddWithValue("@prezime", prezime.Text);
            command.Parameters.AddWithValue("@email", email.Text);
            int space_indexx = clanarina.Text.IndexOf(" ");
            string conn = clanarina.Text.Substring(0,space_indexx+1);
            command.Parameters.AddWithValue("@clanarina", conn);

            command.Parameters.AddWithValue("@datumu", Datum);
            int space_index = clanarina.Text.LastIndexOf(" ");
            string datee = clanarina.Text.Substring(space_index+1);


            int date = Int32.Parse(datee);
            command.Parameters.AddWithValue("@datumi", Datum.AddDays(date));
            command.Parameters.AddWithValue("@zaposleni", zaposleni.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                
            }
            ponistiUnosTxt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }
        private void ponistiUnosTxt()
        {

            
            ime.Text = "";
            prezime.Text = "";
            email.Text = "";
            clanarina.Text = "";
            datum.Text = "";
            zaposleni.Text = "";
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
                zaposleni.Items.Add(dataTableCbx.Rows[i]["IdZaposlenog"]);
            }
        }

        private void loaded1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT IdClanarine, TipClanarine, TrajanjeClanarine FROM [Clanarina]";
            commandCbx.Connection = connection;
            SqlDataReader sqlReader = commandCbx.ExecuteReader();
            while (sqlReader.Read())
            {
                int id = sqlReader.GetInt32(0);
                string tip = sqlReader.GetString(1);
                string trajanje = sqlReader.GetString(2);
                string txt = id+" "+tip+" "+trajanje;
                clanarina.Items.Add(txt);
            }


        }

        private void clanarina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = clanarina.SelectedItem.ToString();
            System.Windows.MessageBox.Show(temp);
            int space_index = clanarina.SelectedItem.ToString().IndexOf(" ");
            string content = clanarina.SelectedItem.ToString().Substring(0,space_index);
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["TeretanaDB"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT CenaC FROM [Clanarina] where IdClanarine = @con ";
            commandCbx.Parameters.AddWithValue("@con", content);
            commandCbx.Connection = connection;
            SqlDataReader sqlReader = commandCbx.ExecuteReader();
            
            while (sqlReader.Read())
            {
                kcena.Content = sqlReader.GetString(0);
            }


        }
    }
 }

