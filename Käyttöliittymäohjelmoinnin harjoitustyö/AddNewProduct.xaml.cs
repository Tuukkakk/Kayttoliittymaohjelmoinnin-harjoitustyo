using MySqlConnector;
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
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>
    public partial class AddNewProduct : Window {
        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";

        public AddNewProduct() {
            InitializeComponent();

        }
        //Metodi jolla uusi tuote viedään tietokantaan
        public void AddProdClick(object sender, RoutedEventArgs e) {

            AddProduct();
        }  
        
        public void AddProduct() {
            string addNewProduct = "INSERT INTO Products(ProductID, Model, Amount, Price)VALUES(@ProductID, @Model, @Amount, @Price);";

            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(addNewProduct, conn)) {
                    cmd.Parameters.AddWithValue("@ProductID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                    var dr = cmd.ExecuteNonQuery();
                }
            }

            DialogResult = true;
        }
    }
}
