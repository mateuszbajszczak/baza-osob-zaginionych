using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RejestrOsobZaginionych.Models
{
    public class Uzytkownik
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public bool Aktywny { get; set; }
        public bool Admin { get; set; }
    }

    public class UzytkownikDbContext : DbContext
    {
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
    }
}

