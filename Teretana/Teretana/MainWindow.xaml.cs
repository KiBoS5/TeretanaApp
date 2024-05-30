using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Teretana.ADMINSRTANICE;

namespace Teretana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        
        
            private AuthenticationService authService;

            public MainWindow()
            {
                InitializeComponent();
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\bvkib\\source\\repos\\Teretana\\Teretana\\Baza\\TeretanaDB.mdf;Integrated Security=True";
                authService = new AuthenticationService(connectionString);
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                string username = user.Text; 
                string password = pass.Password; 

                if (authService.ValidateUser(username, password, "Admin"))
                {
                    AdminMeni dashboard = new ADMINSRTANICE.AdminMeni();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka za admina.");
                }
            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                string username = user.Text; 
                string password = pass.Password; 

                if (authService.ValidateUser1(username, password, "Zaposleni"))
                {
                    ZaposleniMeni dashboard = new ADMINSRTANICE.ZaposleniMeni();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka za zaposlenog.");
                }
            }
        
    }
    public class AuthenticationService
    {
        private string connectionString;

        public AuthenticationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool ValidateUser(string username, string password, string userType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(1) FROM {userType} WHERE UsernameA = @Username AND SifraA = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public bool ValidateUser1(string username, string password, string userType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(1) FROM {userType} WHERE UsernameZ = @Username AND SifraZ = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}
