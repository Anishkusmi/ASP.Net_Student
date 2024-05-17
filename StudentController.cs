using StudentCURD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCURD.Controllers
{
    public class StudentController : Controller
    {
        StudentContext db = new StudentContext();
        public ActionResult IndexSearch(string searchString)
        {
            var data = db.Students.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                var results = db.Students.Where(u => u.Name.Contains(searchString));
                return View(results);
            }

            return View(data);
        }
        // GET: Student
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Create(Student std)
        {
            if(ModelState.IsValid)
            {
                db.Students.Add(std);
                db.SaveChanges();
                TempData["success"] = "Data Inster Successfully!!";
                //TempData["success"] = "<script>alert('Data Inster Successfully!!')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                // ViewBag.Key = "<script>alert('Error')</script>"
                ViewBag.error = "Data not Insert";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();  
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Student std)
        {
            if(ModelState.IsValid)
            {
                db.Entry(std).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.success = "Success!!";
                    ViewBag.success = "<script>alert('Success')</script>";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "<script>alert('Error')</script>";
                }
            }         
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id) 
        {
            if (id > 0)
            {
                var delStd = db.Students.Where(model => model.Id == id).FirstOrDefault();
                if (delStd != null)
                {
                    db.Entry(delStd).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["success"] = "Successfully deleted";
                        //TempData["success"] = "<script>alert('Successfully deleted')</script>";
                    }
                    else
                    {
                        TempData["error"] = "<script>alert('Not Success deleted')</script>";
                    }
                } 
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var detail = db.Students.Where(model => model.Id == id).FirstOrDefault();   
            return View(detail);
        }
    }
}