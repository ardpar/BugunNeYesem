using BugunNeYesem.Business.Abstract;
using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using BugunNeYesem.Infrastructure;
using BugunNeYesem.Infrastructure.Abstract;
using BugunNeYesem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Concrete
{
    public class MainManager : IMainManager
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IEatCardHistoryRepository _eatCardHistoryRepository;
        private readonly ILocationManager _locationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSessionService _userSessionService;

        public MainManager(
         ILocationRepository locationRepository,
         IHistoryRepository historyRepository,
         IEatCardHistoryRepository eatCardHistoryRepository,
         ILocationManager locationManager,
         IUserSessionService userSessionService,
        IHttpContextAccessor httpContextAccessor)
        {
            _locationRepository = locationRepository;
            _historyRepository = historyRepository;
            _userSessionService = userSessionService;
            _eatCardHistoryRepository = eatCardHistoryRepository;
            _locationManager = locationManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public CalcData GetData()
        {
            var userId = _userSessionService.GetUserId();
            //Havızada Data varmı?

            var datetime = DateTime.Now.Date;

            var q = _historyRepository.TableAsNoTracking.Include(x=>x.Location).FirstOrDefault(x => x.Date == datetime && x.Location.UserId == userId);
            //Yoksa Hesapla
            if (q != null)
            {
                bool yk = _eatCardHistoryRepository.TableAsNoTracking.Any(x => x.EndDate.Date >= datetime && x.UserId==userId);

                var qq = new CalcData()
                {
                    Date = datetime,
                    Name = q.Location.Name,
                    YK = q.Location.HasEatKard,
                    YKE = yk
                };
                return qq;
            }
            else
            {
                return Calc();
            }
        }

        //Calc
        public CalcData Calc()
        {
            var userId = _userSessionService.GetUserId();
            CalcData data = new CalcData();
            LocationListItemViewModel finding = null;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //Tüm Listeyi Getir.
            bool ykbittimi = _eatCardHistoryRepository.TableAsNoTracking.Any(x => x.EndDate.Date >= dt);
            var list = _locationManager.GetList();
            var q = list.ToList();
            if (!ykbittimi)
                q = q.Where(x => x.HasEatKard).ToList();
            //yk bittimi?

            //Bugün ün Kayıtlarını Getireceğiz.
            var toDay = GetDayToGoModels(DateTime.Now.DayOfWeek, q);
            // Kesin Yer varmı?
            var toDayCertain = GetCertainDayToModels(DateTime.Now.DayOfWeek, q);
            if (!ykbittimi && toDayCertain.Any(x => x.HasEatKard))
            {
                    toDayCertain= toDayCertain.Where(x => x.HasEatKard).ToList();
            }
            if (toDayCertain.CheckAny()) //Mecburi varsa 
            {
                finding = toDayCertain.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
            else
            {
                finding = toDay
                    .OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
            if (finding == null)
                finding = list.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            if (finding == null)
                return null;
            //Historyye kaydet
            var date = DateTime.Now.Date;
            var h = _historyRepository.Table.FirstOrDefault(x => x.Location.UserId == userId && x.Date == date);
            if (h == null)
                h = new History();
            h.Date = date;
            h.LocationId = finding.Id;


            data.Name = finding.Name;
            data.YK = finding.HasEatKard;
            data.Date = date;
            data.YKE = ykbittimi;
            if (h.Id > 0)
                _historyRepository.Update(h);
            else
                _historyRepository.Add(h);
            return data;

        }
        //Set Eatcard End

        public void SetEatCartStatus()
        {
            var userId = _userSessionService.GetUserId();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var yk = _eatCardHistoryRepository.Table.FirstOrDefault(x => x.EndDate.Date >= dt);
            if (yk == null)
            {
                yk = new EatCardHistory();
                yk.EndDate = DateTime.Now.Date;
                yk.UserId = userId;
                _eatCardHistoryRepository.Add(yk);
            }
            else
            {
                _eatCardHistoryRepository.Delete(yk);
            }

        }



        private List<LocationListItemViewModel> GetDayToGoModels(DayOfWeek dayOfWeek, IList<LocationListItemViewModel> data)
        {
            var q = data.AsEnumerable();

            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Monday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Tuesday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Wednesday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Thursday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Friday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                case DayOfWeek.Saturday:
                    q = q.Where(x => x.DayToGoSunday);
                    break;
                default: return null;
            }
            return q.ToList();
        }
        private List<LocationListItemViewModel> GetCertainDayToModels(DayOfWeek dayOfWeek, IList<LocationListItemViewModel> data)
        {
            var q = data.AsEnumerable();

            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Monday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Tuesday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Wednesday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Thursday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Friday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                case DayOfWeek.Saturday:
                    q = q.Where(x => x.CertainDayToGoSunday);
                    break;
                default:
                    return null;
            }
            return q.ToList();
        }
    }


}
