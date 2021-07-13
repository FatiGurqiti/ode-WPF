using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace ODE
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    /// 




    public partial class SignUp : Window
    {
        public bool isOkay = true;
        public SignUp()
        {
            InitializeComponent();
        }




        public void isSpecial(string name)
        {

            Regex rgx = new Regex("[^A-Za-z0-9]");
            bool containsSpecialCharacter = rgx.IsMatch(name);

            if (containsSpecialCharacter)
            {
                isOkay = false;
                System.Windows.MessageBox.Show("This cannot contain special character");

            }

        }

       public bool IfEmailExists(string mail)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.account WHERE mail='"+ mail +"');";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false; }


        }  

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;

            }
            catch
            {
                isOkay = false;
                return false;

            }
        }

        public void isNumberic(string password)
        {
            if (!Regex.IsMatch(password, @"^\d+$"))
            {
                isOkay = false;
                System.Windows.MessageBox.Show("This must be numberic!");
            }
        }

        private void Image_TouchUp(object sender, TouchEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string TextName = nameText.Text;
            string TextMail = mailText.Text;
            string TextPin = pinText.Password.ToString();






            isSpecial(TextName);

            if (!IsValidEmail(TextMail))
            {
                isOkay = false;
                System.Windows.MessageBox.Show("Please enter a valid e-mail");
            }

            isNumberic(TextPin);

            if (isOkay == true)
            {
                string mail = mailText.Text;
                string pin = pinText.Password.ToString();
                int Intpin = Int32.Parse(pin);
                string name = nameText.Text;
                string MyConnection2 = "datasource=localhost;port=3306;username=root";






                if (!IfEmailExists(mail))

                {

                    try
                    {
                        string Query = "insert into ode.account(mail,pin,name) values ('" + mail + "','" + Intpin + "','" + name + "');";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;
                        MyConn2.Open();
                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Your account has been created succesfully");
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
                else
                {
                    MessageBox.Show("This mail has already been taken");
                }

               

            }



        }
    }
}
