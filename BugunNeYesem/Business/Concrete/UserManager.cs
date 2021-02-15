using BugunNeYesem.Business.Abstract;
using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using BugunNeYesem.Infrastructure.Abstract;
using BugunNeYesem.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSessionService _userSessionService;
        public UserManager(IUserRepository userRepository, ICryptoService cryptoService, IHttpContextAccessor httpContextAccessor, IUserSessionService userSessionService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _httpContextAccessor = httpContextAccessor;
            _userSessionService = userSessionService;
        }

        public bool Login(LoginViewModel model)
        {
            var cPasword = _cryptoService.MD5Hash(model.Password);
            var user = _userRepository.TableAsNoTracking.FirstOrDefault(x => x.Mail == model.Mail && x.Password == cPasword);
            if (user == null)
                return false;
            var claims = new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name??""),
                    new Claim(ClaimTypes.Surname, user.Surname??""),
                    new Claim(ClaimTypes.Role, user.IsAdmin?"AD":"SK"),
                    new Claim(ClaimTypes.Email, user.Mail??"")
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)).Wait();

            return user != null;
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync().Wait();
        }
        public byte Register(RegisterViewModel model)
        {
            var user = _userRepository.TableAsNoTracking.FirstOrDefault(x => x.Mail == model.Mail);
            if (user != null)
                return 1;//Mail Kullanımda
            var cPasword = _cryptoService.MD5Hash(model.Password);

            user = new User()
            {
                Name = model.Name,
                Mail = model.Mail,
                Password = cPasword,
                Surname = model.Surname
            };

            var save = _userRepository.Add(user);
            if (save)
                return 0;//Kayıt etti
            return 2;//Kayıt Hatası
        }

        public byte LostPassword(LostPasswordViewModel model)
        {
            var user = _userRepository.Table.FirstOrDefault(x => x.Mail == model.Mail);
            if (user == null)
                return 1;//Mail Bulunamadı

            var passwod = Guid.NewGuid().ToString().Substring(0, 5);
            var cPasword = _cryptoService.MD5Hash(passwod);

            user.Password = cPasword;

            //TODO :Mail Gönderme işlemleri

            var update = _userRepository.Update(user);
            if (update)
                return 0;//Kayıt etti
            return 2;//Kayıt Hatası
        }

        public UserEditProfileViewModel GetProfile()
        {
            var userId = _userSessionService.GetUserId();
            return _userRepository.TableAsNoTracking.Where(x => x.Id == userId)
                .Select(x => new UserEditProfileViewModel()
                {
                    Name = x.Name,
                    Password = "",
                    Surname = x.Surname
                })
                .FirstOrDefault();

        }
        public byte SaveProfile(UserEditProfileViewModel model)
        {
            var userId = _userSessionService.GetUserId();
            var user = _userRepository.Table.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return 1;
            user.Name = model.Name;
            user.Surname = model.Surname;
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = _cryptoService.MD5Hash(model.Password);
            }
            var up = _userRepository.Update(user);
            if (up)
                return 0;
            return 2;
        }


        public UserEditViewModel GetUser(int id)
        {
            if (id == 0)
                return new UserEditViewModel();
            var user = _userRepository.Table.FirstOrDefault(x => x.Id == id);
            var x = new UserEditViewModel()
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Mail = user.Mail,
                Name = user.Name,
                Surname = user.Surname
            };
            return x;
        }
        public byte SaveUser(UserEditViewModel model)
        {
            var userId = model.Id;
            var user = _userRepository.Table.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                user = new User();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Mail = model.Mail;
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = _cryptoService.MD5Hash(model.Password);
            }
            user.IsAdmin = model.IsAdmin;
            if (user.Id == 0)
            {
                var add = _userRepository.Add(user);
                if (add)
                    return 0;
            }
            else
            {
                var up = _userRepository.Update(user);
                if (up)
                    return 0;
            }
            return 2;
        }

        public List<UserListItemViewModel> GetList()
        {

            return _userRepository.TableAsNoTracking.Select(
                x => new UserListItemViewModel()
                {
                    Id = x.Id,
                    IsAdmin = x.IsAdmin,
                    Mail = x.Mail,
                    Name = x.Name,
                    Surname = x.Surname
                }).ToList();
        }
        public bool DeleteUser(int id)
        {

            var user = _userRepository.Table.FirstOrDefault(x => x.Id == id);
            return _userRepository.Delete(user);
        }
    }
}
