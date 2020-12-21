using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCS.WebUI.Models
{
    public class SinavModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sınav Başlık Bilgisi Giriniz.")]
        [Display(Name = "Sınav Başlığı")]        
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Sınav Başlangıç Tarihini Giriniz.")]
        [Display(Name = "Sınav Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [Required(ErrorMessage = "Sınav Başlangıç Tarihini Giriniz.")]
        [Display(Name = "Sınav Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }

        [Required(ErrorMessage = "Sınav İçerik Bilgisi Giriniz.")]
        [Display(Name = "Sınav İçeriği")]
        public string Icerik { get; set; }
    }
}
