using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Infrastructure.Abstract
{
    public interface IUserSessionService
    {
        int GetUserId();
        string GetUserName();
        bool IsAdmin();
    }
}
