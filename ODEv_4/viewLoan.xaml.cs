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
    /// Interaction logic for viewLoan.xaml
    /// </summary>
    public partial class viewLoan : Window
    {
        public string Wmail;
        public int ownerOnr;

        public bool IfDNRexists(int DNR, int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.dept WHERE DNR='" + DNR + "' and deptOwnerONR='" + ONR + "' );";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            con.Close();
            if (result == "1") { return true; }
            else { return false; }


        }
        public viewLoan(string mail, int onr)
        {
            InitializeComponent();

            Wmail = mail;
            ownerOnr = onr;

            Window1 w1 = new Window1(mail);

            totalLoanText.Content = w1.LoanTotal(onr);

            addContact ac = new addContact(mail, onr);
            viewDept vd = new viewDept(mail, onr);


            string minDNR = ac.querry("select MIN(DNR) from ode.dept where deptOwnerONR = '" + onr + "'  ");
            string maxDNR = ac.querry("select MAX(DNR) from ode.dept where deptOwnerONR = '" + onr + "'  ");

              if(!String.IsNullOrEmpty(minDNR) && !String.IsNullOrEmpty(maxDNR)){

            int intMinDNR = Int32.Parse(minDNR);
            int intMaxDNR = Int32.Parse(maxDNR);


            string coutRow = "select count(DNR) from ode.dept where deptOwnerONR = '" + onr + "'  ";
            string rowNR = ac.querry(coutRow);
            int intRowNR = Int32.Parse(rowNR);

            Label[] scoresLabelArr = new Label[intRowNR];
            int location = 98;
            int buttonlocation = 98;

            double ammount;
            string currency;
            string strlabel;
            string to;
            string deptOwnerName;


            for (int i = intMinDNR; i <= intMaxDNR; ++i)
            {
                if (IfDNRexists(i, onr))
                {

                    ammount = vd.doublequerry("select ammount from ode.dept where deptOwnerONR = '" + onr + "' and DNR = '" + i + "' ");
                    currency = ac.querry("select currency from ode.dept where deptOwnerONR = '" + onr + "' and DNR = '" + i + "'  ");
                    to = ac.querry("select ONR from ode.dept where deptOwnerONR = '" + onr + "' and DNR = '" + i + "'   ");
                    strlabel = ac.querry("select label from ode.dept where deptOwnerONR = '" + onr + "' and DNR = '" + i + "'   ");

                    int intTo = Int32.Parse(to);
                    Name = ac.querry("select name from ode.account where ONR = '" + intTo + "'  ");

                    Label label = new Label();
                    label.Height = 50;
                    label.Width = 644;
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Top;
                    label.Content = "You owe  " + ammount + " " + currency + " to: " + Name + " with label : " + strlabel;
                    label.Margin = new Thickness(50, location, 0, 0);
                    grid1.Children.Add(label);
                    location += 34;



                }

            }
        }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1(Wmail);
            w1.DataContext = this;
            w1.Show();
            this.Close();
        }
    }
}
