using ERP_Core2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Mapper
{
    public class Mapper : AbstractErrorHandling
    {
        public T Map<T>(Object o) where T : new()
        {
            try
            {
                var propts = typeof(T).GetProperties();

                T model;
                object val;

                model = new T();
                foreach (var l in propts)
                {
                    val = o.GetType().GetProperty(l.Name).GetValue(o, null);

                    if (val == DBNull.Value)
                    {
                        l.SetValue(model, null);
                    }
                    else
                    {
                        l.SetValue(model, val);
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
