using CrudUsingCore.DbCtx;
using CrudUsingCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudUsingCore.Controllers
{
    public class NameController : Controller
    {
        private readonly DataAccess _db;
        public NameController(DataAccess db)
        {
            _db = db;   
        }

        /*public IActionResult Index()
        {
            var res = _db.Students.ToList();

            return View(res);
        }

        public IActionResult Delete(int id)
        {
            var res = _db.Students.Where(f => f.Id == id).FirstOrDefault();
            if(res != null)
            {
                _db.Students.Remove(res);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Name = "e";
            var res = _db.Students.Where(f => f.Id == id).FirstOrDefault();
            if(res != null)
            {
                return View("AddOrEdit", res);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddOrEdit()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddOrEdit(Student std)
        {
            ViewBag.Name = "f";
            if (std.Id == 0)
            {
                _db.Students.Add(std);
                _db.SaveChanges();
            }
            else
            {
                _db.Update(std);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }*/

        public IActionResult Index()
        {
            var students = _db.Students.ToList();
            return View(students);
        }
        public IActionResult Edit(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                var serializedStudent = JsonConvert.SerializeObject(student);
                TempData["StudentData"] = serializedStudent;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AddOrEdit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    _db.Students.Add(student);
                }
                else
                {
                    _db.Students.Update(student);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            // If model state is not valid, return to the Index view with the validation errors
            return View("Index", student);
        }

        public IActionResult Delete(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
