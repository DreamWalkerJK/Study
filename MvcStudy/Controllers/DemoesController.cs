using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcStudy.Models;

namespace MvcStudy.Controllers
{
    public class DemoesController : Controller
    {
        private MvcStudyDbContext db = new MvcStudyDbContext();

        // GET: Demoes
        public ActionResult Index(string demoColor, string searchString)
        {
            var demoColorLst = Enum.GetNames(typeof(ColorType));

            ViewBag.demoColor = new SelectList(demoColorLst);

            var demoes = from d in db.Demoes 
                         select d;

            if (!string.IsNullOrEmpty(demoColor))
            {
                int demoColorValue = (int)Enum.Parse(typeof(ColorType), demoColor);
                demoes = demoes.Where(d => (int)d.DemoColor == demoColorValue);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                demoes = demoes.Where(d => d.DemoName.Contains(searchString));
            }

            return View(demoes);

            //return View(db.Demoes.ToList());
        }

        // GET: Demoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demoes.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            return View(demo);
        }

        // GET: Demoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Demoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DemoId,DemoName,DemoOther,DemoPrice,DemoColor,DemoDate")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                db.Demoes.Add(demo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demo);
        }

        // GET: Demoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demoes.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            return View(demo);
        }

        // POST: Demoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DemoId,DemoName,DemoOther,DemoPrice,DemoColor,DemoDate")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(demo);
        }

        // GET: Demoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demo demo = db.Demoes.Find(id);
            if (demo == null)
            {
                return HttpNotFound();
            }
            return View(demo);
        }

        // POST: Demoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demo demo = db.Demoes.Find(id);
            db.Demoes.Remove(demo);
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
    }
}
