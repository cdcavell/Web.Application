using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ClassLibrary.Data.Models
{
    public class AuditEntry(EntityEntry entry)
    {
        public EntityEntry Entry { get; } = entry;
        [DataType(DataType.Text)]
        public string TableName { get; set; } = String.Empty;
        [DataType(DataType.Text)]
        public string State { get; set; } = String.Empty;
        [DataType(DataType.DateTime)]
        public DateTime ModifiedOn { get; set; }
        public Dictionary<string, object> KeyValues { get; } = [];
        public Dictionary<string, object> OriginalValues { get; } = [];
        public Dictionary<string, object> CurrentValues { get; } = [];
        public List<PropertyEntry> TemporaryProperties { get; } = [];

        public bool HasTemporaryProperties => TemporaryProperties.Count != 0;

        public AuditHistory ToAuditHistory()
        {
            return new AuditHistory
            {
                Entity = TableName,
                State = State,
                ModifiedOn = ModifiedOn,
                KeyValues = JsonSerializer.Serialize(KeyValues),
                OriginalValues = OriginalValues.Count == 0 ? string.Empty : JsonSerializer.Serialize(OriginalValues),
                CurrentValues = CurrentValues.Count == 0 ? string.Empty : JsonSerializer.Serialize(CurrentValues)
            };
        }
    }
}
