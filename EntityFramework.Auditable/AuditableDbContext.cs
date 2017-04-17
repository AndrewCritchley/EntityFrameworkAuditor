using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Auditable.Entities;
using EntityFramework.Auditable.Extensions;

namespace EntityFramework.Auditable
{
	public class AuditableDbContext : DbContext
	{
		private readonly EntityFrameworkAuditor _auditor;

		public IDbSet<AuditEntry> AuditEntries { get; set; }
		public IDbSet<AuditEntry> AuditEntryProperties { get; set; }

		public AuditableDbContext(EntityFrameworkAuditor auditor)
		{
			_auditor = auditor;
		}

		public override int SaveChanges()
		{
			this.AuditEntries.AddRange(this._auditor.GetAuditEntriesForModifiedEntities(this));
			this.AuditEntries.AddRange(this._auditor.GetAuditEntriesForDeletedEntities(this));

			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			this.AuditEntries.AddRange(this._auditor.GetAuditEntriesForModifiedEntities(this));
			this.AuditEntries.AddRange(this._auditor.GetAuditEntriesForDeletedEntities(this));

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
