using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Order
    {
        public Table Table { get; set; }
        public Recipe recipe { get; set; }
        public Customer Customer { get; set; }

        public Order(Table table, Customer customer, Recipe recipe)
        {
            this.Table = table;
            this.Customer = customer;
            this.recipe = recipe;
        }
    }
}
