using BanSach.DataAcess.Data;
using BanSach.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanSachWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The name must not same displayoder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Create Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0) 
            {
                return NotFound();
            }
            //var categoryfromDb = _db.Categories.Find(id);
            var categoryfromDbFrist = _db.Categories.FirstOrDefault(c => c.Name == "id");
            //var categoryfromDbFristSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryfromDbFrist == null) 
            {
                return NotFound();
            }
            return View(categoryfromDbFrist);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The name must not same displayoder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Update Sucessfully";
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
            var categoryfromDb = _db.Categories.Find(id);
            //var categoryfromDbFrist = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryfromDbFristSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        //post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            //var categoryfromDbFrist = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryfromDbFristSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (obj.Name == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Dalete Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
