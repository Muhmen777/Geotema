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
using System.Data.SqlClient;

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for DeleteBruger.xaml
    /// </summary>
    public partial class DeleteBruger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand DeleteCommand;
        SqlCommand SQLCMD;
        SqlDataReader SQLDR;
        string Select;
        public DeleteBruger()
        {
            InitializeComponent();
            fillcombox();
        }

        public void fillcombox()
        {
            SQLCONN.Open();
            Select = "SELECT * FROM Brugere";
            SQLCMD = new SqlCommand(Select, SQLCONN);
            SQLDR = SQLCMD.ExecuteReader();

            while (SQLDR.Read())
            {
                int brugerID = SQLDR.GetInt32(0);
                Brugercombo.Items.Add(brugerID);
            }
            SQLCONN.Close();
        }
 
        private void Brugercombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLCONN.Open();
            Select = "SELECT * FROM Brugere WHERE BrugerID= '" + Brugercombo.SelectedItem + "';";

            SQLCMD = new SqlCommand(Select, SQLCONN);
            SQLDR = SQLCMD.ExecuteReader();
            while(SQLDR.Read())
            {
                string fuldenavn = SQLDR.GetString(1);
                string brugernavn = SQLDR.GetString(2);
                string kodeord = SQLDR.GetString(3);
                string gentagkode = SQLDR.GetString(4);
                string tildeltrettighed = SQLDR.GetString(5);

                Fuldenavn.Text = fuldenavn;
                Brugernavn.Text = brugernavn;
                Kodeord.Password = kodeord;
                GentagKodeord.Password = gentagkode;
                rettighed.Text = tildeltrettighed;
            }
            SQLCONN.Close();
        }       
        
        private void SletBruger_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            DeleteCommand = new SqlCommand("DELETE FROM Brugere WHERE BrugerID ="+ Brugercombo.SelectedItem, SQLCONN);
            DeleteCommand.ExecuteNonQuery();
            MessageBox.Show("Bruger slettet");
            Fuldenavn.Text = "";
            Brugernavn.Text = "";
            Kodeord.Password = "";
            GentagKodeord.Password = "";
            rettighed.Text = "";
            SQLCONN.Close();
        }

    }
}
