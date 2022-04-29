using System.Collections.Generic;

namespace BCC.Pharm.Shared.Contracts
{
    /// <summary>
    /// Компаратор для сравнения 2-х множеств объектов. Что изменилось между ниими.
    /// </summary>
    /// <typeparam name="T">Типы объектов.</typeparam>
    public interface IObjectsComparer<T> where T:class
    {
        /// <summary>
        /// Метод сравнения 2-х множеств.
        /// </summary>
        /// <param name="originalObjects">Исходное множество.</param>
        /// <param name="newObjects">Множество, с которым необходимо сравнить.</param>
        /// <returns>Набор изменений, между 2-мя множествами.</returns>
        ChangeSet<T> GetChangeSet(IReadOnlyCollection<T> originalObjects, IReadOnlyCollection<T> newObjects);
    }
}