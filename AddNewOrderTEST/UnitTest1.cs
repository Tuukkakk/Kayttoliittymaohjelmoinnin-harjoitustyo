using System;
using Xunit;
using K�ytt�liittym�ohjelmoinnin_harjoitusty�;
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