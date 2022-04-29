using System.Collections.Generic;
using System.Linq;

namespace BCC.Pharm.Shared
{
    /// <summary>
    /// Набор изменений после сравнения 2-х множеств объектов типа <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChangeSet<T> where T:class
    {
        public IReadOnlyCollection<T> Added { get; }
        public IReadOnlyCollection<T> Removed { get; }
        public IReadOnlyCollection<DifferencesObjects<T>> Updated { get; }

        public ChangeSet(IReadOnlyCollection<T> added = null, IReadOnlyCollection<T> removed = null, IReadOnlyCollection<DifferencesObjects<T>> updated = null)
        {
            Added = added ?? Enumerable.Empty<T>().ToArray();
            Removed = removed ?? Enumerable.Empty<T>().ToArray();
            Updated = updated ?? Enumerable.Empty<DifferencesObjects<T>>().ToArray();
        }
    }
}