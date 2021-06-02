using EsportApp.models.UserGames;
using EsportApp.models.UserGameTitles;
using EsportApp.models.UserTeams;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsportApp.models.Users
{
    public class GetUserModel : BaseUserModel
    {
        public Guid Id { get; set; }

        public ICollection<string> Roles { get; set; }
        public ICollection<GetUserTeamsModel> UserTeams { get; set; }
        public ICollection<GetUserGamesModel> UserGames { get; set; }
        public ICollection<GetUserGameTitlesModel> UserGameTitles { get; set; }

    }
}
