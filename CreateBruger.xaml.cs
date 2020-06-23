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
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Windows.Automation.Peers;
using System.Text.RegularExpressions;

namespace GeoTema
{
    /// <summary>
    /// Interaction logic for CreateBruger.xaml
    /// </summary>
    public partial class CreateBruger : Window
    {
        SqlConnection SQLCONN = new SqlConnection(@"Data Source = 10.0.5.104, 1433; Network Library = DBMSSOCN; Initial Catalog = GeoTema; User ID = Myuser; Password=password;");
        SqlCommand InsertCommand;
        public CreateBruger()
        {
            InitializeComponent();
            rettighed.Items.Add("Administrator");
            rettighed.Items.Add("Superbruger");
    
        }

        private void opretBruger_Click(object sender, RoutedEventArgs e)
        {
    
     
            if (Fuldenavn.Text == "")
            {
                MessageBox.Show("Der skal indtastes fuldt navn");
            }

            else if (Brugernavn.Text == "")
            {
                MessageBox.Show("Der skal indtastes et brugernavn");
            }

            else if (Kodeord.Password == "" || GentagKodeord.Password == "" || Kodeord.Password != GentagKodeord.Password)
            {
                MessageBox.Show("Koderne matcher ikke hinanden");
            }
            else if (rettighed.SelectedItem == null)
            {
                MessageBox.Show("Der skal tildels en rettighed til bruger");
            }
            else
            {
                SQLCONN.Open();
                InsertCommand = new SqlCommand("INSERT INTO Brugere VALUES(@Fuldenavn, @Brugernavn, @Kodeord, @GentagKodeord, @rettighed)", SQLCONN);
                InsertCommand.Prepare();
                InsertCommand.Parameters.AddWithValue("@Fuldenavn", Fuldenavn.Text);
                InsertCommand.Parameters.AddWithValue("@Brugernavn", Brugernavn.Text);
                InsertCommand.Parameters.AddWithValue("@Kodeord", Kodeord.Password);
                InsertCommand.Parameters.AddWithValue("@GentagKodeord", GentagKodeord.Password);
                InsertCommand.Parameters.AddWithValue("@rettighed", rettighed.SelectedItem);
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Bruger oprettet");
                SQLCONN.Close();
            }
        }

        // Function To test for Alphabets.   
        public bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }


    }
}
