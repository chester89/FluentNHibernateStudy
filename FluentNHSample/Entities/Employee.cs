using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHSample.Entities {
    public class Employee {
        public virtual Int32 Id { get; private set; }
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual Store Store { get; set; }
    }
}
