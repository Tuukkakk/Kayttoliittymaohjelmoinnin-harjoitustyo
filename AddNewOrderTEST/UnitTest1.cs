using System;
using Xunit;
using Käyttöliittymäohjelmoinnin_harjoitustyö;
using System.Windows;

namespace AddNewOrderTEST {
    public class UnitTest1 {
        [Fact]
        public void Test1() {
            var test = new AddNewProduct();
            test.AddProduct();
        }
    }
}