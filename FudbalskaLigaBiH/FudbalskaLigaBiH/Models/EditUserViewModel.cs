using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Ime")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Prezime")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Broj telefona")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
    }
}
