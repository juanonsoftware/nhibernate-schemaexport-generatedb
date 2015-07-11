using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NhibMigrations.Domain
{
    public class Province
    {
        private readonly IList<User> _citizens = new List<User>();
        private readonly IList<User> _visitors = new List<User>();

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ReadOnlyCollection<User> Citizens
        {
            get { return new ReadOnlyCollection<User>(_citizens); }
        }

        public ReadOnlyCollection<User> Visitors
        {
            get { return new ReadOnlyCollection<User>(_visitors); }
        }


    }
}
