using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for AddNewOrder.xaml
    /// </summary>
    public partial class AddNewOrder : Window {
        private int RowCount;
        private int OrderCount;

        private MySqlDataAdapter DataFromBillrows;
        private DataTable BillrowsTable;

        private MySqlDataAdapter DataFromOrders;
        private DataTable OrdersTable;

        private DataTable DataGridView;
        private const string LocalWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";

        public AddNewOrder() {
            InitializeComponent();

            // Otetaan yhteys kantaan ja haetaan sieltä dataa
            // Luodaan uudet taulut ja asetetaan data tauluihin

            DataFromBillrows = new MySqlDataAdapter("SELECT * FROM Billrows", LocalWithDb);
            DataFromOrders = new MySqlDataAdapter("SELECT * FROM Orders", LocalWithDb);

            BillrowsTable = new DataTable();
            OrdersTable = new DataTable();
            DataGridView = new DataTable();

            DataFromBillrows.Fill(BillrowsTable);
            DataFromOrders.Fill(OrdersTable);


            //Tehdään uusi taulu datagridin näyttämistä varten käyttöliittymässä
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

            myDataGrid.ItemsSource = DataGridView.DefaultView;

            OrderCount = OrdersTable.Rows.Count + 1;
            Debug.WriteLine(OrderCount);

        }
        // Metodi jolla lisätään uusia tilausrivejä
        private void AddClick(object sender, RoutedEventArgs e) {
            Product product = new Product();

            // Käydään kannasta malli ja hinta tuottelle
            using (MySqlConnection conn = new MySqlConnection(LocalWithDb)) {
                conn.Open();

                MySqlCommand cmd;
                string prodID = txtProductID.Text;

                cmd = new MySqlCommand("SELECT Model, Price FROM products WHERE ProductID="+prodID+"; ", conn);

                var dr = cmd.ExecuteReader();
                
                if (dr.Read()) {
                    // Map the name value to the class property
                    
                    product.Model = dr.GetString(0);
                    product.Price = dr.GetDouble(1);
                }
                
            }
            RowCount = BillrowsTable.Rows.Count + 1;

            // Otetaan käyttäjän syöttämä data riveittäin
            DataRow newBillRow = BillrowsTable.NewRow();

            newBillRow["ID"] = RowCount;
            newBillRow["OrderID"] = OrderCount;
            newBillRow["ProductID"] = txtProductID.Text;
            newBillRow["Amount"] = txtAmount.Text;

            DataRow row = DataGridView.NewRow();
            row["RowID"] = myDataGrid.Items.Count;
            row["ProductID"] = txtProductID.Text;
            row["Model"] = product.Model;
            row["Amount"] = txtAmount.Text;
            row["Price"] = (product.Price * int.Parse(txtAmount.Text));


            BillrowsTable.Rows.Add(newBillRow);
            DataGridView.Rows.Add(row);

            txtProductID.Clear();
            txtAmount.Clear();

        }

        // Viedään tilausrivit kantaan
        private void SaveClick(object sender, RoutedEventArgs e) {
            OrderCount = OrdersTable.Rows.Count + 1;

            DataRow newOrderRow = OrdersTable.NewRow();

            newOrderRow["OrderID"] = OrderCount;
            newOrderRow["CustomerID"] = txtCustomerID.Text;
            newOrderRow["OrderDate"] = dateOrderDate.Text;

            OrdersTable.Rows.Add(newOrderRow);
            try {

                MySqlCommandBuilder cb = new MySqlCommandBuilder(DataFromOrders);
                DataFromOrders.Update(OrdersTable);

                cb = new MySqlCommandBuilder(DataFromBillrows);
                DataFromBillrows.Update(BillrowsTable);

                MessageBox.Show("Tilaus vietiin tietokantaan");
            }
            catch (Exception er) {
                MessageBox.Show("Virhe päivittäessä tietokantaa: " + er.Message);
            }
        }

    }

}












