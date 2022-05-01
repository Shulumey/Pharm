namespace BCC.Pharm.Shared
{
    /// <summary>
    /// Измененные свойство.
    /// </summary>
    public class PropertyValueChange
    {
        /// <summary>
        /// Название свойства.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Оригинальное значение.
        /// </summary>
        public string ValueBefore { get; set; }
        
        /// <summary>
        /// Новое значение.
        /// </summary>
        public string ValueAfter { get; set; }
    }
}