using E_Sports.Data;
using E_Sports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E_Sports.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDBContext applicationDBContext;
        public Guid SelectedTeam { get; set; }
        public string test { get; set; }

        public TeamController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        public IActionResult TeamLogin()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoginTo(TeamLogin teamLogin)
        {
            var team = await applicationDBContext.Team.FirstOrDefaultAsync(x => (x.Username == teamLogin.UserName) && (x.Password == teamLogin.Password));

            var loginModel = new TeamLogin();
            if (team == null)
            {
                return RedirectToAction("LoginError");

            }
            else
            {
                //SelectedTeam = team.Id;
                ViewBag.Throphy = team.Trophy;
                ViewBag.TeamName = team.Name;
                ViewBag.TeamId = team.Id;
                return View();
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


        [HttpGet]
        public async Task<IActionResult> PlayersForBid(Guid Id)
        {
            var theTeam = await applicationDBContext.Team.FindAsync(Id);
            if (theTeam != null)
            {
                var players = await applicationDBContext.Players.ToListAsync();

                var playersF = new List<Players>();

                foreach (var word in players)
                {
                    if ((word.Trophy == theTeam.Trophy) && (word.IsActive == true))
                    {
                        playersF.Add(word);
                    }
                }

                //if(admin )
                ViewBag.TeamName = theTeam.Name;
                ViewBag.TeamId = theTeam.Id;
                ViewBag.CanSpend = theTeam.SpendLimit - theTeam.Spent;
                return View(playersF);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ViewAllBid(Guid Id)
        {
            //var teams = await mvcESportsDBContext.Team.(s => s.Throphy == id);
            var bids = await applicationDBContext.Bids.ToListAsync();

            var bidsF = new List<Bidding>();

            foreach (var word in bids)
            {
                if (word.PlayerId == Id)
                {
                    bidsF.Add(word);
                }
            }

            //if(admin )
            //ViewBag.Trophy = id;
            return View(bidsF);
        }

        //[HttpGet("{id}/{teamId}")]
        [HttpGet]
        //[Route("/{id}/{teamId}")]
        public async Task<IActionResult> MakeABid(Guid Id)
        {
            var theTeam = await applicationDBContext.Team.FindAsync(Id);
            if (theTeam != null)
            {
                ViewBag.TeamName = theTeam.Name;
                ViewBag.TeamId = theTeam.Id;
                ViewBag.CanSpend = theTeam.Spent - theTeam.Spent;
            }

            return View();
        }

        //CallTheBid
        [HttpPost]
        public async Task<IActionResult> CallTheBid(AddBid addBidModel)
        {
            var thePlayer = await applicationDBContext.Players.FindAsync(addBidModel.PlayerId);
            if (thePlayer != null)
            {
                if (addBidModel.CanSpend < addBidModel.Price)
                {
                    return RedirectToAction("Limit");
                }
                else
                {
                    var bid = new Bidding()
                    {
                        Id = Guid.NewGuid(),
                        PlayeName = thePlayer.FisrtName,

                        PlayerId = thePlayer.Id,
                        TeamId = addBidModel.TeamId,
                        TeamName = addBidModel.TeamName,
                        Price = addBidModel.Price
                    };

                    await applicationDBContext.Bids.AddAsync(bid);
                    await applicationDBContext.SaveChangesAsync();
                    return RedirectToAction("Success");
                }
            }
            return RedirectToAction("Wrong");

        }

        public IActionResult Limit()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        //ShowMyPlayers
        public async Task<IActionResult> ShowMyPlayers(Guid Id)
        {
            //var teams = await mvcESportsDBContext.Team.(s => s.Throphy == id);
            var players = await applicationDBContext.Players.ToListAsync();

            var playersF = new List<Players>();

            foreach (var word in players)
            {
                if (word.Team == Id)
                {
                    playersF.Add(word);
                }
            }

            //if(admin )
            //ViewBag.Trophy = id;
            return View(playersF);
        }
    }
}
