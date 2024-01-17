using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Käyttöliittymäohjelmoinnin_harjoitustyö
{
    /// <summary>
    /// Interaction logic for AddNewCustomer.xaml
    /// </summary>
    public partial class AddNewCustomer : Window
    {
        public AddNewCustomer()
        {
            
            InitializeComponent();

            this.DataContext = new Customer();
        }
        //Metodi jolla uusi asiakas viedään tietokantaan
        private void AddCusClick(object sender, RoutedEventArgs e)
        {
            var customer = (Customer)this.DataContext;

            var repo = new DiscRepository();
            repo.AddCustomer(customer);

            DialogResult = true;
        }
    }


}

