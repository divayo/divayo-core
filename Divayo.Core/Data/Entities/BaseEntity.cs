using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Divayo.Core.Data.Entities
{
    /// <summary>
    ///     Base Entity
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        ///     Id of the entity
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        
        /// <summary>
        ///     Date the entity was created
        /// </summary>
        [Required]
        public virtual DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     Name of the user that created the entity
        /// </summary>
        [Required]
        [StringLength(128)]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        ///     Date of the last update to the entity
        /// </summary>
        public virtual DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        ///     Name of the user that last updated the entity
        /// </summary>
        [StringLength(128)]
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        ///     Date when the entity was marked as deleted
        /// </summary>
        public virtual DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        ///     Name of the user that marked the entity as deleted
        /// </summary>
        [StringLength(128)]
        public virtual string DeletedBy { get; set; }

        /// <summary>
        ///     Reason why the entity was marked as deleted
        /// </summary>
        [StringLength(512)]
        public virtual string DeletedReason { get; set; }

        /// <summary>
        ///     Check if the entity is deleted
        /// </summary>
        public virtual bool IsDeleted => DeletedAt.HasValue && DeletedAt.Value <= DateTimeOffset.Now;
    }
}
