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

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for Slet.xaml
    /// </summary>
    public partial class Slet : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand DeleteCommand;
        SqlCommand SQLCMD;
        SqlDataReader SQLDTA;
        string Select;
        public Slet()
        {
            InitializeComponent();
            FillComboboxWithID();
        }


        private void FillComboboxWithID()
        {
            SQLCONN.Open();                             //Opening the Database
            Select = "SELECT * FROM Rang";              //SELECT FROM the table Rang
            SQLCMD = new SqlCommand(Select, SQLCONN);   //Command

            SQLDTA = SQLCMD.ExecuteReader();            //Datareader, who wil read
            while (SQLDTA.Read())                       //While loop, which will looping t9he data through
            {
                int LandID = SQLDTA.GetInt32(3);        //Making the variable LandID, which wil get the index of Land
                IDBox.Items.Add(LandID);                //And then add it to the combobox
            }
            SQLCONN.Close();                            //Closing the database
        }
        private void IDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() != typeof(ComboBox)) return;
            ComboBox IDBox = (ComboBox)sender;

            SQLCONN.Open();                                 //Opening the database
            Select = "select Land.Land,Land.Verdensdel, Rang.Rang, Rang.Fodselsrate, Rang.LandID from Land inner join Rang on Rang.LandID = Land.LandID where Land.LandID = @ID order by Rang"; //Selecting the different values
            SQLCMD = new SqlCommand(Select, SQLCONN);       //Command
            SQLCMD.Parameters.AddWithValue("@ID", IDBox.SelectedValue); //Adding value/reading
            SQLDTA = SQLCMD.ExecuteReader();                            //Definding executive reader

            while (SQLDTA.Read())                               //Reading the value
            {
                string land = SQLDTA.GetString(0);              //Getting the first index (land)
                string verdensdel = SQLDTA.GetString(1);        //Getting the second index (verdensdel)
                string Rang = SQLDTA.GetInt32(2).ToString();    //Getting the third index (rang)
                string fr = SQLDTA.GetDecimal(3).ToString();    //Getting the fouth index (fodselsrate)

                LandBox.Text = land;                            //Adding the value into the textboxes
                VerdensDelComboBox.Text = verdensdel;           //Adding the value into the textboxes
                RangeBox.Text = Rang;                           //Adding the value into the textboxes
                FodselsrateBox.Text = fr;                       //Adding the value into the textboxes
            }
            SQLCONN.Close();                                    //Closing the database
        }

        private void Deletebutton_Click(object sender, RoutedEventArgs e) 
        {
            SQLCONN.Open();                    //Opening the database
            DeleteCommand = new SqlCommand("DELETE FROM Rang where Rang.LandID ='" + IDBox.SelectedValue + "'", SQLCONN);  //Delete from the Rang tabel, when the LandID matches with the selected ID
            DeleteCommand.ExecuteNonQuery();    //Non Query    
            SQLCONN.Close();                    //Closing the database

            LandBox.Text = "";                  //Make the box empty after delete
            VerdensDelComboBox.Text = "";       //Make the box empty after delete
            RangeBox.Text = "";                 //Make the box empty after delete
            FodselsrateBox.Text = "";           //Make the box empty after delete

            MessageBox.Show("Data slettet");    //MessageBox  to user
        }
    }
}
