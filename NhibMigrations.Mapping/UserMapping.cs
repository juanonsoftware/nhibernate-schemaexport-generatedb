using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NhibMigrations.Domain;

namespace NhibMigrations.Mapping
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Id(u => u.Id, idm =>
            {
                idm.Generator(Generators.Identity);
                idm.Type(new Int32Type());
            });
            Property(u => u.Email);
            Property(u => u.Address);

            List(e => e.VisitedPlaces, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Lazy(CollectionLazy.Lazy);
                cm.Key(km => km.Column("ProvinceId"));
                cm.Table("UserVisitedPlaces");
                cm.Index(im => im.Column("Position"));
            }, m => m.ManyToMany(mm =>
            {
                mm.Class(typeof(Province));
                mm.Column("UserId");
            }));

            Bag(e => e.LivedPlaces, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Lazy(CollectionLazy.Lazy);
                cm.Key(km => km.Column("ProvinceId"));
                cm.Table("UserLivedPlaces");
            }, m => m.ManyToMany(mm =>
            {
                mm.Class(typeof(Province));
                mm.Column("UserId");
            }));
        }
    }
}
