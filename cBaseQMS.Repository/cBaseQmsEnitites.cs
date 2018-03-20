using cBaseQms.DAL;
using cBaseQms.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository
{
  public partial  class CBaseDevEntities : cBaseQms.DAL.cBaseDevEntities
    {
        public virtual void Commit()
        {
            base.SaveChanges();
        }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Configurations.Add(new TestMasterConfiguration());
           
        }
    }
}
