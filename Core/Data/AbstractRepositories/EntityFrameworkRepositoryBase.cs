using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Data.Interfaces;

namespace Core.Data.AbstractRepositories
{
    public abstract class EntityFrameworkRepositoryBase<T, U> : IEntityFrameworkRepository<T>
        where T : class, new()
        where U : DbContext, new()
    {
        protected abstract T AddEntity(U context, T entity);
        protected abstract T GetEntity(U context, int id);
        protected abstract T UpdateEntity(U context, T entity);
        protected abstract IEnumerable<T> GetEntities(U context);
        public T Add(T entity)
        {
            using (U context = new U())
            {
                T addedEntity = AddEntity(context, entity);
                context.SaveChanges();
                return addedEntity;
            }
        }

        public void Remove(T entity)
        {
            using (U context = new U())
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (U context = new U())
            {
                T entity = GetEntity(context, id);
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }        

        public T Update(T entity)
        {
            using (U context = new U())
            {
                T existingEntity = UpdateEntity(context, entity);       // gets the existing entity by entity.id

                EntityMapper.PropertyMap(entity, existingEntity);

                context.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (U context = new U())
                return (GetEntities(context)).ToArray().ToList();
        }

        public T Get(int id)
        {
            using (U context = new U())
                return GetEntity(context, id);
        }
    }
}
