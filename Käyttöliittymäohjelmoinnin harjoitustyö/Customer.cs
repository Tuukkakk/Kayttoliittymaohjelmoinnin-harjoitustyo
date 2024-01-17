namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    //luokka asiakkaan tietojen käsittelemiseen
    public class Customer {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Customer()
        {
            this.Name= string.Empty;
            this.StreetAddress = string.Empty;
            this.PostalCode = string.Empty;
            this.City = string.Empty;
        }
    }

    
}
