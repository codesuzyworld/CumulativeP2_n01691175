using CumulativeP1_n01691175.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CumulativeP1_n01691175.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

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

    }
}