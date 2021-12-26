using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IndirimKuponum.Data;

namespace IndirimKuponum.Models
{
    public class IndirimlerContext:DbContext
    {
        public IndirimlerContext() : base("IndirimKuponumDB")
        {
            Database.SetInitializer(new IndirimlerInitializer());
        }
        public DbSet<Indirimler> Indirim { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }


    }
}
