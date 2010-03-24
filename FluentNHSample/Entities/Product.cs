using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHSample.Entities {
    public class Product {
        public virtual Int32 Id { get; private set; }
        public virtual String Name { get; set; }
        public virtual Double Price { get; set; }
        public virtual IList<Store> StoresStockedIn { get; private set; }

        public Product() {
            StoresStockedIn = new List<Store>();
        }
    }
}
