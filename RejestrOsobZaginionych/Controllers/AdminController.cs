using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RejestrOsobZaginionych.Models;

namespace RejestrOsobZaginionych.Controllers
{
    public class AdminController : Controller
    {
        private UzytkownikDbContext db = new UzytkownikDbContext();

        // GET: Admin
        [LoginFilter]
        public ActionResult Index()
        {
            if (Session["zalogowany"].ToString() != "1" || Session["admin"].ToString() != "1")
            {
                return RedirectToAction("List", "OsobyZaginione");
            }
            return View(db.Uzytkownicy.ToList());
        }

        // GET: Admin/Details/5
        [LoginFilter]
        public ActionResult Details(int? id)
        {
            if (Session["zalogowany"].ToString() != "1" || Session["admin"].ToString() != "1")
            {
                return RedirectToAction("List", "OsobyZaginione");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: Admin/Create
        [LoginFilter]
        public ActionResult Create()
        {
            if (Session["zalogowany"].ToString() != "1" || Session["admin"].ToString() != "1")
            {
                return RedirectToAction("List", "OsobyZaginione");
            }
            return View();
        }

        // POST: Admin/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Haslo,Aktywny,Admin")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                uzytkownik.Haslo = Crypto.HashPassword(uzytkownik.Haslo);
                db.Uzytkownicy.Add(uzytkownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uzytkownik);
        }

        // GET: Admin/Edit/5
        [LoginFilter]
        public ActionResult Edit(int? id)
        {
            if (Session["zalogowany"].ToString() != "1" || Session["admin"].ToString() != "1")
            {
                return RedirectToAction("List", "OsobyZaginione");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Admin/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Haslo,Aktywny,Admin")] Uzytkownik uzytkownik, string HasloZmienione)
        {
            if (ModelState.IsValid)
            {
                if (HasloZmienione == "1")
                {
                    uzytkownik.Haslo = Crypto.HashPassword(uzytkownik.Haslo);
                }

                db.Entry(uzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uzytkownik);
        }

        // GET: Admin/Delete/5
        [LoginFilter]
        public ActionResult Delete(int? id)
        {
            if (Session["zalogowany"].ToString() != "1" || Session["admin"].ToString() != "1")
            {
                return RedirectToAction("List", "OsobyZaginione");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            db.Uzytkownicy.Remove(uzytkownik);
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
