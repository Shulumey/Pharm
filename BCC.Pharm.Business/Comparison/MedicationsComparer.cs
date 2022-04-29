using System.Collections.Generic;
using System.Linq;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts;

namespace BCC.Pharm.Business
{
    public class MedicationsComparer<T> : IObjectsComparer<T> where T : class
    {
        public ChangeSet<T> GetChangeSet(IReadOnlyCollection<T> originalObjects, IReadOnlyCollection<T> newObjects)
        {
            T[] updatedObjects = newObjects.Intersect(originalObjects).ToArray();
            T[] added = newObjects.Except(originalObjects).ToArray();
            T[] removed = originalObjects.Except(newObjects).ToArray();

            List<DifferencesObjects<T>> differencesObjects = new List<DifferencesObjects<T>>();
            
            foreach (T updatedObject in updatedObjects)
            {
                T originalObject = originalObjects.FirstOrDefault(x => updatedObject.Equals(x));
                if (originalObject != null && originalObject.GetDiffProps(updatedObject).Any())
                {
                    differencesObjects.Add(new DifferencesObjects<T>(originalObject, updatedObject));
                }
            }

            return new ChangeSet<T>(added, removed, differencesObjects);
        }
    }
}