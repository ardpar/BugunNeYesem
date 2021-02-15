using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen Adınızı giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Soy Adınızı giriniz.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi Tekrar giriniz.")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "E-Posta ")]
        public string Mail { get; set; }

    }
}
