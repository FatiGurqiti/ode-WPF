# odeWPF
Öde app is a WPF application developed for the .NET class of VSITE University

#Technologies

- C#
- WPF
- MySQL
- XML

#Usage  of app

![Image of SignIn](https://github.com/FatiGurqiti/odeWPF/blob/master/img/1.bmp)

On the opening page of the app you should see the log in page.If you don't have an account you can simply click on 'Sign up?' and register on the opened page.
_E-mail_ will not accept any given input if it isn't on correct e-mail format, _Pin_ will not accept any character or special symbol and Name will not accept numbers or special characters.

These are being checked by simple algorithms.


```


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
        
```


If users enteres all the informations correctly the program will make a SQL querry to see if the e-mail is already taken.

```

  public bool IfEmailExists(string mail)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "SELECT EXISTS(SELECT * FROM ode.account WHERE mail='"+ mail +"');";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            if (result == "1") { return true; }
            else { return false;
                con.Close();
            }

        } 
```

Once all the checks are done successfully,program takes all the informations from inputs and makes a SQL input

```
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

```

Once the Sign up process done,you may continue to the main page.On click event of the _Sign in_ button.It checks the same things as _Sign Up_.
If all is correct it has additional checks too see if the given mail and pin matches.

```

 private bool ifPinCorrect(string mail, int pin)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root";
            var con = new MySqlConnection(MyConnection2);
            con.Open();

            var stm = "select pin from ode.account where mail = '" + mail + "';";
            var cmd = new MySqlCommand(stm, con);

            var result = cmd.ExecuteScalar().ToString();
            int Intresult = Int32.Parse(result);

            if (Intresult == pin) return true;

            else return false;
        }
        
        ```
        
        If so,it directs user to the main page
        
        ```
        
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
            
            ```
