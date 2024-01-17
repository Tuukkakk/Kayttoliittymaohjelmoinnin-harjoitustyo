using MySqlConnector;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {

    //Luokka jossa käsitellään tietokantaa
    public class DiscRepository {

        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=DiscDb;";
        private const string local = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1;";

        //Luodaan tietokanta
        public void CreateDiscDb() {
            using (MySqlConnection conn = new MySqlConnection(local)) {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("DROP DATABASE IF EXISTS DiscDb", conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("CREATE DATABASE DiscDb", conn);
                cmd.ExecuteNonQuery();
            }
        }
        //Luodaan tietokantaan taulut
        public void CreateTables() {
            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();

                string customers = "CREATE TABLE Customers (CustomerID INT NOT NULL PRIMARY KEY, Name VARCHAR(50), StreetAddress VARCHAR(50), PostalCode VARCHAR(10), City VARCHAR(50))";
                string orders = "CREATE TABLE Orders (OrderID INT NOT NULL PRIMARY KEY, CustomerID INT, FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID), OrderDate VARCHAR(50))";
                string billRows = "CREATE TABLE billRows (ID INT NOT NULL PRIMARY KEY, OrderID INT, ProductID INT, Amount INT, FOREIGN KEY (OrderID) REFERENCES Orders(OrderID), FOREIGN KEY (ProductID) REFERENCES Products(ProductID))";
                string products = "CREATE TABLE Products (ProductID INT NOT NULL PRIMARY KEY, Model VARCHAR(50), Amount INT, Price DOUBLE)";


                MySqlCommand cmd = new MySqlCommand(customers, conn);
                cmd.ExecuteNonQuery();

                MySqlCommand cmd1 = new MySqlCommand(products, conn);
                cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand(orders, conn);
                cmd2.ExecuteNonQuery();

                MySqlCommand cmd3 = new MySqlCommand(billRows, conn);
                cmd3.ExecuteNonQuery();


            }
        }
        //Täytetään tietokannan taulut
        public void FillDatabase() {
            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();

                string disc1 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(1, 'Discmania D-Line P2 Putter', 39, 11.90)";
                string disc2 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(2, 'Discmania S-Line P3X Putter', 12, 19.90)";
                string disc3 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(3, 'Discmania Neo Origin Mid-Range', 18, 18.90)";
                string disc4 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(4, 'Discmania C-Line MD1 Mid-Range', 5, 22.90)";
                string disc5 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(5, 'Discmania Neo Instinct Fairway Driver', 30, 18.90)";
                string disc6 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(6, 'Discmania Horizon FD3 Fairway Driver', 7, 25.90)";
                string disc7 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(7, 'Discmania S-Line DD3 Distance Driver', 40, 19.90)";
                string disc8 = "INSERT INTO Products(ProductID, model, amount, price)VALUES(8, 'Discmania S-Line PD2 Distance Driver',2 ,19.90)";

                string customer1 = "INSERT INTO Customers(CustomerID, Name, StreetAddress, PostalCode, City)VALUES(1, 'Simon Lizotte', 'Fakeavenue 112', 1234567, 'Arizona')";
                string customer2 = "INSERT INTO Customers(CustomerID, Name, StreetAddress, PostalCode, City)VALUES(2, 'Vaino Makela', 'Asiakkaankatu 13', 1234567, 'Espoo')";

                string order1 = "INSERT INTO Orders(OrderID, CustomerID, OrderDate)VALUES(1, 1, '06.03.2023')";
                string order2 = "INSERT INTO Orders(OrderID, CustomerID, OrderDate)VALUES(2, 2, '06.03.2023')";

                string billrow1 = "INSERT INTO billRows(ID, OrderID, ProductID, Amount)VALUES(1, 1, 1, 3)";
                string billrow2 = "INSERT INTO billRows(ID, OrderID, ProductID, Amount)VALUES(2, 1, 3, 5)";
                string billrow3 = "INSERT INTO billRows(ID, OrderID, ProductID, Amount)VALUES(3, 2, 4, 1)";
                string billrow4 = "INSERT INTO billRows(ID, OrderID, ProductID, Amount)VALUES(4, 2, 5, 1)";

                MySqlCommand cmd = new MySqlCommand(disc1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc3, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc4, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc5, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc6, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc7, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(disc8, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(customer1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(customer2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(order1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(order2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(billrow1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(billrow2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(billrow3, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(billrow4, conn);
                cmd.ExecuteNonQuery();

            }
        }
        //Metodi jonka avulla lisätään uusi asiakas tietokantaan
        public void AddCustomer(Customer customer) {
            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();

                string addNewCustomer = "INSERT INTO Customers(CustomerID, Name, StreetAddress, PostalCode, City)" +
                    "VALUES(@CustomerID, @Name, @StreetAddress, @PostalCode, @City);";

                MySqlCommand cmd = new MySqlCommand(addNewCustomer, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                cmd.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                cmd.Parameters.AddWithValue("@City", customer.City);
                var dr = cmd.ExecuteNonQuery();



            }
        }
        //Metodi jonka avulla muokattu tuote viedään tietokantaan
        public void EditProduct(Product product) {
            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();

                string editProduct = "UPDATE Products SET Model=@Model, Amount=@Amount, Price=@Price WHERE ProductID=@ProductID;";

                MySqlCommand cmd = new MySqlCommand(editProduct, conn);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@Model", product.Model);
                cmd.Parameters.AddWithValue("@Amount", product.Amount);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                var dr = cmd.ExecuteNonQuery();

            }
        }
        //Metodi jonka avulla muokattu asiakas viedään tietokantaan
        public void EditCustomer(Customer customer) {
            using (MySqlConnection conn = new MySqlConnection(localWithDb)) {
                conn.Open();

                string editCustomer = "UPDATE Customers SET Name=@Name, StreetAddress=@StreetAddress, PostalCode=@PostalCode, City=@City WHERE CustomerID=@CustomerID;";

                MySqlCommand cmd = new MySqlCommand(editCustomer, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                cmd.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                cmd.Parameters.AddWithValue("@City", customer.City);
                var dr = cmd.ExecuteNonQuery();

            }
        }
        //Metodi jonka avulla muokattu tilaus viedään tietokantaan
       

        
    }

}
