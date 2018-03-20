using Autofac;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.ResolveDependency
{
    public class RepositryModule :Module
    {
    protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<TestMasterRepository>().As<ITestMasterRepository>().InstancePerRequest();
            builder.RegisterType<TestRepositry>().As<ITestRepository>().InstancePerRequest();
            builder.RegisterType<TestExpressionRepository>().As<ITestExpressionRepository>().InstancePerRequest();
            builder.RegisterType<PartMasterRepositry>().As<IPartMasterRepositry>().InstancePerRequest();
            builder.RegisterType<LocationMasterRepositry>().As<ILocationMasterRepositry>().InstancePerRequest();
            builder.RegisterType<TestMasterMappingRepositry>().As<ITestMasterMappingRepositry>().InstancePerRequest();
            builder.RegisterType<ComponentRepositry>().As<IComponentRepositry>().InstancePerRequest();
            builder.RegisterType<AppParameterRepository>().As<IAppParameterRepository>().InstancePerRequest();
            builder.RegisterType<EquationRepositry>().As<IEquationRepositry>().InstancePerRequest();
            builder.RegisterType<TestInputFieldsRepositry>().As<ITestInputFieldsRepositry>().InstancePerRequest();
            builder.RegisterType<VWLocationAttributesRepository>().As<IVWLocationAttributesRepository>().InstancePerRequest();
            builder.RegisterType<PartAttributesRepository>().As<IPartAttributesRepository>().InstancePerRequest();
            builder.RegisterType<TestCalculatedFieldsRepository>().As<ITestCalculatedFieldsRepository>().InstancePerRequest();
        }
    }
}
