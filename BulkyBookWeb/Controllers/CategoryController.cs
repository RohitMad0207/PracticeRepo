using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _dbContext.Categories;
            return View(CategoryList);
        }

        //Get
        public IActionResult Create()
        {
            
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category objCategory)
        {
            if (objCategory.Name == objCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder can not be same");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(objCategory);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Created successfully.";
                return RedirectToAction("Index");
            }
            return View(objCategory);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _dbContext.Categories.Find(id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category objCategory)
        {
            if (objCategory.Name == objCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder can not be same");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(objCategory);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(objCategory);
        }

        
        public IActionResult Delete( int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _dbContext.Categories.Find(Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }


            _dbContext.Categories.Remove(categoryFromDb);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
