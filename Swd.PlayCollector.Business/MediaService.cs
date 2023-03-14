using Swd.PlayCollector.Model;
using Swd.PlayCollector.Repository;

namespace Swd.PlayCollector.Business
{



    public class MediaService
    {

        private IMediaRepository _IRepository;


        public MediaService()
        {
            _IRepository = new MediaRepository();

        }


        public async Task<IQueryable<Media>> GetAllAsync()
        {
            var returnList = await _IRepository.GetAllAsync();
            return returnList;
        }



    }
}