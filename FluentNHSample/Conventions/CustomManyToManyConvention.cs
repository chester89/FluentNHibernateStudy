using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions;

namespace FluentNHSample.Conventions {
    public class CustomManyToManyConvention : ManyToManyTableNameConvention {
        protected override String GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide) {
            return collection.EntityType.Name + otherSide.EntityType.Name;
        }

        protected override String GetUniDirectionalTableName(IManyToManyCollectionInspector collection) {
            throw new NotImplementedException();
        }
    }
}
