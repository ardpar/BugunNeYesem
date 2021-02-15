using BugunNeYesem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Business.Abstract
{
   public interface IMainManager
    {
        CalcData GetData();
        CalcData Calc();
        void SetEatCartStatus();
    }
}
