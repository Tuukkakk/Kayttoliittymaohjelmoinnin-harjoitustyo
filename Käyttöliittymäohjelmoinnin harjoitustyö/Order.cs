namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    public class Order {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string OrderDate { get; set; }
        public int BillrowID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }


        public Order()
        {
            this.Name = string.Empty;
            this.OrderDate = string.Empty;
            this.Model = string.Empty;
        }
    }

    
}
