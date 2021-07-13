using System;
using ODEv_4;
using System.Collections.Generic;
using System.Configuration;
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
using MySql.Data.MySqlClient;
using ODE;

namespace ODEv_4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        //List<account> GetAccounts = new List<account>();

        public string W1mail;
        public int onr;

        public string querryGo(string mail)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select name from ode.account where mail = '" + mail + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

            return result;}
        public string ONRquerry(string mail)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select ONR from ode.account where mail = '" + mail + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

            return result;
        }
        public bool IfLNRexists(int LNR, int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.contact WHERE LNR='" + LNR + "' and ONR='" + ONR + "' );";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false; }
        }
        public string Contantquerry(int ONR,int LNR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select name from ode.contact where ONR = '" + ONR + "' and LNR = '" + LNR +"' ;";
            var cmd = new MySqlCommand(stm, con);

            
            var result = cmd.ExecuteScalar().ToString();


            return result;
        }

            public int ContantONRquerry(int ONR, int LNR)
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root";
                var con = new MySqlConnection(MyConnection2);
                con.Open();

                var stm = "select contactONR from ode.contact where ONR = '" + ONR + "'  and LNR = '" + LNR + "' ;";

                var cmd = new MySqlCommand(stm, con);
                cmd.ExecuteNonQuery();

                var result = cmd.ExecuteScalar().ToString();
                int Intresult = Int32.Parse(result);

                return Intresult;

            }


        public int MinLNR(int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select MIN(LNR) from ode.contact where ONR = '" + ONR + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            int Intresult = Int32.Parse(result);

            return Intresult;
        }

        public int MaxLNR(int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select MAX(LNR) from ode.contact where ONR = '" + ONR + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

            int Intresult = Int32.Parse(result);

            return Intresult;
        }

        public string LoanTotal(int ONR) 
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select SUM(ammount) from ode.dept where deptOwnerONR = '" + ONR + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();


            return result;
        }

        public string DeptTotal(int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select SUM(ammount) from ode.dept where ONR = '" + ONR + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

           // int Intresult = Int32.Parse(result);

            return result;
        }
        public bool IfContactExists(int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.contact WHERE ONR= '" + ONR + "');";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false; }


        }

        public Window1(string mail)
        {
            InitializeComponent();


            //var MainWindow = this.DataContext;
            W1mail = mail;

            string user_name = querryGo(mail);
            User_Name.Content = user_name;
            string strONR = ONRquerry(mail);
            int IntONR = Int32.Parse(strONR);
            onr = IntONR;
            onrText.Content = IntONR;



            if (IfContactExists(IntONR)) {

            int maxLNR = MaxLNR(IntONR);
            int minLNR = MinLNR(IntONR);


              
                for (int i = minLNR; i <= maxLNR; ++i) {

                    if (IfLNRexists(i,IntONR)) { 
                     Contacts.Items.Add(ContantONRquerry(IntONR, i).ToString() + " : "+ Contantquerry(IntONR, i).ToString());
                    }
                }

            }
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();

        }

        private void Contacts_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Contacts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            addContact ac = new addContact(W1mail,onr);
            ac.DataContext = this;
            ac.Show();
            this.Close();


         
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            deptAdd da = new deptAdd(W1mail,onr);
            da.DataContext = this;
            da.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            loanAdd la = new loanAdd(W1mail);
            la.DataContext = this;
            la.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            viewDept vd = new viewDept(W1mail,onr);
            vd.DataContext = this;
            vd.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            viewLoan vl = new viewLoan(W1mail,onr);
            vl.DataContext = this;
            vl.Show();
            this.Close();
        }
    }
}
