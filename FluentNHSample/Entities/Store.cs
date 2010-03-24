using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHSample.Entities {
    public class Store {
        public virtual Int32 Id { get; private set; }
        public virtual String Name { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<Employee> Staff { get; set; }

        public Store() {
            Products = new List<Product>();
            Staff = new List<Employee>();
        }

        public virtual void addProduct(Product product) {
            product.StoresStockedIn.Add(this);
            Products.Add(product);
        }

        public virtual void addEmployee(Employee employee) {
            employee.Store = this;
            Staff.Add(employee);
        }
    }
}
