﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderingBusiness.Repositories
{
    public class CustomerRepository
    {
        private OnlineOrderingEntities _entities = new OnlineOrderingEntities();
        
        public List<Customer> GetCustomers()
        {
            return _entities.Customers.ToList();
        }

        public Customer GetCustomer(Guid customerId)
        {
            return _entities.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public void CreateCustomer(Customer customer)
        {
            _entities.Customers.Add(customer);
            _entities.SaveChanges();
        }
    }
}
