using System;

namespace BCC.Pharm.Shared.Dtos
{
    public class MedicationDto : IEquatable<MedicationDto>
    {
        public int Id { get; set; }
        public string ActiveSubstance { get; set; }
        public string Name { get; set; }
        
        [HistoricalField( Name = "Цена")]
        public decimal Price { get; set; }
        
        [HistoricalField( Name = "Количество")]
        public int Quantity { get; set; }
        
        public bool Equals(MedicationDto other)
        {
            if (other is null) return false;
            if (Name is null && other.Name is null) return true;
            if (other.Name is null && Name != null) return false;
            if (Name is null && other.Name != null) return false;

            return Name.ToUpper().Replace(" ", string.Empty).Trim() == other.Name.ToUpper().Replace(" ", string.Empty).Trim();
        }
    }
}