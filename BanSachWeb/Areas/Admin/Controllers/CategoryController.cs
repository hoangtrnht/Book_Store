using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanSachWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The name must not same displayoder");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Category Create Sucessfully";
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
            //var categoryfromDb = _db.Categories.Find(id);
            var categoryfromDbFrist = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            var categoryfromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //var categoryfromDbFrist = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryfromDbFristSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //var categoryfromDbFrist = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryfromDbFristSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (obj.Name == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Category Dalete Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
