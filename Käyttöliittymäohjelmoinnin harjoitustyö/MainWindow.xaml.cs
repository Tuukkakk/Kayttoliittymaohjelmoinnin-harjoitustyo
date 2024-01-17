using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        //Luodaan tarvittavat ominaisuudet
        public DiscRepository repo;

        private MySqlDataAdapter DataFromProducts;
        private DataTable ProductsTable;

        private MySqlDataAdapter DataFromCustomers;
        private DataTable CustomerTable;

        private MySqlDataAdapter DataFromOrders;
        private DataTable OrdersTable;

        private MySqlDataAdapter DataFromBillrows;
        private DataTable BillrowsTable;

        private DataTable DataGridView;

        private int RowCount;
        private int OrderCount;

        private const string LocalWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";
        public ObservableCollection<OrderView> Orders { get; set; }

        private List<string> imagePaths;  
        




        //Kutsutaan discrepository luokan metodeja
        public MainWindow() {
            InitializeComponent();

            imagePaths = new List<string>();
            imagePaths.Add(@"TÄMÄ PITÄÄ MUUTTAA\cs\Käyttöliittymäohjelmoinnin harjoitustyö\Käyttöliittymäohjelmoinnin harjoitustyö\kiekko.jpg");
            imagePaths.Add(@"TÄMÄ PITÄÄ MUUTTAA\cs\Käyttöliittymäohjelmoinnin harjoitustyö\Käyttöliittymäohjelmoinnin harjoitustyö\kiekko2.jpg");
            imagePaths.Add(@"TÄMÄ PITÄÄ MUUTTAA\cs\Käyttöliittymäohjelmoinnin harjoitustyö\Käyttöliittymäohjelmoinnin harjoitustyö\kiekko3.jpg");


            repo = new DiscRepository();

            repo.CreateDiscDb();

            repo.CreateTables();

            repo.FillDatabase();

            //Haetaan data tuote taulusta ja asetetaan data näkymään tuotenäkymän datagridin datacontextissa
            DataFromProducts = new MySqlDataAdapter("SELECT * FROM Products", LocalWithDb);
            ProductsTable = new DataTable();
            DataFromProducts.Fill(ProductsTable);
            dgProducts.ItemsSource = ProductsTable.DefaultView;

            //Haetaan data asiakkaat taulusta ja asetetaan data näkymään asiakasnäkymän datagridin datacontextissa
            DataFromCustomers = new MySqlDataAdapter("SELECT * FROM Customers", LocalWithDb);
            CustomerTable = new DataTable();
            DataFromCustomers.Fill(CustomerTable);
            dgCustomers.ItemsSource = CustomerTable.DefaultView;

            GetOrderView();

            //Haetaan data tilaukset taulusta ja asetetaan data näkymään tilausnäkymän datagridin datacontextissa
            DataFromOrders = new MySqlDataAdapter("SELECT * FROM Orders INNER JOIN Customers ON orders.CustomerID=Customers.CustomerID", LocalWithDb);
            OrdersTable = new DataTable();
            DataFromOrders.Fill(OrdersTable);
            dgOrders.ItemsSource = OrdersTable.DefaultView;


        }

        //Tapahtumakuuntelijoitten metodit
        private void EditCustomer(object sender, RoutedEventArgs e) {
            var customerView = new CustomerEdit();
            customerView.ShowDialog();
        }

        private void EditProduct(object sender, RoutedEventArgs e) {

            var prodEditView = new ProductsEdit();
            prodEditView.ShowDialog();
        }

        private void OpenStoreInfo(object sender, RoutedEventArgs e) {
            var storeView = new StoreInfo();
            storeView.ShowDialog();
        }

        private void VersionClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Version 1.0 @Tuukka Kuittinen");
        }

        private void BringProducts(object sender, RoutedEventArgs e) {

            DataFromProducts = new MySqlDataAdapter("SELECT * FROM Products", LocalWithDb);
            ProductsTable = new DataTable();
            DataFromProducts.Fill(ProductsTable);
            dgProducts.ItemsSource = ProductsTable.DefaultView;


        }

        private void BringCus(object sender, RoutedEventArgs e) {
            DataFromCustomers = new MySqlDataAdapter("SELECT * FROM Customers", LocalWithDb);
            CustomerTable = new DataTable();
            DataFromCustomers.Fill(CustomerTable);
            dgCustomers.ItemsSource = CustomerTable.DefaultView;
        }

        private void AddProduct(object sender, RoutedEventArgs e) {
            var newProductAdd = new AddNewProduct();
            newProductAdd.ShowDialog();
        }

        private void AddOrder(object sender, RoutedEventArgs e) {
            var newOrderAdd = new AddNewOrder();
            newOrderAdd.ShowDialog();
        }

        private void AddCustomer(object sender, RoutedEventArgs e) {
            var newCustomerAdd = new AddNewCustomer();
            newCustomerAdd.ShowDialog();
        }

        private void EditOrder(object sender, RoutedEventArgs e) {
            var newEditOrder = new OrderEdit();
            newEditOrder.ShowDialog();

        }

        private void BringOrders(object sender, RoutedEventArgs e) {
            DataFromOrders = new MySqlDataAdapter("SELECT * FROM Orders INNER JOIN customers ON orders.CustomerID=customers.CustomerID", LocalWithDb);
            OrdersTable = new DataTable();
            DataFromOrders.Fill(OrdersTable);
            dgOrders.ItemsSource = OrdersTable.DefaultView;
        }


        private void openImagesClick(object sender, RoutedEventArgs e) {
            
            ImageView iv = new ImageView(imagePaths);
            iv.Show();
        }

        private void DeleteOrderClick(object sender, RoutedEventArgs e) {
            var deleteOrder = new DeleteOrder();
            deleteOrder.ShowDialog();
        }

        private void DeleteCustomerClick(object sender, RoutedEventArgs e) {
            var deleteCustomer = new DeleteCustomer();
            deleteCustomer.ShowDialog();
        }

        private void DeleteProductClick(object sender, RoutedEventArgs e) {
            var deleteProduct = new DeleteProduct();
            deleteProduct.ShowDialog();
        }
        // Tällä metodilla tilausnäkymä saadaan aikaan. Dataa hateaan ja asetetaan data gridille
        public void GetOrderView() {

            DataFromBillrows = new MySqlDataAdapter("SELECT ID FROM Billrows", LocalWithDb);

            BillrowsTable = new DataTable();
            DataGridView = new DataTable();

            DataFromBillrows.Fill(BillrowsTable);

            DataColumn colOrderID = new DataColumn("OrderID", typeof(int));
            DataColumn colOrderDate = new DataColumn("OrderDate", typeof(string));
            DataColumn colName = new DataColumn("Name", typeof(string));
            DataColumn colTotal= new DataColumn("Total", typeof(double));

            DataGridView.Columns.Add(colOrderID);
            DataGridView.Columns.Add(colOrderDate);
            DataGridView.Columns.Add(colName);
            DataGridView.Columns.Add(colTotal);

            dgOrders.ItemsSource = DataGridView.DefaultView;

            Order order = new Order();

            using (MySqlConnection conn = new MySqlConnection(LocalWithDb)) {
                conn.Open();

                MySqlCommand cmd;

                cmd = new MySqlCommand("SELECT orders.OrderID, orders.OrderDate, customers.CustomerID, customers.Name, products.Price, billrows.Amount FROM orders " +
                    "INNER JOIN billrows ON billrows.OrderID=orders.OrderID " +
                    "INNER JOIN customers ON customers.CustomerID=orders.CustomerID " +
                    "INNER JOIN products ON products.ProductID=billrows.ProductID;", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read()) {

                    order.OrderID = dr.GetInt32(0);
                    order.OrderDate = dr.GetString(1);
                    order.CustomerID = dr.GetInt32(2);
                    order.Name = dr.GetString(3);
                    order.Price = dr.GetDouble(4);
                    order.Amount = dr.GetInt32(5);

                    order.Total = order.Price * order.Amount;
                    // Datagridille yhteissumma ei päivity vaikka tässä vaiheessa kun käyttää debuggia total näkyy oikein
                    //Debug.WriteLine(order.Total);
                }
            }
            RowCount = BillrowsTable.Rows.Count + 1;

            DataRow row = DataGridView.NewRow();
            row["OrderID"] = order.OrderID;
            row["OrderDate"] = order.OrderDate;
            row["Name"] = order.Name;
            row["Total"] = order.Total;

          

            DataGridView.Rows.Add(row);
        }
    }
}




