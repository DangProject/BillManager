using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core
{
    public class EntityMapper : IEntityMapper
    {
        public EntityMapper()
        {
            configurations = new Dictionary<Type, Type>();            
        }
        //public IDictionary<Type, Func<object, object>> Configurations { get ; set; }
        IDictionary<Type, Type> configurations;

        public void Configure<TFrom, TTo>()
        {
            configurations.Add(typeof(TFrom), typeof(TTo));
        }

        public static void PropertyMap<T, U>(T source, U destination)
            where T : class, new()
            where U : class, new()
        {   
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
        }

        public static U PropertyMap<T, U>(T source)
            where T : class, new()
        {
            U destination = Activator.CreateInstance<U>();
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (Exception e)
                    {  
                        Debug.Write(e.Message);
                    }
                }
            }

            return destination;
        }

        public TTo Map<TFrom, TTo>(TFrom source, TTo destination)
            where TFrom : class, new()
            where TTo : class, new()
        {            
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (Exception e)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(destinationProperty.PropertyType))
                        {
                            try
                            {
                                Type genericType = destinationProperty.PropertyType.GetGenericArguments()[0];

                                var listType = typeof(List<>).MakeGenericType(genericType);
                                IList list = Activator.CreateInstance(listType) as IList;

                                IList coll = sourceProperty.GetValue(source, null) as IList;
                                if (coll != null && coll.Count > 0)
                                {
                                    if (configurations.ContainsKey(coll[0].GetType()))
                                        foreach (object item in coll)
                                            list.Add(Map(item, Activator.CreateInstance(configurations[coll[0].GetType()])));
                                }

                                destinationProperty.SetValue(destination, list, null);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                if (configurations.ContainsKey(sourceProperty.PropertyType))
                                    destinationProperty.SetValue(destination, Map(sourceProperty.GetValue(source, null),
                                                        Activator.CreateInstance(configurations[sourceProperty.PropertyType])), null);
                            }
                            catch { }
                        }
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            return destination;
        }

        public TTo Map<TFrom, TTo>(TFrom source) 
            where TFrom : class, new()
            where TTo : class
        {
            TTo destination = Activator.CreateInstance<TTo>();
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {                    
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (Exception e)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(destinationProperty.PropertyType))
                        {
                            try
                            {
                                Type genericType = destinationProperty.PropertyType.GetGenericArguments()[0];

                                var listType = typeof(List<>).MakeGenericType(genericType);
                                IList list = Activator.CreateInstance(listType) as IList;

                                IList coll = sourceProperty.GetValue(source, null) as IList;
                                if (coll != null && coll.Count > 0)
                                {
                                    if (configurations.ContainsKey(coll[0].GetType()))
                                        foreach (object item in coll)
                                            list.Add(Map(item, Activator.CreateInstance(configurations[coll[0].GetType()])));                                    
                                }

                                destinationProperty.SetValue(destination, list, null);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {                                
                                if (configurations.ContainsKey(sourceProperty.PropertyType))
                                    destinationProperty.SetValue(destination, Map(sourceProperty.GetValue(source, null),
                                                        Activator.CreateInstance(configurations[sourceProperty.PropertyType])), null);                                
                            }
                            catch { }
                        }
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            return destination;
        }
    }
}


//if (Configurations.ContainsKey(genericType))
//    foreach (object item in coll)
//        list.Add(Configurations[genericType](item));

//if (Configurations.ContainsKey(destinationProperty.PropertyType))
//    destinationProperty.SetValue(destination, Configurations[destinationProperty.PropertyType](sourceProperty.GetValue(source, null)), null);