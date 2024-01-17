using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Xml;

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    
    public partial class OrderEdit : Window {

        private int RowCount;
        private int OrderCount;

        private MySqlDataAdapter DataFromBillrows;
        private DataTable BillrowsTable;

        private MySqlDataAdapter DataFromOrders;
        private DataTable OrdersTable;

        private DataTable DataGridView;
        private const string LocalWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";

        public DataTable GridViewForUpdate;

        public OrderEdit() {

            InitializeComponent();
        }

        // Metodi jolla haetaan tämänhetkinen tilausrivien ja tilauksien data kannasta
        private void refreshProductsEdit(object sender, RoutedEventArgs e) {

            string number = txtOrderNro.Text;

            // Luodaan taulut, suoritetaan kyselyt ja upotetaan saatu data tauluihin
            if (number != String.Empty) {

                DataFromBillrows = new MySqlDataAdapter("SELECT * FROM Billrows", LocalWithDb);
                DataFromOrders = new MySqlDataAdapter("SELECT * FROM Orders", LocalWithDb);


                BillrowsTable = new DataTable();
                OrdersTable = new DataTable();
                DataGridView = new DataTable();

                DataFromBillrows.Fill(BillrowsTable);
                DataFromOrders.Fill(OrdersTable);


                // Luodaan uusi taulu data gridiä varten ja asetetaan se Data gridin data contextiksi
                DataColumn colRowID = new DataColumn("RowID", typeof(int));
                DataColumn colProdID = new DataColumn("ProductID", typeof(string));
                DataColumn colModel = new DataColumn("Model", typeof(string));
                DataColumn colAmount = new DataColumn("Amount", typeof(string));
                DataColumn colPrice = new DataColumn("Price", typeof(double));

                DataGridView.Columns.Add(colRowID);
                DataGridView.Columns.Add(colProdID);
                DataGridView.Columns.Add(colModel);
                DataGridView.Columns.Add(colAmount);
                DataGridView.Columns.Add(colPrice);

                dgOrders.ItemsSource = DataGridView.DefaultView;

                OrderCount = OrdersTable.Rows.Count + 1;

                Debug.WriteLine(OrderCount);

                OrderView ov = new OrderView();

                // Haetaan data kannasta tietyn tilausnumeron perusteella ja lisätään data tauluihin
                using (MySqlConnection conn = new MySqlConnection(LocalWithDb)) {
                    conn.Open();

                    MySqlCommand cmd;
                    string OrderID = txtOrderNro.Text;

                    cmd = new MySqlCommand("SELECT billrows.ID, billrows.OrderID, billrows.ProductID, products.Model, billrows.Amount, products.Price FROM billrows " +
                        "INNER JOIN products ON billrows.ProductID=products.ProductID WHERE billrows.OrderID=" + OrderID + ";", conn);

                    var dr = cmd.ExecuteReader();

                    while (dr.Read()) {
                        

                        ov.BillrowID = dr.GetInt32(0);
                        ov.OrderID = dr.GetInt32(1);
                        ov.ProductID = dr.GetInt32(2);
                        ov.Model = dr.GetString(3);
                        ov.Amount = dr.GetInt32(4);
                        ov.Price = dr.GetDouble(5);

                        DataRow newBillRow = BillrowsTable.NewRow();

                        newBillRow["ID"] = RowCount;
                        newBillRow["OrderID"] = OrderCount;
                        newBillRow["ProductID"] = ov.ProductID;
                        newBillRow["Amount"] = ov.Amount;

                        BillrowsTable.Rows.Add(newBillRow);

                        DataRow row = DataGridView.NewRow();
                        row["RowID"] = dgOrders.Items.Count;
                        row["ProductID"] = ov.ProductID;
                        row["Model"] = ov.Model;
                        row["Amount"] = ov.Amount;
                        row["Price"] = ov.Price;

                        Debug.WriteLine(row["RowID"]);
                        Debug.WriteLine(row["ProductID"]);
                        Debug.WriteLine(row["Model"]);
                        Debug.WriteLine(row["Amount"]);
                        Debug.WriteLine(row["Price"]);

                        DataGridView.Rows.Add(row);
                    }
                }
            }
            else {
                MessageBox.Show("Anna tilausnumero");
            }
        }

        // Metodi jolla muutokset voidaan tallentaa kantaan
        public void SaveEditOrder(object sender, RoutedEventArgs e) {

            string number = txtOrderNro.Text;

            MySqlConnection connection = new MySqlConnection(LocalWithDb);
            connection.Open();

            IEnumerable items = DataGridView.AsEnumerable();

            dgOrders.ItemsSource = items;

            if (items != null) {
                foreach (var item in items) {
                    DataRow? row = item as DataRow;
                    var id = row["RowID"];
                    var productID = row["ProductID"];
                    var amount = row["Amount"];
                    var price = row["Price"];

                    string update = "UPDATE billrows SET OrderID=@orderID, ProductID=@productID, Amount=@amount WHERE ID=@id";

                    MySqlCommand command = new MySqlCommand(update, connection);

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@OrderID", number);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
   
                }
                MessageBox.Show("Muokkaus vietiin tietokantaan");
            }  
        }
    }
}
