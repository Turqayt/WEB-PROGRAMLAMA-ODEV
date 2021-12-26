using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndirimKuponum.Models
{
    public class Indirimler
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Resim { get; set; }
        public string Aciklama { get; set; }
        public string Icerik { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool Onay { get; set; }
        public bool Anasayfa { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public Yorum Yorum { get; set; }

    }
}
