using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;
using FluentValidation;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ITestMasterMappingService
    {
        List<usp_GetLocationAndPartMapping> GetAllLocationAndPartMasterMapping(int testmasterid);
        void CreateTestMasterMapping(TestMasterMapping testMasterMapping);
        void RemovePartAndLocationMapping(TestMasterMapping testMasterMapping);
        void UpdateTestMasterMapping(object[] param);
        void IfExistsPartAndLocationCombination(TestMasterMapping testMasterMapping);
    }
    public class TestMasterMappingService : ITestMasterMappingService
    {
        private readonly ITestMasterMappingRepositry testMasterMappingRepositry;        
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidatorFactory validatorFactory;


        public TestMasterMappingService(ITestMasterMappingRepositry testMasterMappingRepositry, IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
            this.testMasterMappingRepositry = testMasterMappingRepositry;
            this.unitOfWork = unitOfWork;

        }

        public void UpdateTestMasterMapping(object[] param)
        {
            string spName = "usp_UpdateLocationAndPartMapping {0}";
            testMasterMappingRepositry.ExecuteCommand(spName, param);
        }

        List<usp_GetLocationAndPartMapping> ITestMasterMappingService.GetAllLocationAndPartMasterMapping(int testmasterid)
        {
            return testMasterMappingRepositry.GetAllLocationAndPartMasterMapping(testmasterid);
        }

        public void SaveTest()
        {
            unitOfWork.Commit();
        }

        public void CreateTestMasterMapping(TestMasterMapping testMasterMapping)
        {
            var validator = validatorFactory.GetValidator<TestMasterMapping>();
            validator.ValidateAndThrow(testMasterMapping);
           testMasterMappingRepositry.Add(testMasterMapping);
        }

        public void RemovePartAndLocationMapping(TestMasterMapping testMasterMapping)
        {

            var validator = validatorFactory.GetValidator<TestMasterMapping>();
            validator.ValidateAndThrow(testMasterMapping);
            //Property based updation
            testMasterMappingRepositry.Update(testMasterMapping, o => o.IsActive,o=>o.ModifiedBy, o => o.ModifiedOn);
            
        }

        public void IfExistsPartAndLocationCombination(TestMasterMapping testMasterMapping)
        {
            var validator = validatorFactory.GetValidator<TestMasterMapping>();
            validator.ValidateAndThrow(testMasterMapping);
            
        }
    }
}
