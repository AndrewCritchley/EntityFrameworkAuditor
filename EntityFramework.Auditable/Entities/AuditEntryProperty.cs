namespace EntityFramework.Auditable.Entities
{
	public class AuditEntryProperty
	{
		public int AuditEntryPropertyId { get; set; }
		
		public int AuditEntryId { get; set; }
		public virtual AuditEntry AuditEntry { get; set; }

		public string PropertyName { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
	}
}
