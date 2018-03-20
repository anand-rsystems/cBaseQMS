using Autofac;
using cBaseQms.DAL;
using cBaseQms.Service.ModelValidation;
using cBaseQms.Service.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.ResolveDependency
{
   public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestMasterService>().As<ITestMasterService>().InstancePerRequest();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerRequest();
            builder.RegisterType<TestMasterService>().As<ITestMasterService>().InstancePerRequest();
            builder.RegisterType<TestExpressionService>().As<ITestExpressionService>().InstancePerRequest();
            builder.RegisterType<PartMasterService>().As<IPartMasterService>().InstancePerRequest();
            builder.RegisterType<LocationMasterService>().As<ILocationMasterService>().InstancePerRequest();
            builder.RegisterType<TestMasterMappingService>().As<ITestMasterMappingService>().InstancePerRequest();
            builder.RegisterType<AppParameterService>().As<IAppParameterService>().InstancePerRequest();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().InstancePerRequest();
            builder.RegisterType<TestMasterValidator>().As<IValidator<TestMaster>>().InstancePerRequest();
            builder.RegisterType<TestMasterMappingValidator>().As<IValidator<TestMasterMapping>>().InstancePerRequest();
        }
    }
}
