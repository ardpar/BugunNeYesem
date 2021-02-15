using BugunNeYesem.Business.Abstract;
using BugunNeYesem.DataLayer;
using BugunNeYesem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserManager _userManager;

        public LoginController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var q = _userManager.Login(model);
                if (q)
                {
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("01", "Yanlış Kullanıcı Adı Veya Şifre");
                }
            }
            return View("index", model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.RePassword)
                {
                    ModelState.AddModelError(nameof(model.Password), "Şifreler birbirini tutmuyor.");
                }
                else
                {

                    var q = _userManager.Register(model);
                    if (q == 0)
                    {
                 return        Redirect("/login");
                    }
                    else if (q == 1)
                    {
                        ModelState.AddModelError(nameof(model.Mail), "Aynı Mail Kullanımda.");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(model.Name), "Tanımlanmayan Hata Mevcut.");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LostPassword()
        {
            LostPasswordViewModel model = new LostPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LostPassword(LostPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var q = _userManager.LostPassword(model);
                if (q == 0)
                {
                    ModelState.AddModelError(nameof(model.Mail), "Yeni Şifreniz Mail Adresinize gönderildi.");
                }
                else if (q == 1)
                {
                    ModelState.AddModelError(nameof(model.Mail), "Mail Bulunamadı.");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Mail), "Tanımlanmayan Hata Mevcut.");
                }
            }
            return View(model);
        }

    }
}
