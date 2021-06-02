using EsportApp.api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportApp.api.Data
{
    public class DbInitializer
    {
        internal static void Initialize(IServiceProvider serviceProvider)
        {
            EsportAppContext _context = serviceProvider.GetRequiredService<EsportAppContext>();
            UserManager<User> _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<Role> _roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            // Delete existing database and create new database with default test data
            // =======================================================================

            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Look for any roles
            // If no roles found then the db is empty
            // Fill the db with test data
            // ======================================

            if (_context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            // Create roles
            // ============

            _ = _roleManager.CreateAsync(new Role { Name = "Administrator", Omschrijving = "Heeft volledige toegang tot alle functionaliteiten." }).Result;
            _ = _roleManager.CreateAsync(new Role { Name = "User", Omschrijving = "Kan alles behalve het wijzigen van toegangsrechten." }).Result;
            // Create users
            // ============
            User administrator = new User
            {
                Voornaam = "Seppe",
                Familienaam = "De Craene",
                Email = "decraeneseppe@hotmail.be",
                Punten = 1000,
                UserName = "decraeneseppe@hotmail.be",
            };
            _ = _userManager.CreateAsync(administrator, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(administrator, new List<string> { "Administrator", "User" }).Result;

            User user1 = new User
            {
                Voornaam = "siebe",
                Familienaam = "helper",
                Email = "siebe@helper.be",
                Punten = 1,
                UserName= "siebe@helper.be",
            };

            _ = _userManager.CreateAsync(user1, "_Azerty123").Result;
            _ = _userManager.AddToRolesAsync(user1, new List<string> { "User" }).Result;

            // Create GameTitles
            // ============
            GameTitle cod = new GameTitle { Naam = "Call of Duty: Black Ops Cold War", Uitgever = "Activision" };
            _context.GameTitles.Add(cod);
            GameTitle lol = new GameTitle { Naam = "League Of Legends", Uitgever = "Riot Games" };
            _context.GameTitles.Add(lol);
            GameTitle csgo = new GameTitle { Naam = "Counter Strike Global Offensive", Uitgever = "Valve" };
            _context.GameTitles.Add(csgo);

            // Create Teams
            // ============
            Team thieves = new Team { Naam = "100 Thieves" };
            _context.Teams.Add(thieves);
            Team Cloud = new Team { Naam = "Cloud9" };
            _context.Teams.Add(Cloud);
            Team astralis = new Team { Naam = "Astralis" };
            _context.Teams.Add(astralis);
            Team optic = new Team { Naam = "Optic Gaming" };
            _context.Teams.Add(optic);

            // Create Tornooien
            // ============
            Tornooi tornooia = new Tornooi {
                Naam = "Worlds",
                Organisator = "Riot",
                Beschrijving = "Wk van league of legends",
                GameTitleId=lol.Id,
                Type="",
            };
            _context.Tornooien.Add(tornooia);
            Tornooi tornooib = new Tornooi
            {
                Naam = "ESL One Cologne 2021",
                Organisator = "ESL",
                Beschrijving = "ESL Pro Tour Championship",
                GameTitleId =csgo.Id,
                Type = "",
            };
            _context.Tornooien.Add(tornooib);

            Tornooi tornooic = new Tornooi
            {
                Naam = "LA THIEVES HOME SERIES",
                Organisator = "Activision",
                Beschrijving = "Stage IV week 3",
                GameTitleId =cod.Id,
                Type = "",
            };
            _context.Tornooien.Add(tornooic);

            // Create Games
            // ============

            Game gameA = new Game
            {
                Score = "",
                Datum = new DateTime(2021, 6, 5, 18, 30, 0),
                Status = "TBD",
                Type = "",
                ToernooiId = tornooib.Id,
            };
            _context.Games.Add(gameA);
            Game gameB = new Game
            {
                Score = "",
                Datum = new DateTime(2021, 6, 10, 18, 30, 0),
                Status = "TBD",
                Type = "",
                ToernooiId = tornooia.Id,
            };
            _context.Games.Add(gameB);
            Game gameC = new Game
            {
                Score = "",
                Datum = new DateTime(2021, 6, 5, 19, 0, 0),
                Status = "TBD",
                Type = "",
                ToernooiId = tornooic.Id,
            };
            _context.Games.Add(gameC);

            // Create UserGameTitles
            // ============
            _context.UserGameTitles.Add(new UserGameTitle { UserId = administrator.Id, GameTitleId = lol.Id });
            _context.UserGameTitles.Add(new UserGameTitle { UserId = administrator.Id, GameTitleId = csgo.Id });

            _context.UserGameTitles.Add(new UserGameTitle { UserId = user1.Id, GameTitleId = cod.Id });

            // Create UserTeams
            // ============
            _context.UserTeams.Add(new UserTeam { UserId = administrator.Id, TeamId = thieves.Id });

            // Create UserGames
            // ============
            _context.UserGames.Add(new UserGame { UserId = administrator.Id, GameId = gameA.Id });

            // Create GameTitleTeams
            // ============
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = cod.Id, TeamId = thieves.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = cod.Id, TeamId = Cloud.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = cod.Id, TeamId = optic.Id });

            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = csgo.Id, TeamId = Cloud.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = csgo.Id, TeamId = astralis.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = csgo.Id, TeamId = optic.Id });

            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = lol.Id, TeamId = thieves.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = lol.Id, TeamId = Cloud.Id });
            _context.GameTitleTeams.Add(new GameTitleTeam { GameTitleId = lol.Id, TeamId = astralis.Id });

            // Create TornooiTeams
            // ============
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooia.Id, TeamId = thieves.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooia.Id, TeamId = Cloud.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooia.Id, TeamId = astralis.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooia.Id, TeamId = optic.Id });

            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooib.Id, TeamId = astralis.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooib.Id, TeamId = optic.Id });

            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooic.Id, TeamId = thieves.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooic.Id, TeamId = Cloud.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooic.Id, TeamId = astralis.Id });
            _context.TornooiTeams.Add(new TornooiTeam { ToernooiId = tornooic.Id, TeamId = optic.Id });

            // Create TeamGames
            // ============
            _context.TeamGames.Add(new TeamGame { TeamId = astralis.Id, GameId = gameA.Id });
            _context.TeamGames.Add(new TeamGame { TeamId = optic.Id, GameId = gameA.Id });

            _context.TeamGames.Add(new TeamGame { TeamId = thieves.Id, GameId = gameB.Id });
            _context.TeamGames.Add(new TeamGame { TeamId = Cloud.Id, GameId = gameB.Id });

            _context.TeamGames.Add(new TeamGame { TeamId = thieves.Id, GameId = gameC.Id });
            _context.TeamGames.Add(new TeamGame { TeamId = optic.Id, GameId = gameC.Id });

            _context.SaveChanges();
        }
    }
}
