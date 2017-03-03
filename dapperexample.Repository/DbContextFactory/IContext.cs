using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperexample.Repository.DbContextFactory
{
    interface IContext:IDisposable
    {
        /// <summary>
        /// 表明如果事务开始
        /// </summary>
        bool IsTransactionStarted { get; }
        /// <summary>
        /// 事务开始
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();

    }
}
