using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using System.Linq.Expressions;

namespace lssWebApi2.MapperAbstract
{

    public abstract class MapperAbstract<T1,T2View> where T1 : class where T2View:class
    {
        //public async Task<T1> MapToEntity(T2 inputObject)
        //{
            public Mapper mapper = new Mapper();

        public abstract Task<T1> MapToEntity(T2View inputObject);
        public abstract Task<T2View> GetViewById(long ? Id);
        public abstract Task<T1> GetEntityById(long? Id);
        public abstract Task<IList<T1>> MapToEntity(IList<T2View> inputObjects);
        public abstract Task<T2View> MapToView(T1 inputObject);
        public string GetMyMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

    }
}
