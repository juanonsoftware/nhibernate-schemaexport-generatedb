using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NhibMigrations.Domain
{
    public class User : IUser
    {
        private readonly IList<Province> _visitedPlaces = new List<Province>();
        private readonly IList<Province> _livedPlaces = new List<Province>();

        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        public ReadOnlyCollection<Province> VisitedPlaces
        {
            get { return new ReadOnlyCollection<Province>(_visitedPlaces); }
        }

        public ReadOnlyCollection<Province> LivedPlaces
        {
            get { return new ReadOnlyCollection<Province>(_livedPlaces); }
        }
    }
}
