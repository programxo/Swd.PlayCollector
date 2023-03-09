

namespace Swd.PlayCollector.Business
{
    public class LocationService
    {
        private ILocationRepository _IRepository;

        public LocationService()
        {
            _IRepository = new LocationRepository();
        }

        public async Task<IQueryable<Location>> GetAllAsync()
        {
            var retunList = await _IRepository.GetAllAsync();
            return retunList;
        }
    }
}