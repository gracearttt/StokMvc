using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            var kategoriler = db.tblkategori.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(tblkategori p)
        {
            db.tblkategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = db.tblkategori.Find(id);
            db.tblkategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategori.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult KategoriGuncelle(tblkategori p)
        {
            var ktg = db.tblkategori.Find(p.id);
            ktg.ad = p.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}