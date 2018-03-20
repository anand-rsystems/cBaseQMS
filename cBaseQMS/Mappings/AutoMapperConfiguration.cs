using AutoMapper;
using cBaseQms.DAL;
using cBaseQMS.Areas.cBaseQMS.Models;
using cBaseQMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<TestMaster, TestMasterViewModel>();
            CreateMap<TestMasterViewModel, TestMaster>();
         
            CreateMap<TestMasterViewModel, TestMaster>()
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           if (src.TestMasterID < 1)
                                                           {
                                                               dest.CreatedOn = DateTime.Now;
                                                               dest.CreatedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if(src.operation== "Update")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.operation == "Delete")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = false;

                                                           }
                                                       });
            
            CreateMap<TestMasterMappingViewModel, TestMasterMapping>()
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           if (src.TestMasterMapID < 1)
                                                           {
                                                               dest.CreatedOn = DateTime.Now;
                                                               dest.CreatedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.operation == "Remove")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = false;
                                                           }

                                                           
                                                       });
            CreateMap<TestMasterMappingViewModel, usp_GetLocationAndPartMapping>(MemberList.Source);



        }
    }
}