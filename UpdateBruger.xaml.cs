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
    /// Interaction logic for UpdateBruger.xaml
    /// </summary>
    public partial class UpdateBruger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand Updatecommand;
        string Select;
        SqlCommand SQLCMD;
        SqlDataReader SQLDR;
        public UpdateBruger()
        {
            InitializeComponent();
            rettighed.Items.Add("Administrator");
            rettighed.Items.Add("Superbruger");
            filcombobox();
        }

        private void filcombobox()
        {
            SQLCONN.Open();
            Select = "SELECT * FROM Brugere";
            SQLCMD = new SqlCommand(Select, SQLCONN);
            SQLDR = SQLCMD.ExecuteReader();
            while (SQLDR.Read())
            {
                int BrugerID = SQLDR.GetInt32(0);
                Brugercombo.Items.Add(BrugerID);
            }
            SQLCONN.Close();
        }

        private void Brugercombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLCONN.Open();
            Select = "SELECT * FROM Brugere WHERE BrugerID = '"+Brugercombo.SelectedItem+"';";
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

        private void RedigerBruger_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            Updatecommand = new SqlCommand("UPDATE Brugere SET fuldeNavn = @Fuldenavn, brugernavn = @Brugernavn, Kodeord = @Kodeord, gentagKodeord = @GentagKodeord, tildeltRettighed = @rettighed WHERE BrugerID ='" + Brugercombo.SelectedItem + "'", SQLCONN);
            Updatecommand.Prepare();
            Updatecommand.Parameters.Add("@Fuldenavn", SqlDbType.VarChar).Value = Fuldenavn.Text;
            Updatecommand.Parameters.Add("@Brugernavn", SqlDbType.VarChar).Value = Brugernavn.Text;
            Updatecommand.Parameters.Add("@Kodeord", SqlDbType.VarChar).Value = Kodeord.Password;
            Updatecommand.Parameters.Add("@GentagKodeord", SqlDbType.VarChar).Value = GentagKodeord.Password;
            Updatecommand.Parameters.Add("@rettighed", SqlDbType.VarChar).Value = rettighed.SelectedItem;
            Updatecommand.ExecuteNonQuery();
            MessageBox.Show("Bruger opdateret");
            SQLCONN.Close();
  
        }
    }
}
