using NHibernate.Mapping.ByCode.Conformist;

namespace ListOnNonGenericCollection.Maps
{
    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Id(e => e.Id);
            Property(e => e.Name);
        }
    }
}
