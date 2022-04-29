using System;
using System.Collections.Generic;
using System.Reflection;

namespace BCC.Pharm.Shared
{
    public static class ObjectExtension
    {
        public static PropertyValueChange[] GetDiffProps<T>(this T source, T target) 
            where T : class
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            List<PropertyValueChange> changes = new List<PropertyValueChange>();

            foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Public))
            {
                HistoricalFieldAttribute fieldAttribute = property.GetCustomAttribute<HistoricalFieldAttribute>();
                if (fieldAttribute != null)
                {
                    object sourceValue = property.GetValue(source);
                    object targetValue = property.GetValue(target);
                    
                    if (sourceValue != targetValue)
                    {
                        changes.Add(new PropertyValueChange
                        {
                            Name = string.IsNullOrEmpty(fieldAttribute.Name) ? property.Name : fieldAttribute.Name,
                            ValueBefore = source?.ToString(),
                            ValueAfter = target?.ToString()
                        });
                    }
                }
            }

            return changes.ToArray();
        }
    }
}