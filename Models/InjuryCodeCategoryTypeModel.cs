using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SPMSCAV1.Models
{
    public class InjuryCodeCategoryTypeModel
    {
        public InjuryCodeCategoryTypeModel()
        {
            //InjuryCodes = new HashSet<InjuryCodeModel>();
        }

        public long InjuryCodeCategoryTypeId { get; set; }
        public long InjuryCodeSeriesTypeId { get; set; }
        [StringLength(500)]
        public string Description { get; set; } = null!;
        [StringLength(50)]
        public string Code { get; set; } = null!;
        [StringLength(50)]
        public string CreatedBy { get; set; } = null!;
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; } = null!;
        public DateTime Updated { get; set; }
        public int Version { get; set; }
        [Required]
        public bool? Active { get; set; }
    }
}
