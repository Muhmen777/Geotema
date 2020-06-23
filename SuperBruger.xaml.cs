using System;
using System.Collections.Generic;
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

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for Superbruger.xaml
    /// </summary>
    public partial class Superbruger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand LoadDisplayTable;
        String Query;
        public Superbruger()
        {
            InitializeComponent();
        }

        private void OpretButton_Click(object sender, RoutedEventArgs e)
        {
            Opret OpretWindow = new Opret();
            OpretWindow.Show();
        }

        private void RedigerButton_Click(object sender, RoutedEventArgs e)
        {
            Rediger RedigerWindow = new Rediger();
            RedigerWindow.Show();
        }

        private void SletKnap_Click(object sender, RoutedEventArgs e)
        {
            Slet SletWindow = new Slet();
            SletWindow.Show();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            Query = "SELECT " +
                "Land.LandID, Land.Land,Land.Verdensdel, " +
                "Rang.Rang, Rang.Fodselsrate " +
                "from Land inner join " +
                "Rang on Rang.LandID = Land.LandID order by Rang;";
            LoadDisplayTable = new SqlCommand(Query, SQLCONN);
            LoadDisplayTable.ExecuteNonQuery();
            SqlDataAdapter SQLDA = new SqlDataAdapter(LoadDisplayTable);
            DataTable DTA = new DataTable("Rang");
            SQLDA.Fill(DTA);
            DGW.ItemsSource = DTA.DefaultView;
            SQLDA.Update(DTA);
            SQLCONN.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Er du sikker på, at du logge af?", "Confirmed Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Standardbruger standardbruger = new Standardbruger();
                standardbruger.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Handling fortrudt");
            }
        }
    }
}
