using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TwitterCloneMVC.Models
{
    public partial class TwitterDbContext:DbContext
    {
        public TwitterDbContext()
           : base("name=FSDEntities")

        {

            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        //public DbSet<UserAcccount> userAccount { get; set; }

        public virtual DbSet<Person> person { get; set; }

        public System.Data.Entity.DbSet<TwitterCloneMVC.Models.UserAcccount> UserAcccounts { get; set; }
    }
}