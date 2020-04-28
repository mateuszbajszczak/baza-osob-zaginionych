using RejestrOsobZaginionych.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RejestrOsobZaginionych.Controllers
{
    public class OsobyZaginioneController : Controller
    {
        public OsobaZaginionaDbContext db = new OsobaZaginionaDbContext();
        public UzytkownikDbContext db2 = new UzytkownikDbContext();

        // GET: OsobyZaginione
        [LoginFilter]
        public ActionResult Index(int err = 1, int utworzono = 0)
        {
            if (Session["zalogowany"].ToString() == "1")
            {
                return RedirectToAction("List");
            }

            ViewBag.Utworzono = utworzono;            
            ViewBag.Error = err;
            return View();        
        }

        [LoginFilter]
        public ActionResult List(int plec = 0)
        {
            if (Session["zalogowany"].ToString() != "1")
            {
                return RedirectToAction("Index");
            }
            if (plec != 1 && plec != 2 && plec != 0)
            {
                plec = 0; 
            }

            ViewBag.Plec = plec;
            ViewBag.Sciezka = Path.Combine(Server.MapPath("~/zdjecia"));
            ViewBag.Admin = Session["admin"].ToString();
            return View(db.OsobyZaginione.ToList());
    }
        
        // GET: OsobyZaginione/Create
        [LoginFilter]
        public ActionResult Create()
        {
            if (Session["zalogowany"].ToString() != "1")
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: OsobyZaginione/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Opis,Plec")] OsobaZaginiona osobaZaginiona, HttpPostedFileBase plik)
        {
            if (ModelState.IsValid)
            {
                osobaZaginiona.Zdjecie = "";
                if (plik != null)
                {
                    string sciezka = Path.Combine(Server.MapPath("~/zdjecia"), Path.GetFileName(plik.FileName));
                    plik.SaveAs(sciezka);
                    osobaZaginiona.Zdjecie = plik.FileName;
                }
                db.OsobyZaginione.Add(osobaZaginiona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osobaZaginiona);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logowanie([Bind(Include = "Email,Haslo")] Uzytkownik uzytkownik)
        {
            int error = 1;
            if (ModelState.IsValid)
            {
                bool prawidlowyEmail = db2.Uzytkownicy.Any(x => x.Email == uzytkownik.Email);
                if (prawidlowyEmail)
                {
                    if (uzytkownik.Haslo != null)
                    {
                        var uzytkownikzbazy = db2.Uzytkownicy.Where(x => x.Email == uzytkownik.Email)
                            .Single();

                        bool prawidloweHaslo = Crypto.VerifyHashedPassword(uzytkownikzbazy.Haslo, uzytkownik.Haslo);

                        if (prawidloweHaslo && uzytkownikzbazy.Aktywny)
                        {
                            Session["UzytkownikID"] = uzytkownikzbazy.ID;
                        }
                        else
                        {
                            error = -1;
                        }
                    } else
                    {
                        error = -1;
                    }

                }
                else
                {
                    error = 0;
                }
            }
            return RedirectToAction("Index", new { err = error});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja([Bind(Include = "Email,Haslo")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid && uzytkownik.Email != null && uzytkownik.Haslo != null)
            {
                uzytkownik.Admin = false;
                uzytkownik.Aktywny = false;
                uzytkownik.Haslo = Crypto.HashPassword(uzytkownik.Haslo);
                db2.Uzytkownicy.Add(uzytkownik);
                db2.SaveChanges();
                return RedirectToAction("Index", new { utworzono = 1 });
            } 
            else
            {
                return RedirectToAction("Index", new { utworzono = -1 });
            }

            return View(uzytkownik);
        }

        public ActionResult Wyloguj()
        {
            Session.Remove("UzytkownikID");
            Session.Remove("zalogowany");
            Session.Remove("aktywny");
            Session.Remove("admin");
            Session.Remove("test");
            return RedirectToAction("Index");
        }
    }
}