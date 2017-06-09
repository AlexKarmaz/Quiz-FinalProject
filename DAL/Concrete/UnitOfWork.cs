using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed = false;
        public DbContext Context { get; private set; }
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                    }
                }
            }
            isDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(true);
        }
    }
}
