using Android.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Models
{
    public class MaritalStateModel
    {
        public long MaritalStatusId { get; set; }
        [StringLength(50)]
        public string Description { get; set; } = null!;
        [StringLength(50)]
        public string Code { get; set; } = null!;
        [StringLength(50)]
        public string CreatedBy { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Updated { get; set; }
        public int Version { get; set; }
        [Required]
        public bool? Active { get; set; }
    }
}
