using RejestrOsobZaginionych.Models;
using System.Linq;
using System.Web.Mvc;

public class LoginFilter : ActionFilterAttribute
{

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        string zalogowany = "0";
        string admin = "0";
        string aktywny = "0";

        if (filterContext.HttpContext.Session["UzytkownikID"] != null)
        {
            UzytkownikDbContext dbFilter = new UzytkownikDbContext();
            int id = int.Parse(filterContext.HttpContext.Session["UzytkownikID"].ToString());

            Uzytkownik uzytkownik = dbFilter.Uzytkownicy.Find(id);

            if (uzytkownik != null)
            {
                zalogowany = "1";
                if (uzytkownik.Admin)
                {
                    admin = "1";
                }
                if (uzytkownik.Aktywny)
                {
                    aktywny = "1";
                }
            }
        }
        
        filterContext.HttpContext.Session["zalogowany"] = zalogowany;
        filterContext.HttpContext.Session["admin"] = admin;
        filterContext.HttpContext.Session["aktywny"] = aktywny;
        base.OnActionExecuting(filterContext);
    }
}