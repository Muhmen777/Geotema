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
using System.Windows.Shapes;
using System.Data;
namespace GeoTema
{
    /// <summary>
    /// Interaction logic for Standardbruger.xaml
    /// </summary>
    public partial class Standardbruger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand LoadDisplayTable;
        string Query;
        public Standardbruger()
        {
            InitializeComponent();
        }

        public void LoadButton_Click(object sender, RoutedEventArgs e)
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            login log = new login();
            log.Show();
        }
    }
}
