using System.Collections.Generic;
using System.Data.Entity;

namespace EntityFramework.Auditable.Extensions
{
	public static class DbSetExtensions
	{
		public static void AddRange<TEntity>(this IDbSet<TEntity> dbSet, IEnumerable<TEntity> entities) where TEntity : class
		{
			foreach (var entity in entities)
			{
				dbSet.Add(entity);
			}
		}
	}
}
