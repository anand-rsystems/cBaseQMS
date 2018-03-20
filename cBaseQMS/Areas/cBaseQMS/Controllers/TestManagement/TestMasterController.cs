using cBaseQms.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cBaseQMS.Models;
using AutoMapper;
using cBaseQMS.Mappings;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using Elmah;
using cBaseQMS.Areas.cBaseQMS.Common;
using cBaseQMS.Areas.cBaseQMS.Models;
using FluentValidation.Results;
using cBaseQMS.Areas.cBaseQMS.Helpers;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class TestMasterController : Controller
    {
        // GET: cBaseQMS/TestMaster
        private readonly ITestMasterService testMasterService;
      
        private readonly IUnitOfWork iUnitOfWork;
        private readonly ITestService testService;
        private readonly IPartMasterService partMasterService;
        private readonly ILocationMasterService locationMasterService;
        private readonly ITestMasterMappingService testMasterMappingService;
      

        public TestMasterController(ITestMasterService testMasterService, IUnitOfWork iunit, ITestService testService,
                IPartMasterService partMasterService, ILocationMasterService locationMasterService,
                ITestMasterMappingService testMasterMappingService)
        {
         
            this.locationMasterService = locationMasterService;
            this.partMasterService = partMasterService;
            this.testMasterService = testMasterService;
            this.iUnitOfWork = iunit;
            this.testService = testService;
            this.testMasterMappingService = testMasterMappingService;
         

        }
        // GET: TestMaster
        [HandleBusinessException(ForAjaxRequest =false)]       
        public ActionResult Index(string name = null)
        {
            IEnumerable<TestMasterViewModel> testMasterViewModel;
            IEnumerable<TestMaster> testMaster;

            testMaster = testMasterService.GetMasterTests(name).ToList();
            testMasterViewModel = Mapper.Map<IEnumerable<TestMaster>, IEnumerable<TestMasterViewModel>>(testMaster);
           // throw new Exception();
            return View(testMasterViewModel);
            //return View();
        }


        [HttpPost]
        [HandleBusinessException]
        public ActionResult CreateTestMaster(TestMasterViewModel testMasterModel)
        {
            TestMaster objTestMaster = null;
            objTestMaster = Mapper.Map<TestMasterViewModel, TestMaster>(testMasterModel);
            testMasterService.CreateTestMaster(objTestMaster);
            iUnitOfWork.Commit();

            var Data = new
            {
                Success = true,
                SuccessCode = 200,
                SuccessMessage = "Test Master Created Successfuly"
            };
           return Json(Data, JsonRequestBehavior.AllowGet);
           
        }

        [HttpGet]
        public JsonResult GetAllTest(string name = null)
        {
            IEnumerable<SelectListItem> testsList = new List<SelectListItem>();
            GetAllTests getAllTests = new GetAllTests(testService, iUnitOfWork, testMasterService);
            testsList= getAllTests.GetAllTest(name);
            return Json(testsList, JsonRequestBehavior.AllowGet);
         
        }
        /// <summary>
        ///    Get all testmaster for particular test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>  
        public JsonResult GetAllTestMastersByTest(string testid)
        {            
            List<TestMaster> testMasterList = new List<TestMaster>();
            List<TestMasterViewModel> objTestMasterViewModel = null;
            object[] param= { string.IsNullOrEmpty(testid)? "0":testid};
            testMasterList = testMasterService.GetMasterTests(param).ToList();
            objTestMasterViewModel = Mapper.Map<List<TestMaster>, List<TestMasterViewModel>>(testMasterList);
             var jsonData = new { rows= objTestMasterViewModel };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///    Get all testmaster for particular test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>  
        [HttpPost]
        [HandleBusinessException]
        public JsonResult UpdateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            TestMaster testMaster = null;
            testMaster = Mapper.Map<TestMasterViewModel,TestMaster > (testMasterViewModel);
            testMasterService.UpdateTestMaster(testMaster);
            iUnitOfWork.Commit();
             var Data = new
             {
                 Success = true,
                 testMasterId= testMasterViewModel.TestMasterID,
                 testMasterName= testMasterViewModel.TestMasterName,
                 testMasterDescription = testMasterViewModel.Description,
                 msg = (testMasterViewModel.operation== "Delete")?"Test Master Removed":  "Test Master Updated!"
             };
            return Json(Data, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// TO GET ALL PART MASTERS
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllPartMaster()
        {
            IEnumerable<SelectListItem> testsList = new List<SelectListItem>();
            GetAllTests getAllTests = new GetAllTests( partMasterService, iUnitOfWork);
            testsList = getAllTests.GetAllPartMasters();
            return Json(testsList, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// TO GET ALL PART MASTERS
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllLocationMaster()
        {
            IEnumerable<SelectListItem> locationList = new List<SelectListItem>();
            GetAllTests getAllTests = new GetAllTests(locationMasterService, iUnitOfWork);
            locationList = getAllTests.GetAllLocationMasters();
            return Json(locationList, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>  
        public JsonResult GetAllLocationAndPartMasterMapping(int testMasterid=0)
        {
            List<usp_GetLocationAndPartMapping> testMasterMappingLocationAndParameterList = new List<usp_GetLocationAndPartMapping>();
            List<GetLocationAndPartMappingViewModel> masterMappingLocationAndParameterList = new List<GetLocationAndPartMappingViewModel>();
           
            testMasterMappingLocationAndParameterList = testMasterMappingService.GetAllLocationAndPartMasterMapping(testMasterid).ToList();
            
            masterMappingLocationAndParameterList = Mapper.Map<List<GetLocationAndPartMappingViewModel>>(testMasterMappingLocationAndParameterList);
            var jsonData = new { rows = masterMappingLocationAndParameterList };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>  
        [HttpPost]
        [HandleBusinessException]
        public JsonResult CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            //moved to mapper configuration
            //testMasterMappingViewModel.CreatedBy = 111;
            //testMasterMappingViewModel.CreatedOn = DateTime.Now;
            //testMasterMappingViewModel.ModifiedBy = 2222;
            //testMasterMappingViewModel.ModifiedOn = DateTime.Now;
            //testMasterMappingViewModel.IsActive = true;

            TestMasterMapping objestMasterMappingr = null;

            objestMasterMappingr = Mapper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            testMasterMappingService.CreateTestMasterMapping(objestMasterMappingr);
            iUnitOfWork.Commit();
            var Data = new
            {
                Success = true,
                testMasterId = testMasterMappingViewModel.TestMasterID,
                msg = "location and part has been created!"
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
            
          
        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>  
        [HttpPost]
        public JsonResult RemovePartAndLocation(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            TestMasterMapping objTestMasterMapping = null;

            objTestMasterMapping = Mapper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            testMasterMappingService.RemovePartAndLocationMapping(objTestMasterMapping);
            iUnitOfWork.Commit();
            var data = new
            {
                status = true,
                testMasterId= testMasterMappingViewModel.TestMasterID,
                msg ="Location and Part mapping removed !"
            };
            return Json(data,JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>  
        [HttpPost]
        [HandleBusinessException]
        public JsonResult checkExistence(TestMasterMappingViewModel testMasterMappingViewModel)
        {

            TestMasterMapping objestMasterMapping = null;

            objestMasterMapping = Mapper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            testMasterMappingService.IfExistsPartAndLocationCombination(objestMasterMapping);
            var Data = new
            {
                Success = true
                
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
            
            

        }


        public ActionResult Error()
        {
            return View("Error");
        }
    }
}