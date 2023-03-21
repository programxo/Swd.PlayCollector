using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Swd.PlayCollector.Model
{
    public class ModelBase : ObservableValidator
    {
        [MaxLength(25)]
        public string CreatedBy { get; set; }

        [MaxLength(25)]
        public string? UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [AllowNull]
        public DateTime? UpdatedDate { get; set; }

    }
}
