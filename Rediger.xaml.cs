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
using System.Data;
using System.IO;

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for Rediger.xaml
    /// </summary>
    public partial class Rediger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand SQLCMD;
        SqlCommand Update;
        SqlDataReader SQLDTA;
        string Select;
        public Rediger()
        {
            InitializeComponent();
            FillComboboxWithID();
            StreamReader SR = new StreamReader(@"C:\Users\mupa\source\repos\GeoTema\GeoTema\lande\Lande.txt", System.Text.Encoding.Default);
            string line = SR.ReadLine();
            while (line != null)
            {
                LandBox.Items.Add(line);
                line = SR.ReadLine();
            }


            String[] verdensdele = new string[]
            {
                "Asien",
                "Afrika",
                "Nordamerika",
                "Sydamerika",
                "Antarktis",
                "Europa",
                "Oceanien",
                "Asien/Europa"
            };
            for (int i = 0; i < verdensdele.Length; i++)
            {
                VerdensDelComboBox.Items.Add(verdensdele[i]);
            }
        }
        private void FillComboboxWithID()
        {
            SQLCONN.Open();
            Select = "SELECT * FROM Rang";
            SQLCMD = new SqlCommand(Select, SQLCONN);

            SQLDTA = SQLCMD.ExecuteReader();
            while (SQLDTA.Read())
            {
                int LandID = SQLDTA.GetInt32(3);
                IDBox.Items.Add(LandID);
            }
            SQLCONN.Close();
        }

        private void IDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() != typeof(ComboBox)) return;
            ComboBox IDBox = (ComboBox)sender;

            SQLCONN.Open();                                 //Opening the database
            Select = "select Land.Land,Land.Verdensdel, Rang.Rang, Rang.Fodselsrate, Rang.LandID from Land inner join Rang on Rang.LandID = Land.LandID where Land.LandID = @ID order by Rang"; //Selecting the different values
            SQLCMD = new SqlCommand(Select, SQLCONN);       //Command
            SQLCMD.Parameters.AddWithValue("@ID", IDBox.SelectedItem); //Adding value/reading
            SQLDTA = SQLCMD.ExecuteReader();                            //Definding executive reader

            while (SQLDTA.Read())                               //Reading the value
            {
                string land = SQLDTA.GetString(0);              //Getting the first index (land)
                string verdensdel = SQLDTA.GetString(1);        //Getting the second index (verdensdel)
                string Rang = SQLDTA.GetInt32(2).ToString();    //Getting the third index (rang)
                string fr = SQLDTA.GetDecimal(3).ToString();    //Getting the fouth index (fodselsrate)

                LandBox.SelectedItem = land;                            //Adding the value into the textboxes
                VerdensDelComboBox.SelectedItem = verdensdel;           //Adding the value into the textboxes
                RangeBox.Text = Rang;                                   //Adding the value into the textboxes
                FodselsrateBox.Text = fr;                               //Adding the value into the textboxes
            }
            SQLCONN.Close();                                        //Closing the database
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLCONN.Open();
            Update = new SqlCommand("UPDATE Land SET Land.Land =  @LandComboBox, Land.Verdensdel = @VerdensDelComboBox WHERE Land.LandID ='" + IDBox.SelectedItem + "'", SQLCONN);
            Update.Parameters.Add("@LandComboBox", SqlDbType.VarChar).Value = LandBox.SelectedItem;
            Update.Parameters.Add("@VerdensDelComboBox", SqlDbType.VarChar).Value = VerdensDelComboBox.SelectedItem;
            Update.ExecuteNonQuery();
            SQLCONN.Close();


            SQLCONN.Open();
            Update = new SqlCommand("UPDATE Rang SET Rang.Rang = @RangeBox, Rang.Fodselsrate = @FodselsrateBox WHERE Rang.LandID = '" + IDBox.SelectedItem + "'", SQLCONN);
            Update.Parameters.Add("@RangeBox", SqlDbType.Int).Value = RangeBox.Text;
            Update.Parameters.Add("@FodselsrateBox", SqlDbType.Decimal).Value = FodselsrateBox.Text;
            Update.ExecuteNonQuery();
            Update.Parameters.Clear();
            MessageBox.Show("Data opdateret");
            SQLCONN.Close();
        }
    }
}
