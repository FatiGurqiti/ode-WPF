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

Once logged in,you can see your **ONR** number on the right corner of the screen under your name.This is needed to add people to your contact which you can view on the left side of the screen.

To add people;simply go to _Add Contact_ and enter the **ONR** of the person you want to add.
_ADD Contact_ takes _mail_ and _Owneronr_ in it's constructor.

```
 public string Wmail;
        public int ownerOnr;
        public addContact(string mail,int Owneronr)
        {
            InitializeComponent();

            Wmail = mail;
            ownerOnr = Owneronr;
        }
        
 ```
 These are sent from the mainpage,this way you can get user's *ONR* inorder to do the _SQL input_.Program will you show you and error message if the person doesn't exist in the database.Otherwise,the process is an _SQL input_.
 
 ```
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
 ```

_querry_ simplyfies the the querry.With this you can easily do your queey inline.

```
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
```

However,there was crashes and unstability when it comes to _SQL insert_ action.So it was a mandatory to use another method.

```
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

```

The reason of this proccess is to make it easier for user to enter informations on the _add debt_ page

[!Image of AddDebt] (https://github.com/FatiGurqiti/odeWPF/blob/master/img/2.bmp)

