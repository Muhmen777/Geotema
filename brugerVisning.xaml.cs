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
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for brugerVisning.xaml
    /// </summary>
    public partial class brugerVisning : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand LoadDisplayTable;
        SqlDataAdapter SQLDA;
        DataTable DT;
        string Query;
        public brugerVisning()
        {
            InitializeComponent();
        }


        private void Indlaesbrugere_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            Query = "SELECT * FROM Brugere";
            LoadDisplayTable = new SqlCommand(Query, SQLCONN);
            LoadDisplayTable.ExecuteNonQuery();

            SQLDA = new SqlDataAdapter(LoadDisplayTable);
            DT = new DataTable("Brugere");

            SQLDA.Fill(DT);
            brugergridview.ItemsSource = DT.DefaultView;
            SQLDA.Update(DT);
            SQLCONN.Close();
        }
    }
}

