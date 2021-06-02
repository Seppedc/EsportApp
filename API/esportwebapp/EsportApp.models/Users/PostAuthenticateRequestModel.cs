using System.ComponentModel.DataAnnotations;

namespace EsportApp.models.Users
{
    public class PostAuthenticateRequestModel
    {
        [Required(ErrorMessage = "E-mailadres is verplicht")]
        //[RegularExpression(@"^\w+[\.]\w+(@svsl\.be)$", ErrorMessage = "Ongeldig @svsl.be e-mailadres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^a-zA-Z0-9].*).{6,}$", ErrorMessage = "Minimum 6 karakters met minstens één hoofdletter, kleine letter, cijfer en speciaal teken")]
        public string Password { get; set; }
    }
}

