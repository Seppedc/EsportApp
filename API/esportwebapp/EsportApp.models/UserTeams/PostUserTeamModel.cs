﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportApp.models.UserTeams
{
    public class PostUserTeamModel
    {
        public Guid UserId { get; set; }

        public Guid TeamId { get; set; }
    }
}
