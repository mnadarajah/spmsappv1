using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Models
{
    public class ClientModel
    {
        public ClientModel()
        {
        }
        public long ClientId { get; set; }

        public long PrefixId { get; set; }

        [StringLength(60)]
        public string FirstName { get; set; } = null!;

        [StringLength(60)]
        public string? MiddleName { get; set; }
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(60)]
        public string LastName { get; set; } = null!;
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(20)]
        public string? Suffix { get; set; }
        /// <summary>
        /// Gender ID
        /// </summary> 
        public long GenderId { get; set; }
        public DateTime DateofBirth { get; set; }

        [StringLength(150)]
        public string Address1 { get; set; } = null!;
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(150)]
        public string? Address2 { get; set; }
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(60)]
        public string City { get; set; } = null!;
        /// <summary>
        /// State or Province ID
        /// </summary> 
        public long ProvinceOrStateId { get; set; }
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(10)]
        public string? PostalCodeOrZipCode { get; set; }
        /// <summary>
        /// Country
        /// </summary> 
        public long CountryId { get; set; }
        [StringLength(15)]
        public string? HomePhone { get; set; }
        [StringLength(15)]
        public string? CellPhone { get; set; }
        [StringLength(15)]
        public string? PersanalFax { get; set; }
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(100)]
        public string? PersonalEmail { get; set; }
        public long MaritalStatusId { get; set; }
        [StringLength(10)]
        public string? SocialInsuranceNumber { get; set; }
        [StringLength(50)]
        public string? HealthCardNumber { get; set; }
        [StringLength(50)]
        public string? DriversLicenseNumber { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        [StringLength(50)]
        public string? Occupation { get; set; }
        [StringLength(15)]
        public string? WorkPhone { get; set; }
        [StringLength(10)]
        public string? WorkPhoneExtension { get; set; }
        [StringLength(15)]
        public string? WorkFax { get; set; }
        /// <summary>
        /// This field was imported.
        /// </summary>
        [StringLength(100)]
        public string? WorkEmail { get; set; }
        public byte[]? Picture { get; set; }
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
