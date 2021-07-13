using MySql.Data.MySqlClient;
using ODEv_4;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ODE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 




    public partial class MainWindow : Window
    {
        public string mail;
        public MainWindow()
        {
            InitializeComponent();

        }

      

       

       

        private void openMainScreen(string value)
        {
            Window1 w1 = new Window1(value);
            w1.DataContext = this;
            w1.Show();
            this.Close();
        }

        private bool ifPinCorrect(string mail, int pin)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select pin from ode.account where mail = '" + mail + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            int Intresult = Int32.Parse(result);
            con.Close();
            if (Intresult == pin) return true;
           
            else return false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnClickSignUpPage(object sender, RoutedEventArgs e)
        {
            SignUp su = new SignUp();
            //su.Show();
            su.ShowDialog();
            this.Close();
        }
        

        




        public void Button_Click(object sender, RoutedEventArgs e)
        {
            
            mail = mailText.Text;
            string pin = pinText.Password.ToString();



            try
            {
                int Intpin = Int32.Parse(pin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





            try
            {

                SignUp SU = new SignUp();
                if (SU.IfEmailExists(mail))

                {

                    if (ifPinCorrect(mail, Int32.Parse(pin))) openMainScreen(mail);
                    else MessageBox.Show("Wrong mail adress or pin");

                }
                //log in
                else
                {
                    MessageBox.Show("Wrong mail adress or pin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


       

        
    }
}
