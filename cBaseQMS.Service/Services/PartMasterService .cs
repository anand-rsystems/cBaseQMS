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
    public interface IPartMasterService
    {
        List<PartMaster> GetAllParts();
       
    }
    public class PartMasterService : IPartMasterService
    {
        private readonly IPartMasterRepositry partMasterRepositry;        
        private readonly IUnitOfWork unitOfWork;

        
        public PartMasterService(IPartMasterRepositry partMasterRepositry, IUnitOfWork unitOfWork)
        {   
            this.partMasterRepositry = partMasterRepositry;
            this.unitOfWork = unitOfWork;

        }
        public List<PartMaster> GetAllParts()
        {
            return partMasterRepositry.GetAll().ToList();
        }


    }
}
