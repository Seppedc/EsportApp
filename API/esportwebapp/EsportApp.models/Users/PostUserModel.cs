using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportApp.models.Users
{
    public class PostUserModel : BaseUserModel
    {
        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^a-zA-Z0-9].*).{6,}$", ErrorMessage = "Minimum 6 karakters met minstens één hoofdletter, kleine letter, cijfer en speciaal teken")]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [JsonIgnore]
        [Display(Name = "Herhaal wachtwoord")]
        public string ConfirmPassword { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
