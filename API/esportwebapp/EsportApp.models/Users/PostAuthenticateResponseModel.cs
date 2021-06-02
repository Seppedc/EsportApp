﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsportApp.models.Users
{
    public class PostAuthenticateResponseModel
    {
        public Guid Id { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public ICollection<string> Roles { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
