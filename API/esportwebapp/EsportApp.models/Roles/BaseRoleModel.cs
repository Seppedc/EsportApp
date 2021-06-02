using System.ComponentModel.DataAnnotations;

namespace EsportApp.models.Roles
{
    public class BaseRoleModel
    {
        [StringLength(255, ErrorMessage = "Maximum 255 karakters toegelaten")]
        public string Omschrijving { get; set; }
    }
}
