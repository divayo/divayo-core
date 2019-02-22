using System;

namespace Divayo.Core.Data.Entities
{
    public interface IBaseEntity
    {
        DateTimeOffset CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        string DeletedBy { get; set; }
        string DeletedReason { get; set; }
        Guid Id { get; set; }
        bool IsDeleted { get; }
        DateTimeOffset? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}