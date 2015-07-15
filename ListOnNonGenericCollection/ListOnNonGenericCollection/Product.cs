using System;
using System.Collections.Generic;

namespace ListOnNonGenericCollection
{
    public class Product
    {
        public Product()
        {
            Tags = new TagCollection();
        }

        public Guid Id { get; set; }
        public IEnumerable<Tag> Tags { get; private set; }
        // SchemaExport doesn't work if using ITagCollection
        //public ITagCollection Tags { get; private set; }
    }
}
