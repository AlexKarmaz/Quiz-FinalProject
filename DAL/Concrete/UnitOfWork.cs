using DAL.Interface.Interfaces;
using Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger logger;

        private bool isDisposed = false;
        public DbContext Context { get; private set; }
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
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
