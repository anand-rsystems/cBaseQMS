using AutoMapper;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Service.Services;
using cBaseQMS.Areas.cBaseQMS.Models;
using cBaseQMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.Common
{
    public class GetAllTests
    {



        private readonly ITestService testService;
        private readonly IUnitOfWork iUnitOfWork;
        private readonly ITestMasterService testMasterService;
        private readonly IPartMasterService partMasterService;
        private readonly ILocationMasterService locationMasterService;

        public GetAllTests(ITestService testsService, IUnitOfWork iunit, ITestMasterService testMasterService)
        {
            this.testService = testsService;
            this.testMasterService = testMasterService;
            this.iUnitOfWork = iunit;

        }
        /// <summary>
        /// to get all parts
        /// </summary>
        /// <param name="partMasterService"></param>
        /// <param name="iunit"></param>
        public GetAllTests(IPartMasterService partMasterService, IUnitOfWork iunit)
        {
            this.partMasterService = partMasterService;
            this.iUnitOfWork = iunit;

        }

        /// <summary>
        /// to get all location
        /// </summary>
        /// <param name="partMasterService"></param>
        /// <param name="iunit"></param>
        public GetAllTests(ILocationMasterService locationMasterService, IUnitOfWork iunit)
        {
            this.locationMasterService = locationMasterService;
            this.iUnitOfWork = iunit;

        }


        /// <summary>
        /// TO GET ALL TEST
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAllTest(string name = null)
        {
            List<Test> testsList = new List<Test>();

            testsList = testService.GetAllTests(name).Where(x => x.IsActive == true).ToList();

            IEnumerable<TestsViewModel> _testsViewModel;
            _testsViewModel = Mapper.Map<IEnumerable<Test>, IEnumerable<TestsViewModel>>(testsList);

            IEnumerable<SelectListItem> _testlist = _testsViewModel.AsEnumerable()
                                                    .Select(testItem => new SelectListItem() { Text = testItem.TestName, Value = testItem.TestID.ToString() });
            return _testlist;
        }

        /// <summary>
        /// TO GET ALL TEST MASTERS
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAllTestMasters(string name = null)
        {
            List<TestMaster> testsMasterList = new List<TestMaster>();

            testsMasterList = testMasterService.GetMasterTests(name).Where(x => x.IsActive == true).ToList();

            IEnumerable<TestMasterViewModel> _testsViewModel;
            _testsViewModel = Mapper.Map<IEnumerable<TestMaster>, IEnumerable<TestMasterViewModel>>(testsMasterList);

            IEnumerable<SelectListItem> _testlist = _testsViewModel.AsEnumerable()
                                                    .Select(testItem => new SelectListItem() { Text = testItem.TestMasterName, Value = testItem.TestMasterID.ToString() });
            return _testlist;
        }


        public IEnumerable<SelectListItem> GetAllPartMasters()
        {
            List<PartMaster> partMasterList = new List<PartMaster>();

            partMasterList = partMasterService.GetAllParts().Where(x => x.IsActive == true).ToList();

            IEnumerable<PartMasterViewModel> partMasterViewModel;
            partMasterViewModel = Mapper.Map<IEnumerable<PartMaster>, IEnumerable<PartMasterViewModel>>(partMasterList);

            IEnumerable<SelectListItem> partMasterlist = partMasterViewModel.AsEnumerable()
                                                    .Select(partItem => new SelectListItem() { Text = partItem.PartNumber, Value = partItem.PartMasterID.ToString() });
            return partMasterlist;
        }


        public IEnumerable<SelectListItem> GetAllLocationMasters()
        {
            List<LocationMaster> locationMasterList = new List<LocationMaster>();

            locationMasterList = locationMasterService.GetAllLocations().Where(x => x.IsActive == true).ToList();

            IEnumerable<LocationMasterViewModel> locationMasterViewModel;
            locationMasterViewModel = Mapper.Map<IEnumerable<LocationMaster>, IEnumerable<LocationMasterViewModel>>(locationMasterList);

            IEnumerable<SelectListItem> locationMasterlist = locationMasterViewModel.AsEnumerable()
                                                    .Select(locaitonItem => new SelectListItem() { Text = locaitonItem.LocationName, Value = locaitonItem.LocationMasterID.ToString() });
            return locationMasterlist;
        }

    }


}