using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities(); 
        public ActionResult Index()
        {
            var urunler = db.tblurunler.Where(x => x.durum == true).ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler t)
        {
            var ktgr = db.tblkategori.Where(x => x.id == t.tblkategori.id).FirstOrDefault();
            t.tblkategori = ktgr;
            db.tblurunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            var ktgr = db.tblurunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }

        public ActionResult UrunGuncelle(tblurunler p)
        {
            var urun = db.tblurunler.Find(p.id);
            urun.ad = p.ad;
            urun.marka = p.marka;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.satisfiyat = p.satisfiyat;
            var ktg = db.tblkategori.Where(x => x.id == p.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(tblurunler p)
        {
            var urunbul = db.tblurunler.Find(p.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}