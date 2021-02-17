using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Naziv uloge")]
        public string RoleName { get; set; }
    }
}
