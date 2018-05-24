using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crudular.Models;
using Crudular.Services;
using Crudular.ViewModel.Edit;
using Crudular.ViewModel.Home;

namespace Crudular.Controllers
{
    public class ItemsController : Controller
    {
        private LitsEntities db = new LitsEntities();

        // GET: Items
        public ActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();
            return View(vm);
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);

            LitsService service = new LitsService();
            //var info = service.getGrindInfo(item.BoxID);
            int boxSize = db.Boxes.Find(item.BoxID).BoxSize;
            if (item == null)
            {
                return HttpNotFound();
            }
            var itemsInBox = db.Items.Where(s => s.BoxID == item.BoxID && s.ItemID != item.ItemID).Select(s => s.ItemID);
            ViewBag.FilledLocations = db.ItemLocations.Where(r => itemsInBox.Contains(r.ItemID)).Select(s => s.Location).ToList();
            ViewBag.Locations = db.ItemLocations.Where(r => r.ItemID == item.ItemID).FirstOrDefault().Location;
            ViewBag.BoxSize = boxSize;
            ViewBag.BoxID = new SelectList(db.Boxes.GroupBy(g => g.BoxName).Select(g=> g.FirstOrDefault()), "BoxID", "BoxName", item.BoxID);
            return View(item);
        }

        public ActionResult Search(string search)
        {
            if(search.ToLower() == "hazardous")
            {
                search = true.ToString();
            }
            else if(search.ToLower() == "not hazardous")
            {
                search = false.ToString();
            }
            var records = (from a in db.Items
                    where
                        a.BoxID.ToString().Contains(search) ||
                        a.CatalogNumber.ToString().Contains(search) ||
                        a.HostSpecies.Contains(search) ||
                        a.IsHazardous.ToString().Contains(search) ||
                        a.ItemID.ToString().Contains(search) ||
                        a.ItemName.Contains(search) ||
                        a.Notes.Contains(search) ||
                        a.OrderDate.ToString().Contains(search) ||
                        a.POTSNumber.Contains(search) ||
                        a.Quantity.Contains(search) ||
                        a.Vendor.Contains(search) ||
                        a.WorkingDilution.Contains(search)||
                        a.Box.BoxName.Contains(search)
                    select a);
            IndexViewModel vm = new IndexViewModel { items = records.ToList()};
            return View("index",vm);
        }
    

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,BoxID,ItemName,OrderDate,Vendor,HostSpecies,WorkingDilution,Quantity,POTSNumber,CatalogNumber,Notes,IsHazardous")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoxID = new SelectList(db.Boxes, "BoxID", "BoxName", item.BoxID);
            return View(item);
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
