using lssWebApi2.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.EntityFramework;
using System.Data.Common;
using System.Data;
using lssWebApi2;

namespace lssWebApi2.Services
{
    public static class EF_Extension
    {
        //https://github.com/aspnet/EntityFrameworkCore/issues/10262
        public static async Task<IList<T>> SqlQuery<T>(this DbContext db, CommandType type, string sql, List<SqlParameter> parameters) where T : new()
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = type;
                    if (parameters != null && parameters.Count() > 0)
                    {
                        foreach (var item in parameters)
                        {
                            DbParameter p = command.CreateParameter();
                            p.DbType = item.DbType;
                            p.ParameterName = item.ParameterName;
                            p.Value = item.Value;
                            command.Parameters.Add(p);
                        }
                    }
                    var propts = typeof(T).GetProperties();
                    var rtnList = new List<T>();
                    T model;
                    object val;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            model = new T();
                            foreach (var l in propts)
                            {
                                val = reader[l.Name];
                                if (val == DBNull.Value)
                                    l.SetValue(model, null);
                                else
                                    l.SetValue(model, val);
                            }
                            rtnList.Add(model);
                        }
                    }
                    return rtnList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SqlQuery", ex);

            }
        }


        public class ResultDTO
        {
            public string UserName { get; set; }
            public string TrueName { get; set; }
        }

    }
    public class Repository<T> where T : class
    {
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
             _dbContext=dbContext;
           
        }
        public string GetMyMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }
        public void UpdateObject(T dataObject)
        {

            //if (_dbContext.Entry(dataObject).State == EntityState.Detached || _dbContext.Entry(dataObject).State == EntityState.Modified)
            //{
                _dbContext.Entry(dataObject).State = EntityState.Modified;
            //_dbContext.Set<T>().Attach(dataObject);
            //}



        }
    

        public async Task<NextNumber> GetNextNumber(string nextNumberName)
        {
            try
            {

                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add( new SqlParameter("@NextNumberName", nextNumberName));

       
                IList<NextNumber> nextNumber=await _dbContext.SqlQuery<NextNumber>(CommandType.Text, "usp_GetNextNumber @NextNumberName",parameters);

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
        public String BuildLongDate(DateTime ? myDate)
        {
            String year, month, day = "";
            string hour, minute = "";
            try
            {
                year = myDate?.Year.ToString();
                month = myDate?.Month.ToString().PadLeft(2, '0');
                day = myDate?.Day.ToString().PadLeft(2, '0');


                if (myDate?.TimeOfDay.ToString()== "PM")
                {
                    hour = (myDate?.Hour + 12).ToString().PadLeft(2,'0');
                }
                else
                {
                    hour = myDate?.Hour.ToString().PadLeft(2,'0');
                }
                minute = myDate?.Minute.ToString().PadLeft(2, '0');

                return year + month + day + hour + minute + "00";
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public DateTime AddTimeShortDate(DateTime currentDate, int durationHours,int durationMinutes)
        {
            TimeSpan time = new TimeSpan(0, durationHours, durationMinutes, 0);
            return currentDate.Add(time);
        }
        public DateTime BuildShortDate(DateTime currentDate, string stringShiftTime)
        {
            int shiftTime = Int32.Parse(stringShiftTime);
            int hour = shiftTime / 100;
            int minute = shiftTime - (hour * 100);


            TimeSpan time = new TimeSpan(0, hour, minute, 0);
            return currentDate.Add(time);           
        }
        public String BuildLongDate(DateTime ? myDate, String stringHours )
        {
            try
            {
                int hours = Int32.Parse(stringHours);

                String year, month, day = "";
                string myLongTime = "0" + hours + "00";
                myLongTime = myLongTime.Substring(myLongTime.Length - 6);

                year = myDate?.Year.ToString();
                month = myDate?.Month.ToString().PadLeft(2, '0');
                day = myDate?.Day.ToString().PadLeft(2, '0');

                //longHours = myDate.Hour.ToString().PadLeft(2, '0');
                //minutes = myDate.Minute.ToString().PadLeft(2, '0');
                //seconds = myDate.Second.ToString().PadLeft(2, '0');
                return year + month + day + myLongTime;
                //return year + month + day + longHours + minutes + seconds;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
       
        public async Task<Company> GetCompany()
        {
            try
            {
                ListensoftwaredbContext _dbListensoftwaredbContext = (ListensoftwaredbContext)_dbContext;


                Company company = await (from e in _dbListensoftwaredbContext.Company
                                         where e.CompanyId == 1
                                         select e).FirstOrDefaultAsync<Company>();

                return company;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ChartOfAccount> GetChartofAccount(string companyCode, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {
                ListensoftwaredbContext _dbListensoftwaredbContext = (ListensoftwaredbContext)_dbContext;


                ChartOfAccount chartOfAcct = await (from e in _dbListensoftwaredbContext.ChartOfAccount
                                                 where e.CompanyCode == companyCode
                                                 && e.BusUnit == busUnit
                                                 && e.ObjectNumber == objectNumber
                                                 && (e.Subsidiary ?? "") == subsidiary
                                                 select e).FirstOrDefaultAsync<ChartOfAccount>();

                return chartOfAcct;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
  
        public async Task<TaxRatesByCode> GetTaxRatesByCode(string TaxCode)
        {
            try
            {
                ListensoftwaredbContext _dbListensoftwaredbContext = (ListensoftwaredbContext)_dbContext;

                TaxRatesByCode tax = await (from e in _dbListensoftwaredbContext.TaxRatesByCode
                                            where e.TaxCode == TaxCode
                                            select e).FirstOrDefaultAsync<TaxRatesByCode>();

                return tax;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Udc> GetUdc(string productCode, string keyCode)
        {
            try
            {
                ListensoftwaredbContext _dbListensoftwaredbContext = (ListensoftwaredbContext)_dbContext;

                Udc udc = await (from e in _dbListensoftwaredbContext.Udc
                                 where e.ProductCode == productCode
                                 && e.KeyCode == keyCode
                                 select e).FirstOrDefaultAsync<Udc>();

                return udc;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<T> GetObjectAsync(long id)
        {
                Task<T> result = _dbContext.Set<T>().FindAsync(id);
                return await result;
        }
        public async Task<T> GetObjectAsyncByPredicate(Expression<Func<T, bool>> predicate)
        {
            try
            {
                T result = await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync<T>();
                return result;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /*
        public IQueryable<T> GetObjectsQueryable(Expression<Func<T, bool>> predicate,string includeTable="")
        {
            IQueryable<T> result = _dbContext.Set<T>().Where(predicate);
            if (includeTable != "")
            {
                result=result.Include(includeTable);
            }
                
            
                return result;
        }
        */
       
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
