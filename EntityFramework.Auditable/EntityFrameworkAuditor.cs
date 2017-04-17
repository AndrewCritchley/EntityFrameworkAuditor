using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EntityFramework.Auditable.Entities;

namespace EntityFramework.Auditable
{
	/// <summary>
	/// Allows changes in entities to be pulled out as AuditEntry records
	/// </summary>
	public class EntityFrameworkAuditor
	{
		private IEnumerable<DbEntityEntry> GetRecordsToAudit(DbContext dbContext, params EntityState[] states)
		{
			return dbContext.ChangeTracker.Entries().Where(e => e.Entity is IAuditableEntity && states.Contains(e.State));
		}

		/// <summary>
		/// Gets a list of entities that are modified or added that implement the IAuditableEntity interface
		/// </summary>
		public IEnumerable<AuditEntry> GetAuditEntriesForModifiedEntities(DbContext dbContext)
		{
			var entries = new List<AuditEntry>();
			var addedOrModifiedEntries = GetRecordsToAudit(dbContext, EntityState.Modified, EntityState.Added);

			foreach (var entry in addedOrModifiedEntries)
			{
				var auditEntry = new AuditEntry()
				{
					EntityType = entry.Entity.GetType().FullName,
					Action = AuditEntryAction.Modified
				};

				foreach (var propertyName in entry.CurrentValues.PropertyNames)
				{
					var oldValue = entry.OriginalValues[propertyName].ToString();
					var newValue = entry.CurrentValues[propertyName].ToString();

					auditEntry.Properties.Add(new AuditEntryProperty()
					{
						PropertyName = propertyName,
						OldValue = oldValue,
						NewValue = newValue
					});
				}

				entries.Add(auditEntry);
			}

			return entries;
		}

		/// <summary>
		/// Gets a list of entities that are deleted that implement the IAuditableEntity interface
		/// </summary>
		public IEnumerable<AuditEntry> GetAuditEntriesForDeletedEntities(DbContext dbContext)
		{
			var deletedEntities = GetRecordsToAudit(dbContext, EntityState.Deleted);

			return deletedEntities.Select(e => new AuditEntry()
			{
				EntityType = e.Entity.GetType().FullName,
				Action = AuditEntryAction.Deleted
			});
		}
	}
}