using System;
using System.Reflection;
using FluentNHibernate.Conventions;

namespace FluentNHSample.Conventions {
    public class CustomForeignKeyConvention: ForeignKeyConvention {
        protected override String GetKeyName(PropertyInfo property, Type type) {
            return property == null ? type.Name + "Id": property.Name + "Id";
        }
    }
}
