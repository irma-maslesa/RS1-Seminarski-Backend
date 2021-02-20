using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class ProfileViewModel
    {
        public string UserID { get; set; }

        [Display(Name = "Ime")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Prezime")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Broj telefona")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
