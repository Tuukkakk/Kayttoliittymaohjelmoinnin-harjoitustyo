using MySqlConnector;
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

namespace Käyttöliittymäohjelmoinnin_harjoitustyö
{
    /// <summary>
    /// Interaction logic for DeleteProduct.xaml
    /// </summary>
    public partial class DeleteProduct : Window {
        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";
        public DeleteProduct()
        {
            InitializeComponent();
        }

        // Metodi jolla voidaan poistaa tuote kannasta annetun TuoteID:n perusteella
        private void DeleteProductClick(object sender, RoutedEventArgs e) {
            var product = txtProductID.Text;
            var result = MessageBox.Show("Haluatko varmasti poistaa tuotteen?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Billrows WHERE ProductID=@ProductID", conn);
                    cmd.Parameters.AddWithValue("@ProductID", product);
                    var dr = cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("DELETE FROM Products WHERE ProductID=@ProductID", conn);
                    cmd.Parameters.AddWithValue("@ProductID", product);
                    dr = cmd.ExecuteNonQuery();

                }

            }
        }
    }
}
