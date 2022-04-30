using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BCC.Pharm.Shared.Dtos
{
    [XmlType(TypeName="Medication")]
    public class MedicationDto : IEquatable<MedicationDto>
    {
        private static Comparer _comparer;
        
        [XmlIgnore]
        [JsonIgnore]
        public int Id { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public string ActiveSubstance { get; set; }
        
        public string Name { get; set; }
        
        [HistoricalField( Name = "Цена")]
        public decimal Price { get; set; }
        
        [HistoricalField( Name = "Количество")]
        public int Quantity { get; set; }

        public static IEqualityComparer<MedicationDto> DefaultComparer => _comparer ?? (_comparer = new Comparer());

        public bool Equals(MedicationDto other)
        {
            if (other is null) return false;
            if (Name is null && other.Name is null) return true;
            if (other.Name is null && Name != null) return false;
            if (Name is null && other.Name != null) return false;

            return Name.Normilize() == other.Name.Normilize();
        }
        
        class Comparer: IEqualityComparer<MedicationDto>
        {
            public bool Equals(MedicationDto x, MedicationDto y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name.Normilize() == y.Name.Normilize();
            }

            public int GetHashCode(MedicationDto obj) => obj.Name != null ? obj.Name.GetHashCode() : 0;
        }
    }
}