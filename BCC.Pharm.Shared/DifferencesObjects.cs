using System.Collections.Generic;

namespace BCC.Pharm.Shared
{
    public class DifferencesObjects<T> where T : class
    {
        public T OriginalObject { get; }
        public T NewObject { get; }
        public IReadOnlyCollection<PropertyValueChange> Differences { get; }

        public DifferencesObjects(T originalObject, T newObject)
        {
            OriginalObject = originalObject;
            NewObject = newObject;
            Differences = originalObject.GetDiffProps(newObject);
        }
    }
}