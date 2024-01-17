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

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for DeleteOrder.xaml
    /// </summary>
    public partial class DeleteOrder : Window {

        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";

        public DeleteOrder() {
            InitializeComponent();
        }

        // Metodi jolla voidaan poistaa tilaus kannasta annetun TilausID:n perusteella
        private void DeleteOrderClick(object sender, RoutedEventArgs e) {
            var order = txtOrderID.Text;
            var result = MessageBox.Show("Haluatko varmasti poistaa tilauksen?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Billrows WHERE OrderID=@OrderID", conn);
                    cmd.Parameters.AddWithValue("@OrderID", order);
                    var dr = cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("DELETE FROM Orders WHERE OrderID=@OrderID", conn);
                    cmd.Parameters.AddWithValue("@OrderID", order);
                    dr = cmd.ExecuteNonQuery();

                }

            }

        }
    }
}
