using E_Sports.Data;
using E_Sports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace E_Sports.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDBContext _db;
        public string user;
        public Guid SelectedThrophy;
        public PlayersController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult PlayerLogin()
        {
            return View();
        }
        public IActionResult Register(Guid Id)
        {
            SelectedThrophy = Id;
            ViewBag.throphy = Id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var trophys = await _db.Trophies.ToListAsync();
            return View(trophys);

        }

        //RegisterPlayer

        [HttpGet]
        public async Task<IActionResult> LoginTo(AdminLogin adminLogin)
        {
            var admin = await _db.Players.FirstOrDefaultAsync(x => (x.Username == adminLogin.UserName) && (x.Password == adminLogin.Password));

            var loginModel = new AdminLogin();
            if (admin == null)
            {
                return RedirectToAction("LoginError");

            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public IActionResult RegisterPlayer(PlayerRegister registerPlayerModel)
        {
            var player = new Players()
            {
                Id = Guid.NewGuid(),
                FisrtName = registerPlayerModel.FisrtName,
                LastName = registerPlayerModel.LastName,
                Age = registerPlayerModel.Age,
                Baseprice = registerPlayerModel.Baseprice,
                Category = registerPlayerModel.Category,
                Price = 0,
                IsActive = false,
                Trophy = registerPlayerModel.Trophy,
                Team = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                TeamName = "",
                Username = registerPlayerModel.Username,
                Password = registerPlayerModel.Password,
            };

            _db.Players.Add(player);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        public IActionResult Index()
        {
            IEnumerable<Players> objPlayersList = _db.Players;
            return View(objPlayersList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Players obj)
        {
            if (ModelState.IsValid)
            {
                _db.Players.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "New Player created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var PlayersFromDb = _db.Players.Find(id);

            if (PlayersFromDb == null)
            {
                return NotFound();
            }
            return View(PlayersFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Players obj)
        {
            if (ModelState.IsValid)
            {
                _db.Players.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Player updated successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var PlayersFromDb = _db.Players.Find(id);
            if (PlayersFromDb == null)
            {
                return NotFound();
            }
            return View(PlayersFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Players.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Players.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Player Deleted successfully";
            return RedirectToAction("Index");

        }

        //new ad


    }
}
