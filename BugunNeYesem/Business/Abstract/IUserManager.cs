using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using BugunNeYesem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Abstract
{
    public interface IUserManager
    {
        bool DeleteUser(int id);
        List<UserListItemViewModel> GetList();

        byte SaveUser(UserEditViewModel model);
        byte SaveProfile(UserEditProfileViewModel model);
        UserEditProfileViewModel GetProfile();
        byte LostPassword(LostPasswordViewModel model);
        byte Register(RegisterViewModel model);
        void SignOut();
        bool Login(LoginViewModel model);
        UserEditViewModel GetUser(int id);
    }
}
