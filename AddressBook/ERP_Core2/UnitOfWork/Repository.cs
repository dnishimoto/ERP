using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{
    public class Repository<T> where T : class
    {
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
             _dbContext=dbContext;  
        }
        public void UpdateObject(T dataObject)
        {

            _dbContext.Entry(dataObject).State = System.Data.Entity.EntityState.Modified;
         }
        public async Task<T> GetObjectAsync(int id)
        {
                Task<T> result = _dbContext.Set<T>().FindAsync(id);
                return await result;
        }
        public IQueryable<T> GetObjectsAsync(Expression<Func<T, bool>> predicate,string includeTable="")
        {
            IQueryable<T> result = _dbContext.Set<T>().Where(predicate);
            if (includeTable != "")
            {
                result=result.Include(includeTable);
            }
                
            
                return result;
        }
       
        public void DeleteObject(T dataObject)
        {

                _dbContext.Set<T>().Remove(dataObject);

        }
        public void DeleteObjects(List<T> dataObjects)
        {

                _dbContext.Set<T>().RemoveRange(dataObjects);
         }

        public void AddObject(T dataObject)
        {

                _dbContext.Set<T>().Add(dataObject);

        }
        public void AddObjects(List<T> dataObjects)
        {
            _dbContext.Set<T>().AddRange(dataObjects);
        }
    }
}
