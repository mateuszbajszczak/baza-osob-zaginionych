using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RejestrOsobZaginionych.Models
{
    public class OsobaZaginiona
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Opis { get; set; }
        public string Zdjecie { get; set; }
        public int Plec { get; set; }
    }

    public class OsobaZaginionaDbContext : DbContext
    {
        public DbSet<OsobaZaginiona> OsobyZaginione { get; set; }
    }
}