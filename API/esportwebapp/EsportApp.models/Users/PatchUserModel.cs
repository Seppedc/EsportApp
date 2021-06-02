using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportApp.models.Users
{
    public class PatchUserModel
    {
        [Required(ErrorMessage = "Huidig wachtwoord is verplicht")]
        [Display(Name = "Huidig wachtwoord")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Nieuw wachtwoord is verplicht")]
        [Display(Name = "Nieuw wachtwoord")]
        public string NewPassword { get; set; }
        [JsonIgnore]
        [Display(Name = "Herhaal nieuw wachtwoord")]
        public string ConfirmNewPassword { get; set; }
    }
}
