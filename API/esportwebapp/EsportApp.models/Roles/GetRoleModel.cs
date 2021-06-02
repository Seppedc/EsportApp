using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportApp.models.Roles
{
    public class GetRoleModel : BaseRoleModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        
    }
}
