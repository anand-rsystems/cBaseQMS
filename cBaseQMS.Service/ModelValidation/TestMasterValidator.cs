using cBaseQms.DAL;
using cBaseQms.Repository.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Service.ModelValidation
{
    public class TestMasterValidator : AbstractValidator<TestMaster>
    {
        public TestMasterValidator(ITestMasterRepository testMasterRepository)
        {
            
            RuleFor(p => p.TestMasterName).NotEmpty().When(p=>p.IsActive!=false).WithMessage("Test Master Name is required field");
            RuleFor(p => p.TestMasterName).Must(testMasterRepository.GetCountTestMasterByName).When(p => p.ModifiedBy != null && p.IsActive==true)
               .WithMessage("Provided Name already exists!! ");
            RuleFor(p => p.TestMasterName).Must(testMasterRepository.GetCountTestMasterByName).When(p => p.ModifiedBy == null && p.IsActive == true).WithMessage("Test Master Name already exists");


        }
    }
}
