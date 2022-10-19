using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MvcStudy.DAL;
using MvcStudy.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;
using System.Data;

namespace MvcStudy.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Department
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Instructor);
            return View(await departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string query = "select * from Department where DepartmentId = @p0";
            Department department = await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync();

            //Department department = await db.Departments.FindAsync(id);

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.InstructorId = new SelectList(db.Instructors, "Id", "Name");
            return View();
        }

        // POST: Department/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentId,DepartmentName,Budget,StartDate,InstructorId")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorId = new SelectList(db.Instructors, "Id", "Name", department.InstructorId);
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorId = new SelectList(db.Instructors, "Id", "Name", department.InstructorId);
            return View(department);
        }

        // POST: Department/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldToBind = new string[]
            {
                "DepartmentName",
                "Budget",
                "StartDate",
                "InstructorId",
                "RowVersion"
            };

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departmentToUpdate = await db.Departments.FindAsync(id);
            if(departmentToUpdate == null)
            {
                Department deletedDepartment = new Department();
                TryUpdateModel(deletedDepartment, fieldToBind);
                ModelState.AddModelError(string.Empty,
                    "the department was deleted by another user.");
                ViewBag.InstructorId = new SelectList(db.Instructors, "Id", "Name", deletedDepartment.InstructorId);
                return View(deletedDepartment);
            }

            if(TryUpdateModel(departmentToUpdate, fieldToBind))
            {
                try
                {
                    db.Entry(departmentToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Department)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();

                    if(databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();

                        if(databaseValues.DepartmentName != clientValues.DepartmentName)
                        {
                            ModelState.AddModelError(string.Empty, "Current value:" +
                                databaseValues.DepartmentName);
                        }

                        if (databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", "Current value: " + String.Format("{0:c}", databaseValues.Budget));
                        }

                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("StartDate", "Current value: " + String.Format("{0:d}", databaseValues.StartDate));
                        }

                        if (databaseValues.InstructorId != clientValues.InstructorId)
                        {
                            ModelState.AddModelError("InstructorId", "Current value: " + db.Instructors.Find(databaseValues.InstructorId).Name);
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError(string.Empty, "unable to save, try again.");
                }
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(department).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            ViewBag.InstructorId = new SelectList(db.Instructors, "Id", "Name", departmentToUpdate.InstructorId);
            return View(departmentToUpdate);
        }

        // GET: Department/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Department department)
        {
            try
            {
                db.Entry(department).State = EntityState.Deleted;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = department.DepartmentId });
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again.");
                return View(department);
            }
            //Department department = await db.Departments.FindAsync(id);
            //db.Departments.Remove(department);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
