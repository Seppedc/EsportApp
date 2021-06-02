﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserTeams
{
    public class GetUserTeamModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string User { get; set; }
        public string Naam { get; set; }
        public Guid TeamId { get; set; }
        public string Team { get; set; }
    }
}
