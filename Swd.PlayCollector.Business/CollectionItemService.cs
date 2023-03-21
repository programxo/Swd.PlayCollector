using Swd.PlayCollector.Helper;
using Swd.PlayCollector.Model;
using Swd.PlayCollector.Repository;

namespace Swd.PlayCollector.Business
{
    public class CollectionItemService
    {

        private ICollectionItemRepository _IRepository;

        public CollectionItemService()
        {
            _IRepository = new CollectionItemRepository();
        }

        public async Task AddAsync(CollectionItem item)
        {
            await _IRepository.AddAsync(item);
        }

        public async Task UpdateAsync(CollectionItem item)
        {
            await _IRepository.UpdateAsync(item, item.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _IRepository.DeleteAsync(id);
        }

        public async Task<IQueryable<CollectionItem>> GetAllAsync()
        {
            var returnList = await _IRepository.GetAllAsync();
            return returnList;
        }

        public async Task<IQueryable<CollectionItem>> GetAllInklusiveAsync()
        {
            var returnList = await _IRepository.GetAllInklusiveAsync();
            return returnList;
        }

        public async Task AddMediaItems(IEnumerable<string> sourcefilePaths, CollectionItem item)
        {
            if (item != null)
            {
                foreach (var sourcefilePath in sourcefilePaths)
                {
                    string targetFilePath = await CopyFile(sourcefilePath, item.Id);
                    string fileExtension = Path.GetExtension(targetFilePath);
                    TypeOfDocumentService typeOfDocumentService = new();

                    Media media = new Media
                    {
                        Name = Path.GetFileName(targetFilePath),
                        Uri = string.Format("{0}", item.Id),
                        TypeOfDocument = await typeOfDocumentService.GetTypeOfDocumentByFileExtension(fileExtension),
                        CollectionItem = item
                    };
                    _IRepository.AddMedia(item, media);
                }
            }
        }

        private async Task<string> CopyFile(string sourceFilePath, int id)
        {
            PlayCollectorConfiguration configuration = new();
            string rootDir = configuration.PathToMediafiles;
            string targetFilePath = Path.Combine(rootDir, id.ToString(), Path.GetFileName(sourceFilePath));
            await FileHelper.CopyFile(sourceFilePath, targetFilePath);
            return targetFilePath;
        }
    }
}