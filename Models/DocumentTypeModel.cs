using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SPMSCAV1.Models
{
    public class DocumentTypeModel
    {
        public DocumentTypeModel()
        {
            //InjuryCodes = new HashSet<InjuryCodeModel>();
        }

        public long DocumentTypeId { get; set; }
        [StringLength(50)]
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
        public long CaseTypeId { get; set; }
        public bool Editable { get; set; }
        [Required]
        public bool? Uploadable { get; set; }
    }
}
