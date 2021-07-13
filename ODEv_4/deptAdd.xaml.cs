using MySql.Data.MySqlClient;
using ODE;
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
    /// Interaction logic for deptAdd.xaml
    /// </summary>
    public partial class deptAdd : Window
    {
        public string Wmail;
        public int ownerOnr;

        public int ContantONRquerry(int ONR, int LNR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select contactONR from ode.contact where ONR= '" + ONR + "' AND LNR = '" + LNR + "';";
            var cmd = new MySqlCommand(stm, con);


            var result = cmd.ExecuteScalar().ToString();
            int intresult = Int32.Parse(result);

            return intresult;

            
        }
      

        public deptAdd(string mail, int onr)
        {
            InitializeComponent();
            Wmail = mail;
            ownerOnr = onr;

            Window1 w1 = new Window1(mail);

            //int maxLNR = w1.MaxLNR(onr);
            //int minLNR = w1.MinLNR(onr);
            //for (int i = minLNR = 0; i <= maxLNR; ++i)
            //{
            //    // contact.Items.Add(w1.ContantONRquerry(onr, i).ToString());
            //}

            if (w1.IfContactExists(onr))
            {

                int maxLNR = w1.MaxLNR(onr);
                int minLNR = w1.MinLNR(onr);



                for (int i = minLNR; i <= maxLNR; ++i)
                {

                    if (w1.IfLNRexists(i, onr))
                    {
                        onrText.Items.Add(w1.ContantONRquerry(onr, i).ToString() );
                    }
                }

            }
        }
        public int contantONRquerry(int ONR, int LNR)
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

        public void addDept(string ammount,string label,string currency,int ONR,int deptOwnerONR)
        {
            try
            {


                string MyConnection2 = "datasource=localhost;port=3306;username=root";
                string Query = "insert into ode.dept(ammount,label,currency,ONR,deptOwnerONR) values ('" + ammount + "','" + label + "','" + currency + "','" + ONR + "','" + deptOwnerONR + "');";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Dept added sucessfully");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1(Wmail);
            w1.DataContext = this;
            w1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignUp su = new SignUp();

            string ammount = amountText.Text;
            string label = labelText.Text;
            string currency = currencyText.Text;
            string onr = onrText.Text;
            

            su.isNumberic(ammount);
            su.isSpecial(ammount);
            su.isNumberic(onr);


            if (su.isOkay)
            {
                int intonr = Int32.Parse(onr);
                addDept(ammount, label, currency, ownerOnr, intonr);
            }
            else
            { MessageBox.Show("Oops!"); }
        }
           
    }
}
