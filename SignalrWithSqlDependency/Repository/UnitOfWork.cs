using SignalrWithSqlDependency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrWithSqlDependency.Repository
{
    public class UnitOfWork:IDisposable
    {
        private SignalrWithSqlDependencyEntities context = new SignalrWithSqlDependencyEntities();
        private GenericRepository<DevTest> devTestRepository;        

        public GenericRepository<DevTest> DevTestRepository
        {
            get
            {

                if (this.devTestRepository == null)
                {
                    this.devTestRepository = new GenericRepository<DevTest>(context);
                }
                return devTestRepository;
            }
        }
        

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}