using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndirimKuponum.Models
{
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }
        public string KullaniciAdi { get; set; }
        public string YorumText { get; set; }

        public int IndirimId { get; set; }
    }
}
