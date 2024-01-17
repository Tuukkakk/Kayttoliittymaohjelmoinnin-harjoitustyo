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

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for ProductsEdit.xaml
    /// </summary>
    public partial class ProductsEdit : Window {
        public ProductsEdit() {
            InitializeComponent();

            this.DataContext = new Product();
        }
        // Metodi jolla voidaan muokata tuotetta
        private void EditProdClick(object sender, RoutedEventArgs e) {
            var product = (Product)this.DataContext;
           

            var repo = new DiscRepository();
            repo.EditProduct(product);

            DialogResult = true;
        }


    }
}
