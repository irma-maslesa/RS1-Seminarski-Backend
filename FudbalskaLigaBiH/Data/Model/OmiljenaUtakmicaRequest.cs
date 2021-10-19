using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class OmiljenaUtakmicaRequest
    {
        [Required]
        public string KorisnikId { get; set; }
        [Required]
        public int UtakmicaID { get; set; }
    }
}
