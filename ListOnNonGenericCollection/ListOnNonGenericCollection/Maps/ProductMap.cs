using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ListOnNonGenericCollection.Maps
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(e => e.Id);
            List(e => e.Tags, cm =>
            {
                cm.Table("Product_Tag");
                cm.Key(km=>km.Column("ProductId"));
                cm.Access(Accessor.ReadOnly);
            }, m =>
            {
                m.OneToMany();
            });
        }
    }
}