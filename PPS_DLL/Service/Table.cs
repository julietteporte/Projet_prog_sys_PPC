using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Table
    {

        public int Capacity { get; }
        public bool IsAvailable { get; set; }
        public bool IsDressed { get; set; }
        public bool IsCleaned { get; set; }
        public List<Customer> Customers { get; set; }

        public Table(int capacity)
        {
            this.Capacity = capacity;
            this.IsAvailable = true;
            this.IsDressed = true;
        }

        public void AddCustomer(Customer c)
        {
            this.Customers.Add(c);
        }

        public void RemoveCustomers(Customer c)
        {
            this.Customers.Remove(c);
        }

    }
}
