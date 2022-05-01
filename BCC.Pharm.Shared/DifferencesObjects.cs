using System.Collections.Generic;

namespace BCC.Pharm.Shared
{
    /// <summary>
    /// Инкапсулирует в себе разницу между 2-мя объектами типа <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DifferencesObjects<T> where T : class
    {
        /// <summary>
        /// Оригинальный объект.
        /// </summary>
        public T OriginalObject { get; }
        
        /// <summary>
        /// Новый измененный объект.
        /// </summary>
        public T NewObject { get; }
        
        /// <summary>
        /// Разница между свойствами обектов типа <see cref="T"/>.
        /// </summary>
        public IReadOnlyCollection<PropertyValueChange> Differences { get; }

        public DifferencesObjects(T originalObject, T newObject)
        {
            OriginalObject = originalObject;
            NewObject = newObject;
            Differences = originalObject.GetDiffProps(newObject);
        }
    }
}