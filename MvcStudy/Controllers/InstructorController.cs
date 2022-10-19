using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcStudy.DAL;
using MvcStudy.Models;
using MvcStudy.ViewModels;

namespace MvcStudy.Controllers
{
    public class InstructorController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Instructor
        public ActionResult Index(int? id, int? courseId)
        {
            var viewModel = new InstructorIndexData();
            // eager loading
            viewModel.Instructors = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.Name);

            if(id != null)
            {
                // lazy loading
                ViewBag.InstructorId = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(
                    i => i.Id == id.Value).Single().Courses;
            }

            if(courseId != null)
            {
                ViewBag.CourseId = courseId.Value;
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    c => c.CourseId == courseId.Value).Single().Enrollments;

                var selectedCourse = viewModel.Courses.Where(c => c.CourseId == courseId).Single();
                // explict loading
                // entities : Collection.Load()
                db.Entry(selectedCourse).Collection(c => c.Enrollments).Load();
                foreach(Enrollment enroll in selectedCourse.Enrollments)
                {
                    // entity : Reference.Load()
                    db.Entry(enroll).Reference(e => e.Student).Load();
                }

                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
            //var instructors = db.Instructors.Include(i => i.OfficeAssignment);
            //return View(instructors.ToList());
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            //ViewBag.InstructorId = new SelectList(db.OfficeAssignments, "InstructorId", "Location");

            var instructor = new Instructor();
            instructor.Courses = new List<Course>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructor/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,HireDate,OfficeAssignment")] Instructor instructor, string[] selectedCourses)
        {
            if(selectedCourses != null)
            {
                instructor.Courses = new List<Course>();
                foreach(var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    instructor.Courses.Add(courseToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCourseData(instructor);
            //ViewBag.InstructorId = new SelectList(db.OfficeAssignments, "InstructorId", "Location", instructor.Id);
            return View(instructor);
        }

        // GET: Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // eager loading
            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.Id == id)
                .Single();

            PopulateAssignedCourseData(instructor);
            //Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            //ViewBag.InstructorId = new SelectList(db.OfficeAssignments, "InstructorId", "Location", instructor.Id);
            return View(instructor);
        }

        // POST: Instructor/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructorToUpdate = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.Id == id)
                .Single();

            if(TryUpdateModel(instructorToUpdate, "",
                new string[] { "Name", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdataInstructorCourses(selectedCourses, instructorToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Failed, Try again.");
                }
            }

            PopulateAssignedCourseData(instructorToUpdate);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(instructor).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.InstructorId = new SelectList(db.OfficeAssignments, "InstructorId", "Location", instructor.Id);
            return View(instructorToUpdate);
        }

        // GET: Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Instructor instructor = db.Instructors.Find(id);
            //db.Instructors.Remove(instructor);

            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Where(i => i.Id == id)
                .Single();

            db.Instructors.Remove(instructor);

            var department = db.Departments
                .Where(d => d.InstructorId == id)
                .SingleOrDefault();

            if(department  != null)
            {
                department.InstructorId = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = db.Courses;
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseId));
            var viewModel = new List<AssignedCourseData>();

            foreach(var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    Assigned = instructorCourses.Contains(course.CourseId)
                });
            }

            ViewBag.Courses = viewModel;
        }

        private void UpdataInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if(selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectCoursesHs = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseId));

            foreach(var course in db.Courses)
            {
                if (selectCoursesHs.Contains(course.CourseId.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseId))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseId))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
    }
}
