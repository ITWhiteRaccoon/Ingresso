using IngressoApi.Models; 

namespace RadarCine.Services
{
    public class UserSelectionService
    {
        public List<Theater> SelectedTheaters { get; private set; } = new();

        public void StoreSelectedTheaters(IEnumerable<Theater> theaters)
        {
            SelectedTheaters = theaters.ToList();
        }
    }
}