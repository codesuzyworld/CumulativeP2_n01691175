using CumulativeP1_n01691175.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CumulativeP1_n01691175.Controllers
{
    public class TeacherController : Controller
    {
        [HttpGet]
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //GET : /Teacher/List
        public ActionResult List(string SearchKey)
        {
            //Work with Teacher Data Controller
            TeacherDataController controller = new TeacherDataController();

            //Call List Teacher method
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            //Views/Article/List.cshtml
            return View(Teachers);
        }

        [HttpGet]
        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            //Work with Teacher Data Controller
            TeacherDataController controller = new TeacherDataController();

            //Call to Find Teacher Method
            Teacher selectedTeacher = controller.FindTeacher(id);

            //Views/Article/Show.cshtml
            return View(selectedTeacher);
        }

        public ActionResult New()
        {
            return View();

        }

        [HttpPost]
        //POST: /Teacher/Create
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, double Salary)
        {
            Debug.WriteLine("I have accessed the Create Method!");

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController Controller = new TeacherDataController(); 
            Controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");

        }


        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            //Find the teacher that the user is deleting, and show to user to confirm
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("Goodbye teacher, the delete method is here");

            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }


    }
}