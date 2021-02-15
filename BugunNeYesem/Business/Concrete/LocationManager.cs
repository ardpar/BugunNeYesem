using BugunNeYesem.Business.Abstract;
using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using BugunNeYesem.Infrastructure.Abstract;
using BugunNeYesem.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Concrete
{
    public class LocationManager : ILocationManager
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSessionService _userSessionService;
        public LocationManager(ILocationRepository locationRepository, IHttpContextAccessor httpContextAccessor, IUserSessionService userSessionService)
        {
            _locationRepository = locationRepository;
            _httpContextAccessor = httpContextAccessor;
            _userSessionService = userSessionService;
        }

        //List
        public List<LocationListItemViewModel> GetList()
        {
            var userId = _userSessionService.GetUserId();
            return _locationRepository.TableAsNoTracking.Where(x => x.UserId == userId).ToList()
                .Select(x =>
            {
                var item = new LocationListItemViewModel() { Id = x.Id, Name = x.Name, HasEatKard=x.HasEatKard };
                item.SetDayToGo(x.DayToGo);
                item.SetCertainDayToGo(x.CertainDayToGo);
                return item;
            }).ToList();
        }

        //Edit
        public LocationEditViewModel GetEdit(int id)
        {
            var userId = _userSessionService.GetUserId();
            var q = _locationRepository.TableAsNoTracking.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (q == null)
            {
                var item = new LocationEditViewModel()  ;
                return item;
            }
            else
            {
                var item = new LocationEditViewModel() { Id = q.Id, Name = q.Name, HasEatKard = q.HasEatKard };
                item.SetDayToGo(q.DayToGo);
                item.SetCertainDayToGo(q.CertainDayToGo);
                return item;
            }
        }

        //EklemeDüzenleme
        public byte SetEdit(LocationEditViewModel model)
        {
            var userId = _userSessionService.GetUserId();
            var q = _locationRepository.TableAsNoTracking.FirstOrDefault(x => x.Id == model.Id && x.UserId==userId);
            if (q == null)
                q = new Location();
            q.UserId = userId;
            q.Name = model.Name;
            q.HasEatKard = model.HasEatKard;
            q.DayToGo = model.GetDayToGo();
            q.CertainDayToGo = model.GeCertainDayToGo();

            if (q.Id > 0)
            {
                if (_locationRepository.Update(q))
                    return 0;
            }
            else
            {
                if (_locationRepository.Add(q))
                    return 0;
            }
            return 1;
         }
        //Silme
        public byte SetDelete(int id)
        {
            var userId = _userSessionService.GetUserId();
            var q = _locationRepository.TableAsNoTracking.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (q == null)
                return 1;
            if (_locationRepository.Delete(q))
                return 0;
            return 2;
        }
    }
}
