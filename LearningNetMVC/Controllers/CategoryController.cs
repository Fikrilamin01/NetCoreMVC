using LearningNetMVC.Data;
using LearningNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LearningNetMVC.Controllers
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
            IEnumerable<Category> objCategoryList = _db.categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //validate if model is valid to be save in the database
            //server side validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exatcly match the name");
            }

            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                //the format for RedirectToAction function
                //RedirectToAction("[ActionName]","[ControllerName]")

                //Temporary store data for that request
                TempData["sucess"] = "Category created sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id < 1 || Id > 100)
            {
                return NotFound();
            }

            //return one element from the db and throw exception if the element is not found
            //var category = _db.categories.Single();
            //var category = _db.categories.First();

            //return one element from the db and throw exception if there more than one element
            //if element is not found, db will return empty
            //var categorySingle = _db.categories.SingleOrDefault(u=>u.Id==Id);

            //return one element from the db and if there is more element than one
            //it will not throw exception, instead will return the first element found
            //var categoryFirst = _db.categories.FirstOrDefault(u=>u.Id==Id);

            //find element in the table based on the primary key of the table
            var categoryFromDb = _db.categories.Find(Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //validate if model is valid to be save in the database
            //server side validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exatcly match the name");
            }

            if (ModelState.IsValid)
            {
                obj.UpdatedDateTime = DateTime.Now;
                _db.categories.Update(obj);
                _db.SaveChanges();

                //the format for RedirectToAction function
                //RedirectToAction("[ActionName]","[ControllerName]")

                //Temporary store data for that request
                TempData["sucess"] = "Category updated sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id < 1 || Id > 100)
            {
                return NotFound();
            }

            //return one element from the db and throw exception if the element is not found
            //var category = _db.categories.Single();
            //var category = _db.categories.First();

            //return one element from the db and throw exception if there more than one element
            //if element is not found, db will return empty
            //var categorySingle = _db.categories.SingleOrDefault(u=>u.Id==Id);

            //return one element from the db and if there is more element than one
            //it will not throw exception, instead will return the first element found
            //var categoryFirst = _db.categories.FirstOrDefault(u=>u.Id==Id);

            //find element in the table based on the primary key of the table
            var categoryFromDb = _db.categories.Find(Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.categories.Remove(obj);
            _db.SaveChanges();
            //Temporary store data for that request
            TempData["sucess"] = "Category deleted sucessfully";
            return RedirectToAction("Index");
        }

    }
}
