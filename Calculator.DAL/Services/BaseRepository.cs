using Calculator.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Services
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        private readonly CalculatorDbContext _calculatorDbContext;
        private DbSet<TEntity> dbSet;

        public BaseRepository(CalculatorDbContext calculatorDbContext)
        {
            _calculatorDbContext = calculatorDbContext;
            dbSet = _calculatorDbContext.Set<TEntity>();
        }

        public async Task Delete(TEntity item)
        {
           await Task.Run(()=>dbSet.Remove(item));
        }

        public async Task<TEntity> Get(Guid id)
        {
           var b =  await dbSet.FindAsync(id);
            return b;
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                var b = await orderBy(query).ToListAsync();
                return b;
            }
            else
            {
                var b = await query.ToListAsync();
                return b;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var b = await dbSet.ToListAsync();
            return b;
        }

        public async Task Insert(TEntity item)
        {
            await dbSet.AddAsync(item);
        }

        public async Task Update(TEntity item)
        {
            try
            {
             await Task.Run(()=>dbSet.Attach(item));
            }
            catch { }
            finally
            {
                await Task.Run(()=>dbSet.Update(item));
            }
        }
    }
}
