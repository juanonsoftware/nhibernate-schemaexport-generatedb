using System;
using System.Collections.Generic;

namespace ListOnNonGenericCollection
{
    public class Product
    {
        public Product()
        {
            Tags = new List<Tag>();
        }

        public Guid Id { get; set; }
        public  string Name { get; set; }
        public IList<Tag> Tags { get; private set; }
        // SchemaExport doesn't work if using ITagCollection
        //public ITagCollection Tags { get; private set; }
    }
}
