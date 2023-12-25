using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityframeWork
{
	public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
		where TEntity : class, IEntity, new()
		where TContext : DbContext

	{
		protected readonly TContext Context;

		public EfEntityRepositoryBase(TContext applicationContext)
		{
			Context = applicationContext;
		}

		public TEntity Get(Expression<Func<TEntity, bool>> filter)
		{
			try
			{
				var result = Context.Set<TEntity>().FirstOrDefault(filter);
				return result;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw e;
			}
		}
		public IList<TEntity> GetUsers(Expression<Func<TEntity, bool>> filter)
		{
			return Context.Set<TEntity>().Where(filter).ToList();
		}
		public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
		{
			return filter == null
					? Context.Set<TEntity>().ToList()
					: Context.Set<TEntity>().Where(filter).ToList();
		}

		public void Add(TEntity entity)
		{
			var addedEntity = Context.Entry(entity);
			addedEntity.State = EntityState.Added;
			foreach (var navigation in addedEntity.Navigations)
			{
				if (navigation.Metadata.IsCollection)
				{
					var relatedEntities = (IEnumerable<object>)navigation.CurrentValue;
					foreach (var relatedEntity in relatedEntities)
					{
						Context.Entry(relatedEntity).State = EntityState.Added;
					}
				}
				else
				{
					var relatedEntity = navigation.CurrentValue;
					if (relatedEntity != null)
					{
						Context.Entry(relatedEntity).State = EntityState.Added;
					}
				}
			}
			Context.SaveChanges();
		}

		public void Delete(TEntity entity)
		{
			var deletedEntity = Context.Entry(entity);
			deletedEntity.State = EntityState.Deleted;
			Context.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			var updatedEntity = Context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			foreach (var navigation in Context.Entry(entity).Navigations)
			{
				if (navigation.Metadata.IsCollection)
				{
					var relatedEntities = (IEnumerable<object>)navigation.CurrentValue;
					foreach (var relatedEntity in relatedEntities)
					{
						var primaryKey = Context.Entry(relatedEntity).Metadata.FindPrimaryKey();
						var keyValues = primaryKey.Properties.Select(p => Context.Entry(relatedEntity).Property(p.Name).CurrentValue).ToArray();
						var isExistingEntity = keyValues.All(k => k != null && (int)k > 0);

						Context.Entry(relatedEntity).State = isExistingEntity ? EntityState.Modified : EntityState.Added;
					}
				}
				else
				{
					var relatedEntity = navigation.CurrentValue;
					if (relatedEntity != null)
					{
						var primaryKey = Context.Entry(relatedEntity).Metadata.FindPrimaryKey();
						var keyValues = primaryKey.Properties.Select(p => Context.Entry(relatedEntity).Property(p.Name).CurrentValue).ToArray();
						var isExistingEntity = keyValues.All(k => k != null && (int)k > 0);

						Context.Entry(relatedEntity).State = isExistingEntity ? EntityState.Modified : EntityState.Added;
					}
				}
			}

			Context.SaveChanges();
		}
	}
}