using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NhibMigrations.Domain;

namespace NhibMigrations.Mapping
{
    public class ProvinceMapping : ClassMapping<Province>
    {
        public ProvinceMapping()
        {
            Id(e => e.Id);
            Property(e => e.Code, m =>
                                  {
                                      m.UniqueKey("UK_Profince_Code");
                                      m.NotNullable(true);
                                  });
            Property(e => e.Name);

            List(e => e.Citizens, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Lazy(CollectionLazy.Lazy);
                cm.Key(km =>
                       {
                           km.Column("ProvinceId");
                           km.ForeignKey("FK_Province");
                       });
                cm.Index(im =>
                {
                    im.Column("Position");
                    im.Base(1);
                });
            },
            m => m.OneToMany(mm => mm.Class(typeof(User))));

            List(e => e.Visitors, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Lazy(CollectionLazy.Lazy);
                cm.Key(km => km.Column("ProvinceId"));
                cm.Table("ProvinceVisitors");
                cm.Index(im => im.Column("Position"));
            },
            m => m.ManyToMany(mm =>
                              {
                                  mm.Class(typeof(User));
                                  mm.Column("UserId");
                              }));
        }
    }
}
