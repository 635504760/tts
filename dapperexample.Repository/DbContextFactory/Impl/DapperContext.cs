using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Configuration;
using DapperExtensions;
using System.Diagnostics;
using System.Threading;
namespace dapperexample.Repository.DbContextFactory.Impl
{
    public abstract class DapperContext:IContext
    {
        private bool _isTransactionStarted;

        private IDbConnection _connection;

        private IDbTransaction _transaction;

        private int? _commandTimeout = null;

        public bool IsTransactionStarted
        {
            get { return _isTransactionStarted; }
        }
        public IDbConnection Connection
        {
            get { return _connection; }
        }


        private IDbConnection CreateConnection()
        {
            var config = ConfigurationManager.ConnectionStrings[_connStringName];
            var factory = DbProviderFactories.GetFactory(config.ProviderName);
            var cnn = factory.CreateConnection();
            cnn.ConnectionString = config.ConnectionString;
            return cnn;
        }

        private string _connStringName;

        protected DapperContext(string connStringName)
        {
            _connStringName = connStringName;
            _connection = CreateConnection();
            DapperExtensions.DapperExtensions.DefaultMapper=typeof(CustomClassMapper<>);

            if(_connection.State!=ConnectionState.Open)
            {
                _connection.Open();
            }
            DebugPrint("Connection started.");

        }
        private void DebugPrint(string message)
        {
            Debug.Print(">>> UnitOfWorkWithDapper - Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, message);
        }
        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }

        public int? GetCommandTimeout()
        {
            return _commandTimeout;
        }

        public void BeginTransaction()
        {
            if (_isTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");
            _transaction = _connection.BeginTransaction();
            _isTransactionStarted = true;
            DebugPrint("Transaction started.");
        }

        public void Commit()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");
            _transaction.Commit();
            _transaction = null;
            _isTransactionStarted = false;
            DebugPrint("Transaction committed.");
        }

        public void Rollback()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
            _isTransactionStarted = false;
            DebugPrint("Transaction rollbacked and disposed.");
        }
    }
}
