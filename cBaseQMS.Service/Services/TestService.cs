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
    public interface ITestService
    {
        List<Test> GetAllTests(string name = null);
        Test GetTest(int id);
        Test GetTest(string name);
        void CreateTest(Test test);
        void SaveTest();
    }
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;        
        private readonly IUnitOfWork unitOfWork;
     
        public TestService(ITestRepository testRepository, IUnitOfWork unitOfWork)
        {   
            this.testRepository = testRepository;
            this.unitOfWork = unitOfWork;

        }

        public void CreateTest(Test test)
        {
            testRepository.Add(test);
        }

        public List<Test> GetAllTests(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return testRepository.GetAll().ToList();
            else
                return testRepository.GetAll().Where(c => c.TestName== name).ToList();
        }

        public Test GetTest(int id)
        {
            throw new NotImplementedException();
        }

        public Test GetTest(string name)
        {
            var test = testRepository.GetTests(name);
            return test;
        }

        public void SaveTest()
        {
            unitOfWork.Commit();
        }
    }
}
