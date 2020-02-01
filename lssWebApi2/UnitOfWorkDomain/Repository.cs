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


        public async Task<Udc> GetUdc2(string productCode, string keyCode)
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
            try
            {
                _dbContext.Set<T>().Remove(dataObject);
            }
            catch (Exception ex) { throw new Exception("DeleteObject", ex); }
        }
        public void DeleteObjects(List<T> dataObjects)
        {
            try
            {

                _dbContext.Set<T>().RemoveRange(dataObjects);
            }
            catch (Exception ex) { throw new Exception("DeleteObjects", ex); }
         }

        public void AddObject(T dataObject)
        {

            try
            {
                _dbContext.Set<T>().Add(dataObject);
            }
            catch (Exception ex) { throw new Exception("AddObject", ex); }

        }
        public void AddObjects(List<T> dataObjects)
        {
            try
            {
                _dbContext.Set<T>().AddRange(dataObjects);
            }
            catch (Exception ex) { throw new Exception("AddObjects", ex); }
        }
    }
}
