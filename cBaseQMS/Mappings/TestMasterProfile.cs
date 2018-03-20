using AutoMapper;
using cBaseQms.DAL;
using cBaseQMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Mappings
{
    public class TestMasterProfile :Profile
    {
        public TestMasterProfile()
        {
            //           Mapper.CreateMap<CarViewModel, Car>()
            //.ForMember(dest => dest.Code, opt => opt.Condition(source => source.Id == 0))
            //CreateMap<TestMaster, TestMasterViewModel>().ForSourceMember(src=> src.TestMasterID,opt=>opt.Ignore())
            //                                            .AfterMap(src=>src.)
            CreateMap<TestMaster, TestMasterViewModel>();
            CreateMap<TestMasterViewModel,TestMaster>(MemberList.Source)
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           src.CreatedOn = DateTime.Now;
                                                           src.CreatedBy = 1111;
                                                           src.IsActive = true;
                                                       });
                                                       
               ;
            
        }
    }
}