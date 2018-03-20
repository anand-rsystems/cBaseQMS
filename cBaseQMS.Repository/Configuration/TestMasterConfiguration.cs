using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using System.Data.Entity.ModelConfiguration;

namespace cBaseQms.Repository.Configuration
{
    class TestMasterConfiguration : EntityTypeConfiguration<TestMaster>
    {
        public TestMasterConfiguration()
        {
            ToTable("TestMaster");
            Property(g => g.TestMasterName).IsRequired();
            Property(g => g.Description).HasMaxLength(50);
            Property(g => g.IsActive).IsRequired();
            Property(g => g.CreatedOn).IsRequired();
            Property(g => g.CreatedBy).IsOptional();
            Property(g => g.ModifiedOn).IsOptional();
          
        }
    }
}
