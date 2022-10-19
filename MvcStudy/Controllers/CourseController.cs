using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcStudy.DAL;
using MvcStudy.Models;

namespace MvcStudy.Controllers
{
    public class CourseController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Course
        public ActionResult Index(int? SelectedDepartment)
        {
            //var courses = db.Courses.Include(c => c.Department);
            //return View(courses.ToList());

            //var courses = db.Courses;
            //var sql = courses.ToString();
            //return View(courses.ToList());

            var departments = db.Departments.OrderBy(d => d.DepartmentName).ToList();
            ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentId", "DepartmentName", SelectedDepartment);
            int departmentId = SelectedDepartment.GetValueOrDefault();

            IQueryable<Course> courses = db.Courses
                .Where(c => !SelectedDepartment.HasValue || c.DepartmentId == departmentId)
                .OrderBy(d => d.CourseId)
                .Include(d => d.Department);

            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            //ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Course/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,CourseCredits,DepartmentId")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Falid, Try again");
            }

            PopulateDepartmentsDropDownList(course.DepartmentId);

            //ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);

            PopulateDepartmentsDropDownList(course.DepartmentId);

            return View(course);
        }

        // POST: Course/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseToUpdate = db.Courses.Find(id);
            if(TryUpdateModel(courseToUpdate, "",
                new string[] { "CourseName", "CourseCredits", "DepartmentId" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Failed, Try again.");
                }
            }

            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentId);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(course).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);

            return View(courseToUpdate);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Course SET CourseCredits = {0}", multiplier);
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentQuery = from d in db.Departments
                                  orderby d.DepartmentName
                                  select d;

            ViewBag.DepartmentId = new SelectList(departmentQuery, "DepartmentId", "DepartmentName", selectedDepartment);
        }
    }
}
