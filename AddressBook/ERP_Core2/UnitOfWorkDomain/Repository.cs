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

            //if (_dbContext.Entry(dataObject).State == EntityState.Detached || _dbContext.Entry(dataObject).State == EntityState.Modified)
            //{
                _dbContext.Entry(dataObject).State = System.Data.Entity.EntityState.Modified;
                //_dbContext.Set<T>().Attach(dataObject);
            //}

         

         }
        public async Task<ChartOfAcct> GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary)
        {
            Entities _dbEntities = (Entities)_dbContext;

            ChartOfAcct chartOfAcct= await (from e in _dbEntities.ChartOfAccts
                             where e.CompanyNumber==company
                             && e.BusUnit==busUnit
                             && e.ObjectNumber==objectNumber
                             && (e.Subsidiary??"")==subsidiary
                             select e).FirstOrDefaultAsync<ChartOfAcct>();

            return chartOfAcct;

        }
        public async Task<UDC> GetUdc(string productCode, string keyCode)
        {
            Entities _dbEntities = (Entities)_dbContext;

            UDC udc = await (from e in _dbEntities.UDCs
                                 where e.ProductCode == productCode
                                 && e.KeyCode == keyCode
                                 select e).FirstOrDefaultAsync<UDC>();

            return udc;
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
