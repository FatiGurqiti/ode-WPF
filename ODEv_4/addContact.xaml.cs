using MySql.Data.MySqlClient;
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

namespace ODEv_4
{
    /// <summary>
    /// Interaction logic for addContact.xaml
    /// </summary>
    public partial class addContact : Window
    {

        public string Wmail;
        public int ownerOnr;
        public addContact(string mail,int Owneronr)
        {
            InitializeComponent();

            Wmail = mail;
            ownerOnr = Owneronr;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1(Wmail);
            w1.DataContext = this;
            w1.Show();
            this.Close();
        }
        public bool IfContactExists(int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.account WHERE ONR='" + ONR + "');";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false; }


        }

        public string querry(string quer)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = quer;
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

            con.Close();

            return result;
        }


        public void querryinsert(string name,int ownerOnr,int intONR)
        {
            
           
            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = "datasource=localhost;port=3306;username=root";
                //This is my insert query in which i am taking input from the user through windows forms  
                string Query = "insert into ode.contact(name,ONR,contactONR) values('" + name + "','" + ownerOnr + "','" + intONR + "');";
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                //This is command class which will handle the query and connection object.  
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string strONR = contactONR.Text;
            if (strONR != null)
            {

                try
                {

                    int intONR = Int32.Parse(strONR);

                    if (IfContactExists(intONR))
                    {

                      string name =  querry("select name from ode.account where ONR = '" + intONR + "';");

                        MessageBox.Show(name + " has been added to your contact.Please check it on the Contact section");

                        querryinsert(name, ownerOnr, intONR);
                       
                    }
                    else { MessageBox.Show("This person doesn't exist"); }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else {

                MessageBox.Show("Please enter a value");
            }




            
        }
    }
}
