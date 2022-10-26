using E_Sports.Data;
using E_Sports.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace E_Sports.Controllers
{
    public class TrophyController : Controller
    {
        private readonly ApplicationDBContext _db;
        public TrophyController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           IEnumerable<Trophys> objTrophyList = _db.Trophies;
            return View(objTrophyList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trophys obj)
        {
            if (ModelState.IsValid)
            {
                _db.Trophies.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "New Trophy created successfully";
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
            var TrophiesFromDb = _db.Trophies.Find(id);

            if (TrophiesFromDb == null)
            {
                return NotFound();
            }
            return View(TrophiesFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Trophys obj)
        {
            if (ModelState.IsValid)
            {
                _db.Trophies.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Trophies updated successfully";

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
            var TrophiesFromDb = _db.Trophies.Find(id);
            if (TrophiesFromDb == null)
            {
                return NotFound();
            }
            return View(TrophiesFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Trophies.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Trophies.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Trophies Deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
