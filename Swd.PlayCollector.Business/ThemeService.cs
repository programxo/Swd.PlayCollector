

using Swd.PlayCollector.Repository;

namespace Swd.PlayCollector.Business
{
    public class ThemeService
    {
        private IThemeRepository _IRepository;

        public ThemeService()
        {
            _IRepository = new ThemeRepository();
        }

        public async Task<IQueryable<Theme>> GetAllAsync()
        {
            var retunList = await _IRepository.GetAllAsync();
            return retunList;
        }
    }
}