using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportApp.models.Users
{
    public class PutUserModel : BaseUserModel
    {

        public ICollection<string> Roles { get; set; }
    }
}
