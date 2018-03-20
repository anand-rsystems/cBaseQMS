using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ITestMasterService
    {
        List<TestMaster> GetMasterTests(string name = null);
        TestMaster GetTestMaster(int id);
        TestMaster GetTestMaster(string name);
        bool CreateTestMaster(TestMaster testMaster, ref ValidationResult result);
        void SaveTestMaster();
        IEnumerable<TestMaster> GetMasterTests(object[] param);
        void CreateTestMaster(TestMaster testMaster);
        void UpdateTestMaster(TestMaster testMaster);


    }
    public class TestMasterService : ITestMasterService
    {
        private readonly ITestMasterRepository testMasterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidatorFactory validatorFactory;

        public TestMasterService(ITestMasterRepository testMasterRepository, IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
            this.testMasterRepository = testMasterRepository;
            this.unitOfWork = unitOfWork;

        }


     
        void ITestMasterService.CreateTestMaster(TestMaster testMaster)
        {
            var validator = validatorFactory.GetValidator<TestMaster>();
           validator.ValidateAndThrow(testMaster);
            testMasterRepository.Add(testMaster);

        }

        bool ITestMasterService.CreateTestMaster(TestMaster testMaster,  ref ValidationResult result)
        {
            var validator = validatorFactory.GetValidator<TestMaster>();
             result = validator.Validate(testMaster);

            if(result.IsValid)
            {
                testMasterRepository.Add(testMaster);
             }
                
            return result.IsValid;
            
        }

        TestMaster ITestMasterService.GetTestMaster(int id)
        {
            var category = testMasterRepository.GetById(id);
            return category;
        }

        TestMaster ITestMasterService.GetTestMaster(string name)
        {
            var category = testMasterRepository.GetTestMasterByName(name);
            return category;
        }

        List<TestMaster> ITestMasterService.GetMasterTests(string name)
        {
            if (string.IsNullOrEmpty(name))
                return testMasterRepository.GetAll().Where(c=>c.IsActive==true).ToList();
            else
                return testMasterRepository.GetAll().Where(c => c.TestMasterName == name && c.IsActive==true).ToList();
        }


        void ITestMasterService.SaveTestMaster()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<TestMaster> GetMasterTests(object[] param)
        {
            string spName = "usp_GetTestMasterByTestId {0}";
            return testMasterRepository.ExecuteQuery(spName, param);
        }

        public void UpdateTestMaster(TestMaster testMaster)
        {
            var validator = validatorFactory.GetValidator<TestMaster>();
            validator.ValidateAndThrow(testMaster);
            testMasterRepository.Update(testMaster);
            
        }
    }
}
