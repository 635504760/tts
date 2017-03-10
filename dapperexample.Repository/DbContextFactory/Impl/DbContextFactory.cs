using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperexample.Repository.DbContextFactory.Impl
{
    public class DbContextFactory:IDbContextFactory
    {
        private readonly string _ConnectionStringName;

        public DbContextFactory(string connectionStringName)
        {
            this._ConnectionStringName = connectionStringName;
        }

        private DapperContext _dbContext;

        private DapperContext DbContext
        {
            get 
            {
                if (this._dbContext == null)
                {
                    Type t = typeof(MyDbContext);
                    this._dbContext = (DapperContext)Activator.CreateInstance(t,this._ConnectionStringName);
                }
                return _dbContext;
            }
        }

        public DapperContext GetDbContext()
        {
            return this.DbContext;
        }
    }
}
