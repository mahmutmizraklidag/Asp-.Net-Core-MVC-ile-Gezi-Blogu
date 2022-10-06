using System.ComponentModel.DataAnnotations;

namespace GeziBlogu.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Display(Name="İsim"),StringLength(50),Required(ErrorMessage ="Bu alan Gereklidir")]
        public string Name { get; set; }
        [Display(Name = "Soyisim"), StringLength(50), Required(ErrorMessage = "Bu alan Gereklidir")]
        public string Surname { get; set; }
        [StringLength(50), Required(ErrorMessage = "Bu alan Gereklidir"),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Telefon"), DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Display(Name = "Mesaj"), StringLength(500), Required(ErrorMessage = "Bu alan Gereklidir"),DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
