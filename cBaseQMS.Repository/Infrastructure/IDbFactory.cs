using cBaseQms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
       CBaseDevEntities Init();
    }
}
