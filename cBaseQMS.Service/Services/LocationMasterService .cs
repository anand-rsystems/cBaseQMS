using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ILocationMasterService
    {
        List<LocationMaster> GetAllLocations();
       
    }
    public class LocationMasterService : ILocationMasterService
    {
        private readonly ILocationMasterRepositry locationMasterRepositry;        
        private readonly IUnitOfWork unitOfWork;

        
        public LocationMasterService(ILocationMasterRepositry locationMasterRepositry, IUnitOfWork unitOfWork)
        {   
            this.locationMasterRepositry = locationMasterRepositry;
            this.unitOfWork = unitOfWork;

        }
        public List<LocationMaster> GetAllLocations()
        {
            return locationMasterRepositry.GetAll().ToList();
        }


    }
}
