using ClassLibrary.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ClassLibrary.Data.Entities
{
    public abstract class DataEntity<T> : IDataEntity<DataEntity<T>> where T : DataEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; } = Guid.Empty;
        [ConcurrencyCheck]
        public Guid Version { get; set; } = Guid.NewGuid();

        [NotMapped]
        public bool IsNew
        {
            get
            {
                if (this.Guid == Guid.Empty)
                    return true;

                return false;
            }
        }

        public virtual void AddUpdate(ApplicationDbContext dbContext)
        {
            var dbContextTransaction = dbContext.Database.CurrentTransaction;
            if (dbContextTransaction == null)
            {
                dbContextTransaction = dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);
                using (dbContextTransaction)
                {
                    try
                    {
                        InternalAddUpdate(dbContext);
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                InternalAddUpdate(dbContext);
            }
        }

        private void InternalAddUpdate(ApplicationDbContext dbContext)
        {
            if (this.IsNew)
            {
                this.Guid = Guid.NewGuid();
                dbContext.Add<DataEntity<T>>(this);
            }
            else
            {
                dbContext.Update<DataEntity<T>>(this);
            }

            if (dbContext.HasUnsavedChanges())
            {
                bool saved = false;
                while (!saved)
                {
                    try
                    {
                        this.Version = Guid.NewGuid();
                        dbContext.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException exception)
                    {
                        foreach (var entry in exception.Entries)
                        {
                            var databaseValues = entry.GetDatabaseValues();

                            // Refresh original values to bypass next concurrency check
                            if (databaseValues != null)
                                entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }
            }
        }

        public virtual void Delete(ApplicationDbContext dbContext)
        {
            if (!this.IsNew)
            {
                var dbContextTransaction = dbContext.Database.CurrentTransaction;
                if (dbContextTransaction == null)
                {
                    dbContextTransaction = dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);
                    using (dbContextTransaction)
                    {
                        try
                        {
                            InternalDelete(dbContext);
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    InternalDelete(dbContext);
                }
            }
        }

        private void InternalDelete(ApplicationDbContext dbContext)
        {
            dbContext.Attach<DataEntity<T>>(this);
            dbContext.Remove<DataEntity<T>>(this);
            dbContext.SaveChanges();
        }

        public virtual bool Equals(DataEntity<T> obj)
        {
            return (this == obj);
        }
    }
}
