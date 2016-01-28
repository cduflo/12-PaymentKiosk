using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentKiosk.Core.Domain
{
    public class Product
    {
        public string Description { get; set; }
        public decimal amount { get; set; }
    }

    public static class Coffee
    {
      public static  ObservableCollection<Product> coffee = new ObservableCollection<Product>
            {
            new Product {Description = "Columbian Roast", amount = 3.25m },
            new Product {Description = "Kona Roast", amount = 2.25m },
            new Product {Description = "Costa Rican Roast", amount = 5.25m },
            new Product {Description = "Argentinian Roast", amount = 1.25m }
            };
    }

    public static class Quantity
    {
        public static ObservableCollection<int> quantity = new ObservableCollection<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };
    }
}
