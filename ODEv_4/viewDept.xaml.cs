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
    /// Interaction logic for viewDept.xaml
    /// </summary>
    public partial class viewDept : Window
    {
        public string Wmail;
        public int ownerOnr;




        public double doublequerry(string quer)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            //var stm = "select name from ode.account where ONR = '" + ONR + "';";
            var stm = quer;
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();

            double doubleresult = Convert.ToDouble(result);
            return doubleresult;
        }

        public bool IfDNRexists(int DNR, int ONR)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.dept WHERE DNR='" + DNR + "' and ONR='" + ONR + "' );";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false; }


        }

        public void delete()
        {
            ;


        }

        void button1_Click(object sender, RoutedEventArgs e)
        {
            // delete();


            try
            {
                string content = (sender as Button).Content.ToString();

                char[] cHcontect = content.ToCharArray();
                List<Char> listContect = new List<char>();
                listContect = cHcontect.ToList();
                string strnumber = cHcontect[8].ToString() + cHcontect[9].ToString();
                int number = Int32.Parse(strnumber);

                string MyConnection2 = "datasource=localhost;port=3306;username=root";
                string Query = "delete from ode.dept where ONR='" + ownerOnr + "' AND DNR = '" + number + "'    ;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Dept Setteled");

              

                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
                viewDept vd = new viewDept(Wmail, ownerOnr);
                vd.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }
        public viewDept(string mail, int onr)
        {
            InitializeComponent();


            Wmail = mail;
            ownerOnr = onr;

            Window1 w1 = new Window1(mail);
            totalDeptText.Content = w1.DeptTotal(onr);


            addContact ac = new addContact(mail, onr);

            string minDNR = ac.querry("select MIN(DNR) from ode.dept where ONR = '" + onr + "'  ");
            string maxDNR = ac.querry("select MAX(DNR) from ode.dept where ONR = '" + onr + "'  ");

            int intMinDNR = Int32.Parse(minDNR);
            int intMaxDNR = Int32.Parse(maxDNR);


            string coutRow = "select count(DNR) from ode.dept where ONR = '" + onr + "'  ";
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

                    ammount = doublequerry("select ammount from ode.dept where ONR = '" + onr + "' and DNR = '" + i + "' ");
                    currency = ac.querry("select currency from ode.dept where ONR = '" + onr + "' and DNR = '" + i + "'  ");
                    to = ac.querry("select deptOwnerONR from ode.dept where ONR = '" + onr + "' and DNR = '" + i + "'   ");
                    strlabel = ac.querry("select label from ode.dept where ONR = '" + onr + "' and DNR = '" + i + "'   ");

                    int intTo = Int32.Parse(to);
                    deptOwnerName = ac.querry("select name from ode.account where ONR = '" + intTo + "'  ");

                    Label label = new Label();
                    label.Height = 50;
                    label.Width = 644;
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Top;
                    label.Content = deptOwnerName + "\t owes you " + ammount + " " + currency + " with label of : " + strlabel;
                    label.Margin = new Thickness(50, location, 0, 0);
                    grid1.Children.Add(label);
                    location += 34;

                    Button button = new Button();
                    button.Content = "Delete  " + i.ToString() + "  ";
                    button.HorizontalAlignment = HorizontalAlignment.Left; ;
                    button.VerticalAlignment = VerticalAlignment.Top;
                    button.Width = 75;
                    button.Margin = new Thickness(600, buttonlocation, 0, 0);
                    button.AddHandler(Button.ClickEvent, new RoutedEventHandler(button1_Click));
                    grid1.Children.Add(button);
                    buttonlocation += 34;


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
