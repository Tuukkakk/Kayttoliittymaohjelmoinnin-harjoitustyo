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
    /// Interaction logic for DeleteCustomer.xaml
    /// </summary>
    
    public partial class DeleteCustomer : Window {
        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";
        public DeleteCustomer()
        {
            InitializeComponent();
        }
        // Metodi jolla voidaan poistaa asiakas kannasta annetun AsiakasID:n perusteella
        private void DeleteCustomerClick(object sender, RoutedEventArgs e) {
            var customer = txtCustomerID.Text;
            var result = MessageBox.Show("Haluatko varmasti poistaa asiakkaan?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM billRows WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID)", conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customer);
                    var dr = cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("DELETE FROM Orders WHERE CustomerID=@CustomerID", conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customer);
                    dr = cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("DELETE FROM Customers WHERE CustomerID=@CustomerID", conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customer);
                    dr = cmd.ExecuteNonQuery();

                }

            }
        }
    }
}
