using Divayo.Core.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Divayo.Core.Data
{
    public class DivayoDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;

        public DivayoDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public override int SaveChanges()
        {
            SetMetaData();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetMetaData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetMetaData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetMetaData();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var p in builder.Model
                                .GetEntityTypes()
                                .SelectMany(t => t.GetProperties())
                                .Where(p =>
                                    p.Name == "Id" &&
                                    p.ClrType == typeof(Guid)))
            {
                p.SqlServer().DefaultValueSql = "NEWID()";
            }
            

            base.OnModelCreating(builder);
        }

        protected virtual void SetMetaData()
        {
            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();

            var userId = string.Empty;
            var currentUsername = "Anonymous";

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            try
            {

                userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception)
            {

            }

            try
            {
                currentUsername = !string.IsNullOrEmpty(httpContextAccessor.HttpContext.User?.Identity?.Name)
                ? httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";
            }
            catch (Exception)
            {

            }

            var now = DateTimeOffset.UtcNow;

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = userId ?? currentUsername;
                    entity.CreatedAt = now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedBy = userId ?? currentUsername;
                    entity.UpdatedAt = now;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entity.DeletedBy = userId ?? currentUsername;
                    entity.DeletedAt = now;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
