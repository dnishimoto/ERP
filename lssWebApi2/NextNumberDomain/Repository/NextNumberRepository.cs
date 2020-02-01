using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lssWebApi2;
using lssWebApi2.EntityFramework;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace lssWebApi2.NextNumberDomain
{
    public class NextNumberView
    {
        public long NextNumberId { get; set; }
        public string NextNumberName { get; set; }
        public long NextNumberValue { get; set; }

    }
    public class NextNumberRepository : Repository<NextNumber>, INextNumberRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public NextNumberRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public IQueryable<NextNumber> GetEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate)
        {
            //IQueryable<SalesOrder> result = _dbContext.Set<SalesOrder>().Where(predicate).AsQueryable<SalesOrder>();
            var result = _dbContext.Set<NextNumber>().Where(predicate);
            return result;
        }

        public async Task<NextNumber> GetEntityById(long? nextNumberId)
        {
            try
            {
                return await _dbContext.FindAsync<NextNumber>(nextNumberId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<NextNumber> GetEntityByNumber(long nextNumberValue)
        {
            try
            {
                var query = await (from detail in _dbContext.NextNumber
                                   where detail.NextNumberValue == nextNumberValue
                                   select detail).FirstOrDefaultAsync<NextNumber>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<NextNumber> GetNextNumber(string nextNumberName)
        {
            try
            {
                /*
                NextNumber nextNumber = await (from detail in _dbContext.NextNumber
                                               where detail.NextNumberName == nextNumberName
                                               select detail).FirstOrDefaultAsync<NextNumber>();

                nextNumber.NextNumberValue = nextNumber.NextNumberValue + 1;

                base.UpdateObject(nextNumber);
                _dbContext.SaveChanges();

                return nextNumber;
                */

                List < SqlParameter > parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@NextNumberName", nextNumberName));


                IList<NextNumber> nextNumber = await _dbContext.SqlQuery<NextNumber>(CommandType.Text, "usp_GetNextNumber @NextNumberName", parameters);

                /*
                 SqlParameter param1 = new SqlParameter("@NextNumberName", nextNumberName);
                //NextNumber nextNumber = await _dbContext.Database.SqlQuery<NextNumber>("usp_GetNextNumber @NextNumberName", param1).SingleAsync();

                var command = _dbContext.Database.GetDbConnection().CreateCommand();

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_GetNextNumber";
                command.Parameters.Add(param1);

                _dbContext.Database.OpenConnection();

        

                var queryResults = command.ExecuteReader();
                NextNumber nextNumber = new NextNumber();
                while (queryResults.Read())
                {
                    nextNumber.NextNumberId = (long) queryResults["NextNumberId"];
                    nextNumber.NextNumberName = queryResults["NextNumberName"].ToString();
                    nextNumber.NextNumberValue = (long)queryResults["NextNumberValue"];
                   


                }
                */
                return nextNumber[0];
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            //foreach (NextNumber item in query)
            //{
            //    nextNumber=item;
            // }
            /*
            NextNumber nextNumber = null;
            long? currentNextNumberValue = 0;
            using (DbContextTransaction scope = _dbListensoftwaredbContext.Database.BeginTransaction())
            {
                //Lock the table during this transaction
                nextNumber = await (from e in _dbListensoftwaredbContext.NextNumbers
                                               where e.NextNumberName == nextNumberName
                                               select e).FirstOrDefaultAsync<NextNumber>();

                currentNextNumberValue = nextNumber.NextNumberValue;
                nextNumber.NextNumberValue += 1;
                _dbListensoftwaredbContext.NextNumbers.Attach(nextNumber);
                _dbListensoftwaredbContext.Entry(nextNumber).State = EntityState.Modified;
                _dbListensoftwaredbContext.SaveChanges();
                nextNumber.NextNumberValue = currentNextNumberValue??0;

                scope.Commit();
            }
            */

        }

    }
}
