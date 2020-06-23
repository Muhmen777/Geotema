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
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand SQLCMD;
        SqlDataReader SQLDR;
        public login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            SQLCMD = new SqlCommand("SELECT tildeltRettighed FROM Brugere WHERE brugernavn = '" + Bruger.Text + "' AND Kodeord = '" + adgangskode.Password + "'", SQLCONN);
            SQLDR = SQLCMD.ExecuteReader();
            if (SQLDR.HasRows)
            {
                
                SQLDR.Read(); 
                if (SQLDR[0].ToString() == "Administrator")
                {
                    Administrator Admin = new Administrator();
                    Admin.Show();
                    MessageBox.Show("Din rettighed er Admnistrator");
                }
                else if (SQLDR[0].ToString() == "Superbruger")
                {
                    Superbruger SB = new Superbruger();
                    SB.Show();
                    MessageBox.Show("Din rettighed er Superbruger");
                }
            }
            else
            {
                MessageBox.Show("Bruger findes ikke");
            }

            SQLCONN.Close();


        }
    }
}
