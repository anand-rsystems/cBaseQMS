using cBaseQms.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using cBaseQms.DAL;

namespace cBaseQms.Service.ModelValidation
{
   public class TestMasterMappingValidator : AbstractValidator<TestMasterMapping>
    {
        public TestMasterMappingValidator(ITestMasterMappingRepositry testMasterMappingRepositry)
        {
            
            //RuleFor(x => x.PartMasterID).Must(validatePartAndMaster).WithMessage("Please select part!");
            //RuleFor(x => x.LocationMasterID).Must(validatePartAndMaster).WithMessage("Please select location!");
            RuleFor(x => x.LocationMasterID).Must((o, t) => { return
                testMasterMappingRepositry.IfExistsPartAndLocationCombination(o.LocationMasterID, o.PartMasterID,o.TestMasterID);})
                .WithMessage("combination of part and location exists");

            }
        
        bool validatePartAndMaster(int val)
        {
            if (val == 0)
                return false;
            else return true;
        }
    }
}
