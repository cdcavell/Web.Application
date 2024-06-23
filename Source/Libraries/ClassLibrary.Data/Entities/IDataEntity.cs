using ClassLibrary.Data.Contexts;

namespace ClassLibrary.Data.Entities
{
    public interface IDataEntity<T>
    {
        Guid Guid { get; set; }
        Guid Version { get; set; }

        bool IsNew { get; }

        void AddUpdate(ApplicationDbContext dbContext);
        void Delete(ApplicationDbContext dbContext);
        bool Equals(T obj);
    }
}
