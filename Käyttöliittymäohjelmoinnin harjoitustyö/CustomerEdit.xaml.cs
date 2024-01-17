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

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for CustomerEdit.xaml
    /// </summary>
    public partial class CustomerEdit : Window {
        public CustomerEdit() {
            InitializeComponent();

            this.DataContext = new Customer();
        }
        //Tapahtumankuuntelija asiakkaan tietojen muokkaukselle
        private void EditCustomerClick(object sender, RoutedEventArgs e) {
            var customer = (Customer)this.DataContext;


            var repo = new DiscRepository();
            repo.EditCustomer(customer);

            DialogResult = true;
        }
    }
}
