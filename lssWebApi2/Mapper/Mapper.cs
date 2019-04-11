using ERP_Core2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ERP_Core2.AutoMapper
{
    public class Mapper : AbstractErrorHandling
    {
        public Dictionary<string, string> dictAdditionalFields;
        public Mapper()
        {
            dictAdditionalFields = new Dictionary<string, string>();
        }
        private bool HasClassField(string fieldName, object entityObject)
        {
            bool retVal = false;
            Type t = entityObject.GetType();
            PropertyInfo propertyInfo = t.GetProperty(fieldName);
            if (propertyInfo != null) retVal = true;

            return retVal;
        }
        public T Map<T>(Object entityObject) where T : class, new()
        {
            try
            {
                var propts = typeof(T).GetProperties();

                T model;
                object val;

                model = new T();
                foreach (var viewFieldProperty in propts)
                {
                    bool hasField = false;

                    hasField = HasClassField(viewFieldProperty.Name, entityObject); 

                    if (hasField)
                    {

                        val = entityObject.GetType().GetProperty(viewFieldProperty.Name).GetValue(entityObject, null);
                        viewFieldProperty.SetValue(model, val);
     
                    }
                    else
                    {
                        if (dictAdditionalFields.ContainsKey(viewFieldProperty.Name))
                        {
                            string entityName = dictAdditionalFields[viewFieldProperty.Name];
                            string[] elements = entityName.Split(".");

                            

                            if (entityName.Contains('.'))
                            {
                                //class.fieldname pattern
                                var regex = new Regex(@"(\b\w*\b)\.(\b\w*\b)$");
                                Match match = regex.Match(entityName);

                                if (match.Success)
                                {
                                    string className = elements[0];
                                    string fieldName = elements[1];

                                    var obj = entityObject.GetType().GetProperty(className)?.GetValue(entityObject, null);
                                    val = obj.GetType().GetProperty(fieldName)?.GetValue(obj, null);
                                    viewFieldProperty.SetValue(model, val);
                                }
                            }
                            else
                            {
                                val = entityObject.GetType().GetProperty(entityName)?.GetValue(entityObject, null);
                                viewFieldProperty.SetValue(model, val);
                            }
                           
                        }
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
