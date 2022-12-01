using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StokMvc.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            // var musteriliste = db.tblmusteri.ToList();
            var musteriliste = db.tblmusteri.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriliste);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri p)
        {
            p.durum = true;
            db.tblmusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(tblmusteri p)
        {
            var musteribul = db.tblmusteri.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.tblmusteri.Find(id);
            return View("MusteriGetir", mus);
;       }

        public ActionResult MusteriGuncelle(tblmusteri p)
        {
            var mus = db.tblmusteri.Find(p.id);
            mus.ad = p.ad;
            mus.soyad = p.soyad;
            mus.sehir = p.sehir;
            mus.bakiye = p.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}