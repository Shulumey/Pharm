using System;

namespace BCC.Pharm.Shared
{
    /// <summary>
    /// Атрибут, который говорит нам, что изменение данного свойства будет отслеживаться в истории изменений. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HistoricalFieldAttribute : Attribute 
    {
        /// <summary>
        /// Отображаемое имя.
        /// </summary>
        public string Name { get; set; }
    }
}