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
    /// Interaction logic for loanAdd.xaml
    /// </summary>
    public partial class loanAdd : Window
    {
        public string Wmail;
        public loanAdd(string mail)
        {
            InitializeComponent();

            Wmail = mail;
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
