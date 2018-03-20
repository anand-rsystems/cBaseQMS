using AutoMapper;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Service.Services;
using cBaseQMS.Areas.cBaseQMS.Common;
using cBaseQMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class TestsController : Controller
    {
        
        private readonly IUnitOfWork iUnitOfWork;
        private readonly ITestService testService;
        private readonly ITestMasterService testMasterService;

        public TestsController(IUnitOfWork iunit, ITestService testService, ITestMasterService testMasterService)
        {
            this.testService = testService;
            this.iUnitOfWork = iunit;
            this.testMasterService = testMasterService;

        }
        
        // GET: cBaseQMS/Tests
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllTestMasters(string name = null)
        {
            IEnumerable<SelectListItem> testMasterList = new List<SelectListItem>();
            GetAllTests getAllTests = new GetAllTests(testService, iUnitOfWork, testMasterService);
            testMasterList = getAllTests.GetAllTestMasters(name);
            return Json(testMasterList, JsonRequestBehavior.AllowGet);

        }
        

        [HttpPost]
        public ActionResult CreateTest(TestsViewModel testModel)
        {
                testModel.CreatedBy = 111;
                testModel.CreatedOn = DateTime.Now;
                testModel.ModifiedBy = 2222;
                testModel.ModifiedOn = DateTime.Now;
                testModel.IsActive = true;
                Test objTest = null;
                objTest = Mapper.Map<TestsViewModel, Test>(testModel);
                testService.CreateTest(objTest);
                iUnitOfWork.Commit();
           
            return RedirectToAction("Index", "TestMaster");
        }

    }
}