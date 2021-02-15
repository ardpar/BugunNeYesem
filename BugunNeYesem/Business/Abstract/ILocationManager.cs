using BugunNeYesem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Abstract
{
    public interface ILocationManager
    {
        List<LocationListItemViewModel> GetList();
        LocationEditViewModel GetEdit(int id);
        byte SetEdit(LocationEditViewModel model);
        byte SetDelete(int id);

    }
}
