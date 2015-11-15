using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ListOnNonGenericCollection.Maps
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(e => e.Id);

            Property(e => e.Name);

            List(e => e.Tags, cm =>
            {
                cm.Cascade(Cascade.Remove);
                cm.Lazy(CollectionLazy.Lazy);
                cm.Table("Product_Tag");
                cm.Key(km =>
                {
                    km.Column("ProductId");
                    km.ForeignKey("FK_Product");
                });
                cm.Access(Accessor.ReadOnly);
                cm.Index(lim => lim.Column("Position"));
            },
            m => m.ManyToMany(mmm =>
            {
                mmm.Class(typeof(Tag));
                mmm.Column("TagId");
                mmm.ForeignKey("FK_Tag");
            }));
        }
    }
}