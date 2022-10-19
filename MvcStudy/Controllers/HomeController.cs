using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcStudy.DAL;
using MvcStudy.ViewModels;

namespace MvcStudy.Controllers
{
    public class HomeController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //IQueryable<EnrollmentDateGroup> data = from s in db.Students
            //                                       group s by s.EnrollmentDate into dateGroup
            //                                       select new EnrollmentDateGroup()
            //                                       {
            //                                           EnrollmentDate = dateGroup.Key,
            //                                           StudentCount = dateGroup.Count()
            //                                       };

            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                            + "FROM Person "
                            + "WHERE Discriminator = 'Student' "    
                            + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}