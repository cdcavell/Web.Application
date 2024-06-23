using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void MapEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditHistory>(entity =>
            {
            });
        }
    }
}
