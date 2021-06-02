using System.ComponentModel.DataAnnotations;

namespace EsportApp.models.Users
{
    public class BaseUserModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum 2 en maximum 20 karakters toegelaten")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Familienaam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum 2 en maximum 50 karakters toegelaten")]
        public string Familienaam { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht")]
        //[RegularExpression(@"^\w+[\.]\w+(@svsl\.be)$", ErrorMessage = "Ongeldig @svsl.be e-mailadres")]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Punten is verplicht")]
        public int Punten { get; set; }
    }
}
