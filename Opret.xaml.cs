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
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace GeoTema
{
    /// <summary>
    /// Interaction logic for Opret.xaml
    /// </summary>
    public partial class Opret : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand InsertCommand;
        SqlCommand SelectCommand;
        public Opret()
        {
            InitializeComponent();
            StreamReader SR = new StreamReader(@"C:\Users\mupa\source\repos\GeoTema\GeoTema\lande\Lande.txt", System.Text.Encoding.Default);
            string line = SR.ReadLine();
            while (line != null)
            {
                LandComboBox.Items.Add(line);
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

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            int Parsed;
            if (RangeBox.Text == ""|| FodselsrateBox.Text == "" || LandComboBox.SelectedItem == null || VerdensDelComboBox == null) //Checking if the textboxes and combobox if empty
            {
                MessageBox.Show("Alle felter skal udfyldes");                                                                       //MessageBox to user
            }
            else if (!int.TryParse(RangeBox.Text, out Parsed) || !int.TryParse(FodselsrateBox.Text, out Parsed))                   //Checking if the last two boxes is with int vaalue
            {
                MessageBox.Show("Indtast venligst kun numeriske tegn");                                                             //MessageBox to user
            }
            else
            {
                SQLCONN.Open();                                                                                                 //Opening the Database                                                                                                                     
                InsertCommand = new SqlCommand("INSERT INTO Land VALUES (@LandComboBox, @VerdensDelComboBox)", SQLCONN);        //Definding which values is going to be inserted
                InsertCommand.Prepare();                                                                                        //Preparing
                InsertCommand.Parameters.AddWithValue("@LandComboBox", LandComboBox.SelectedItem);                              //Parameters.AddWithValue-function and calling the respective textboxes, which is ComboBox here
                InsertCommand.Parameters.AddWithValue("@VerdensDelComboBox", VerdensDelComboBox.SelectedItem);                  //Parameters.AddWithValue-function and calling the respective textboxes, which is ComboBox here
                InsertCommand.ExecuteNonQuery();                                                                                //SQL-transaction
                SQLCONN.Close();                                                                                                //Closing the datbase

                SQLCONN.Open();                                                                                                 //Opening the database again for SELECT-statement
                SelectCommand = new SqlCommand("SELECT * FROM Land WHERE Land=@LandComboBox", SQLCONN);                         //SELECT FROM Land
                SelectCommand.Prepare();                                                                                        //Preparing
                SelectCommand.Parameters.AddWithValue("@LandComboBox", LandComboBox.SelectedItem);                              //Parameters.AddWithValue-function and calling the respective textboxes, which is ComboBox here
                SqlDataReader dataReader = SelectCommand.ExecuteReader();                                                       //DataReader, which will read, what we have selected
                int LandID = 0;                                                                                                 //Definding LandID
                while (dataReader.Read())                                                                                        //A while loop, which wil read
                {
                    LandID = dataReader.GetInt32(0);                                                                            //Reading the first position, which is LandID
                    break;                                                                                                      //Breaking the loop adfter getting the element
                }
                SQLCONN.Close();                                                                                                //Closing the Database

                SQLCONN.Open();                                                                                                 //Opening the database for Insert Into Rang
                InsertCommand = new SqlCommand("INSERT INTO Rang VALUES(@RangeBox, @FodselsrateBox, @LandID)", SQLCONN);        //Definding which values is going to be inserted     
                InsertCommand.Parameters.AddWithValue("@RangeBox", RangeBox.Text);                                              //Parameters.AddWithValue-function and calling the respective textboxe´s
                InsertCommand.Parameters.AddWithValue("@FodselsrateBox", FodselsrateBox.Text);                                  //Parameters.AddWithValue-function and calling the respective textboxes, which is ComboBox here
                InsertCommand.Parameters.AddWithValue("@LandID", LandID);                                                       //Parameters.AddWithValue-function and calling the respective textbox and the LandID Wwich was made before          
                InsertCommand.ExecuteNonQuery();                                                                                //SQL transactiom

                MessageBox.Show("Data registreret");                                                                   //Message to the user
                SQLCONN.Close();                                                                                                //Closing thee database
            }
        }
    }
}
