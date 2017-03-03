using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperexample.Repository
{
    public class CustomClassMapper<T>: AutoClassMapper<T> where T:class
    {
        public override void Table(string tableName)
        {
            base.Table(tableName);
        }
    }
}
