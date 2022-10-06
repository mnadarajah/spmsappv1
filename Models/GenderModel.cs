using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMSCAV1.Models
{
    public class GenderModel
    {
        public GenderModel()
        {
            //Clients = new HashSet<Client>();
        }

     
        public long GenderId { get; set; }
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

        //[InverseProperty("Gender")]
        //public virtual ICollection<Client> Clients { get; set; }
    }
}
