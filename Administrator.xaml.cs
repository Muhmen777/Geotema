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
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand LoadDisplayTable;
        string Query;
        SqlDataAdapter SQLDA;
        DataTable DT;
        public Administrator()
        {
            InitializeComponent();
        }

        private void OpretButton_Click(object sender, RoutedEventArgs e)
        {
            Opret Opretwindow = new Opret();
            Opretwindow.Show();
        }

        private void RedigerButton_Click(object sender, RoutedEventArgs e)
        {
            Rediger Redigerwindow = new Rediger();
            Redigerwindow.Show();
        }

        private void SletKnap_Click(object sender, RoutedEventArgs e)
        {
            Slet Sletwindow = new Slet(); ;
            Sletwindow.Show();
        }

        private void OpretBrugerButton_Click(object sender, RoutedEventArgs e)
        {
            CreateBruger CB = new CreateBruger();
            CB.Show();
        }

        private void SletBrugerButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteBruger DB = new DeleteBruger();
            DB.Show();
        }

        private void RedigerBrugerButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBruger UB = new UpdateBruger();
            UB.Show();
        }

        private void seBrugere_Click(object sender, RoutedEventArgs e)
        {
            brugerVisning BV = new brugerVisning();
            BV.Show();
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
            SQLDA = new SqlDataAdapter(LoadDisplayTable);
            DT = new DataTable("Rang");
            SQLDA.Fill(DT);
            DGW.ItemsSource = DT.DefaultView;
            SQLDA.Update(DT);
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
