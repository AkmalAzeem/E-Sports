using E_Sports.Data;
using E_Sports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E_Sports.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDBContext applicationDBContext;
        public string user;

        public AdminController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoginTo(PlayerLogin adminLogin)
        {
            var admin = await applicationDBContext.Admin.FirstOrDefaultAsync(x => (x.Name == adminLogin.UserName) && (x.Password == adminLogin.Password));

            var loginModel = new PlayerLogin();
            if (admin == null)
            {
                return RedirectToAction("LoginError");

            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult LoginError()
        {
            return View();
        }

        //View all trophys for the admin

        [HttpGet]
        public async Task<IActionResult> ThrophyDashboard()
        {
            var trophys = await applicationDBContext.Trophies.ToListAsync();
            return View(trophys);

        }


        [HttpGet]
        public async Task<IActionResult> TeamsDashboard(Guid id)
        {
            //var teams = await mvcESportsDBContext.Team.(s => s.Throphy == id);
            var teams = await applicationDBContext.Team.ToListAsync();

            var teamsF = new List<Team>();
            //for (int i = 1; i < 11; i++)
            //{
            //    courses.Add(new Course()
            //    {
            //        Id = i,
            //        Name = $"Course Name ${i}",
            //        Price = 2000,
            //    });
            //}
            foreach (var word in teams)
            {
                if (word.Trophy == id)
                {
                    teamsF.Add(word);
                }
            }

            //if(admin )
            ViewBag.Trophy = id;
            return View(teamsF);
        }

        public IActionResult CreateTeam(Guid id)
        {
            ViewBag.Trophy = id;
            return View();
        }


        [HttpPost]
        public IActionResult AddTeam(AddTeam addTeamModel)
        {
            //return RedirectToAction("TeamsDashboard");
            //return View();

            var team = new Team()
            {
                Id = Guid.NewGuid(),
                Name = addTeamModel.Name,
                Trophy = addTeamModel.Trophy,
                SpendLimit = addTeamModel.SpendLimit,
                Description = addTeamModel.Description,
                Username = addTeamModel.UserName,
                Password = addTeamModel.Password
            };
            //var team = new Team()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "xxx",
            //    Throphy = Guid.NewGuid(),
            //    spend_limit = 2565444,
            //    description = "xxx",
            //    password = "xxx",
            //};

            applicationDBContext.Team.Add(team);
            applicationDBContext.SaveChanges();
            return RedirectToAction("ThrophyDashboard");
        }

        [HttpGet]
        public async Task<IActionResult> PlayersDashboard(Guid id)
        {
            //var teams = await mvcESportsDBContext.Team.(s => s.Throphy == id);
            var players = await applicationDBContext.Players.ToListAsync();

            var playersF = new List<Players>();
            //for (int i = 1; i < 11; i++)
            //{
            //    courses.Add(new Course()
            //    {
            //        Id = i,
            //        Name = $"Course Name ${i}",
            //        Price = 2000,
            //    });
            //}
            foreach (var word in players)
            {
                if (word.Trophy == id)
                {
                    playersF.Add(word);
                }
            }

            //if(admin )
            ViewBag.Trophy = id;
            return View(playersF);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePlayer(Guid id)
        {
            var player = await applicationDBContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (player != null)
            {
                var viewPlayer = new PlayerUpdate()
                {
                    Id = player.Id,
                    FisrtName = player.FisrtName,
                    LastName = player.LastName,
                    Age = player.Age,
                    Baseprice = player.Baseprice,
                    Username = player.Username,
                    Category = player.Category,
                    Price = player.Price,
                    IsActive = player.IsActive,
                    Trophy = player.Trophy,
                    Team = player.Team,
                    Password = player.Password
                };

                return View(viewPlayer);
            }

            return RedirectToAction("ThrophyDashboard");
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePlayerDetails(PlayerUpdate model)
        {
            var player = await applicationDBContext.Players.FindAsync(model.Id);
            //return RedirectToAction("TeamsDashboard");
            //return View();

            if (player != null)
            {
                player.FisrtName = model.FisrtName;
                player.LastName = model.LastName;
                player.Age = model.Age;
                player.Baseprice = model.Baseprice;
                player.Category = model.Category;
                player.Price = model.Price;
                player.IsActive = model.IsActive;
                player.Trophy = model.Trophy;
                player.Team = model.Team;
                player.Username = model.Username;


                await applicationDBContext.SaveChangesAsync();

                return RedirectToAction("ThrophyDashboard");
            }
            return RedirectToAction("ThrophyDashboard");


            {
                //Id = Guid.NewGuid(),
                //Name = addTeamModel.Name,
                //Throphy = addTeamModel.Throphy,
                //spend_limit = addTeamModel.spend_limit,
                //description = addTeamModel.description,
                //password = addTeamModel.password,
            };
            //var team = new Team()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "xxx",
            //    Throphy = Guid.NewGuid(),
            //    spend_limit = 2565444,
            //    description = "xxx",
            //    password = "xxx",
            //};

            //mvcESportsDBContext.Team.Add(team);
            //mvcESportsDBContext.SaveChanges();
            //return RedirectToAction("ThrophyDashboard");
        }

        public async Task<IActionResult> ViewBids(PlayerUpdate model)
        {
            //var teams = await mvcESportsDBContext.Team.(s => s.Throphy == id);
            var bids = await applicationDBContext.Bids.ToListAsync();

            var bidsF = new List<Bidding>();
            //for (int i = 1; i < 11; i++)
            //{
            //    courses.Add(new Course()
            //    {
            //        Id = i,
            //        Name = $"Course Name ${i}",
            //        Price = 2000,
            //    });
            //}
            foreach (var word in bids)
            {
                if (word.PlayerId == model.Id)
                {
                    bidsF.Add(word);
                }
            }

            //if(admin )
            //ViewBag.Trophy = id;
            return View(bidsF);
        }

        //[HttpPost]
        public async Task<IActionResult> CloseBid(Guid Id)
        {
            var theBid = await applicationDBContext.Bids.FindAsync(Id);

            if (theBid != null)
            {
                var theTeam = await applicationDBContext.Team.FindAsync(theBid.TeamId);
                var player = await applicationDBContext.Players.FindAsync(theBid.PlayerId);

                if (theTeam != null)
                {
                    theTeam.Spent = theTeam.Spent + theBid.Price;
                }
                if (player != null)
                {
                    player.Price = theBid.Price;
                    player.IsActive = false;
                    player.Team = theBid.TeamId;
                    player.Team = theBid.TeamName;

                    await applicationDBContext.SaveChangesAsync();

                    return RedirectToAction("ThrophyDashboard");
                }
            }

            return RedirectToAction("ThrophyDashboard");

        }


        //[HttpGet]
        //public async Task<IActionResult> Dashboard()
        //{
        //    var userData = await mvcESportsDBContext.Admin.ToListAsync();
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //     var admins = await mvcESportsDBContext.Admin.ToListAsync();
        //    return View(admins);

        //}

        //[HttpGet]
        //public async Task<IActionResult> ThrophyDashboard()
        //{
        //    var throphys = await mvcESportsDBContext.Throphy.ToListAsync();
        //    return View(throphys);

        //}


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        ////public IActionResult Add(AddAdminViewModel addAdminRequest)
        ////{
        ////    var admin = new Admin()
        ////    {
        ////        Id = Guid.NewGuid(),
        ////        Name = addAdminRequest.Name,
        ////        Email = addAdminRequest.Email,
        ////        password = addAdminRequest.password
        ////    };

        ////    mvcESportsDBContext.Admin.Add(admin);
        ////    mvcESportsDBContext.SaveChanges();
        ////    return RedirectToAction("Add");
        ////}

        public async Task<IActionResult> Add(AdminView addAdminRequest)
        {
            var admin = new Admin()
            {
                Id = Guid.NewGuid(),
                Name = addAdminRequest.UserName,
                Email = addAdminRequest.Email,
                Password = addAdminRequest.Password
            };

            await applicationDBContext.Admin.AddAsync(admin);
            await applicationDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //public IActionResult View(Guid id)
        //{
        //    var admin = mvcESportsDBContext.Admin.FirstOrDefaultAsync(x => x.Id == id);

        //    //if(admin )
        //    return View(admin);
        //}

        //public IActionResult View()
        //{
        //    //var admin = mvcESportsDBContext.Admin.FirstOrDefaultAsync(x => x.Id == id);

        //    //if(admin )
        //    return View();
        //}
    }
}
