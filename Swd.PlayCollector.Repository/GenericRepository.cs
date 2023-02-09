using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Swd.PlayCollector.Repository
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity>
        where TEntity : class, new()
        where TModel : DbContext, new()

    {
        // Member
        private DbContext _dbContext;
        private DbSet<TEntity> _dbSet;


        // Properties
        public DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        // Constructor
        public GenericRepository()
        {
            Init(new TModel());
        }

        public GenericRepository(DbContext context)
        {
            Init(context);
        }

        // Methods
        private void Init(DbContext context)
        {
            _dbContext= context;
            _dbSet = context.Set<TEntity>();
        }

        // Add
        public void Add(TEntity t)
        {
            _dbSet.Add(t);
            _dbContext.SaveChanges();

        }

        public async Task AddAsync(TEntity t)
        {
            await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();    
        }

        // Delete
        public void Delete(object key)
        {
            TEntity existing = _dbSet.Find(key);

            if (existing != null)
            {
                _dbSet.Remove(existing);
                _dbContext.SaveChanges();

            }
        }

        public async Task DeleteAsync(object key)
        {
            TEntity existing = await _dbSet.FindAsync(key);

            if (existing != null)
            {
                _dbSet.Remove(existing);
                _dbContext.SaveChangesAsync();
            }
        }


        // Get All
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        // Get By Id
        public TEntity GetById(object key)
        {
            return _dbSet.Find(key);
        }

        public async Task<TEntity> GetByIdAsync(object key)
        {
            return await _dbSet.FindAsync(key);
            
        }

        // Update
        public void Update(TEntity t, object key)
        {
            TEntity existing = _dbSet.Find(key);

            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(t);
                _dbContext.SaveChanges();
                _dbContext.Entry(existing).Reload();
            }
        }

        public async Task UpdateAsync(TEntity t, object key)
        {
            TEntity existing = await _dbSet.FindAsync(key);

            if(existing != null)
            {
                _dbSet.Update(existing);
                _dbContext.SaveChangesAsync();
                _dbContext.Entry(existing).Reload();
            }
        }
    }
}
