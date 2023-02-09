

namespace Swd.PlayCollector.Business
{
    public class CollectionItemService
    {
        private ICollectionItemRepository _IRepository;

        public CollectionItemService()
        {
            _IRepository = new CollectionItemRepository();
        }

        public async Task<IQueryable<CollectionItem>> GetAllAsync()
        {
            var retunList = await _IRepository.GetAllAsync();
            return retunList;
        }
    }
}