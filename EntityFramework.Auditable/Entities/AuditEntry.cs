using System;
using System.Collections.Generic;

namespace EntityFramework.Auditable.Entities
{
	public class AuditEntry
	{
		public int AuditEntryId { get; set; }

		public DateTime Timestamp { get; set; }

		public string EntityType { get; set; }

		public AuditEntryAction Action { get; set; }

		public ICollection<AuditEntryProperty> Properties { get; set; }

		public AuditEntry()
		{
			this.Properties = new List<AuditEntryProperty>();
			this.Timestamp = DateTime.Now;
		}
	}
}
