using Swd.PlayCollector.Helper;

namespace Swd.PlayCollector.Model
{
    public class Media
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Uri { get; set; }


        public TypeOfDocument TypeOfDocument { get; set; }
        public CollectionItem CollectionItem { get; set; }


        public string ImagePath 
        { 
            get
            {
                PlayCollectorConfiguration config = new();
                string rootDir = config.PathToMediafiles;
                return Path.Combine(rootDir, Uri, Name);
            }
        }
    }
}
