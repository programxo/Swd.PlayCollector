using Swd.PlayCollector.Helper;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Swd.PlayCollector.Model
{
    public class CollectionItem: ModelBase
    {
        private string _number;


        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(5, ErrorMessage = "Maximum length is 5 characters")]
        public string Number {
            get { return _number; }
            set { SetProperty(ref _number, value, true); } 
        }
        
        public int? ReleaseYear { get; set; }
        public decimal Price { get; set; }


        public int? LocationId { get; set; }
        public Location Location { get; set; }

        public int? ThemeId { get; set; }
        public Theme Theme { get; set; } 
        public List<Media> MediaSet { get; set; }

        public CollectionItem()
        {
            
        }

        public CollectionItem(bool withDefaults)
        {
            if (withDefaults)
            {
                this.Number = "9999";
                this.Price = 0;
                this.ReleaseYear = DateTime.Now.Year;
            }
        }

        public string PreviewImage
        {
            get
            {
                
                //string rootDir = @"C:\\SwDeveloper2022\\SWDData\\PlayCollector";
                
                if (this.MediaSet.FirstOrDefault() != null)
                {
                    return this.MediaSet.FirstOrDefault().ImagePath;
                }
                else
                {
                    PlayCollectorConfiguration configuration = new PlayCollectorConfiguration();
                    return configuration.PathToPlaceholderImage;
                }
            }
        }

        public string SearchField
        {
            get { return string.Format("{0}{1}{2}", this.Id, this.Name, this.Number); }
        }

    }
}