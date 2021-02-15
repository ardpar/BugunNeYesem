using BugunNeYesem.Business.Abstract;
using BugunNeYesem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILocationManager _locationManager;
        private readonly IMainManager _mainManager;
        private readonly IUserManager _userManager;
        public HomeController(
                           ILocationManager locationManager,
                           IMainManager mainManager,
                           IUserManager userManager
                        )
        {
            _locationManager = locationManager;
            _mainManager = mainManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            CalcData data= _mainManager.GetData();
            return View(data);
        }
        public IActionResult Logut()
        {
            _userManager.SignOut();
            return Redirect("/");
        }
        public IActionResult Profile()
        {
            UserEditProfileViewModel q = _userManager.GetProfile();
            return View(q);
        }

        [HttpPost]
        public IActionResult Profile(UserEditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var q = _userManager.SaveProfile(model);
                if (q != 0)
                {
                    ModelState.AddModelError(nameof(model.Name), "Bir Hata Oluştu");
                }
                else {
                    return Redirect("/");
                }
            }
            return View(model);
        }


        public IActionResult LocationList()
        {
            List<LocationListItemViewModel> q = _locationManager.GetList();
            return View(q);
        }

        public IActionResult LocationDelete(int id)
        {
            _locationManager.SetDelete(id);
            return RedirectToAction("LocationList");
        }

        public IActionResult Location(int id)
        {
            LocationEditViewModel q =  _locationManager.GetEdit(id);
            return View(q);
        }

        [HttpPost]
        public IActionResult Location(LocationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_locationManager.SetEdit(model) > 0)
                {
                    ModelState.AddModelError(nameof(model.Name), "Tanımlanmayan hata oluştu");
                }
                else {
                    return RedirectToAction("LocationList");
                }
            }
            return View(model);
        }


        public IActionResult UserList()
        {
            List<UserListItemViewModel> list = _userManager.GetList();
            return View(list);
        }
        public IActionResult User(int id)
        {
            UserEditViewModel model= _userManager.GetUser(id); 
            return View(model);
        }
        [HttpPost]
        public IActionResult User(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.SaveUser(model) > 0)
                {
                    ModelState.AddModelError(nameof(model.Name), "Tanımlanmayan hata oluştu");
                }
                else {
                    return RedirectToAction("UserList");
                }
            }
            return View(model);
        }
        public IActionResult UserDelete(int id)
        {
            _userManager.DeleteUser(id);
            return RedirectToAction("UserList");
        }
        public IActionResult SetEatCardStatus()
        {
            _mainManager.SetEatCartStatus();
            return RedirectToAction("index");
        }
        public IActionResult GetNewCalcLocation()
        {
            _mainManager.Calc();
            return RedirectToAction("index");
        }
    }
}
