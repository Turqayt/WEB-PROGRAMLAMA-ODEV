using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IndirimKuponum.Models;

namespace IndirimKuponum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IndirimKuponum.Models.Kategori> Kategori { get; set; }
        public DbSet<IndirimKuponum.Models.Indirimler> Indirimler { get; set; }
        public DbSet<IndirimKuponum.Models.Yorum> Yorum { get; set; }
    }
}
