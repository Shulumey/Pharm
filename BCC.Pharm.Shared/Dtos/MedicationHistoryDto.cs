using System;

namespace BCC.Pharm.Shared.Dtos
{
    public class MedicationHistoryDto
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public SourceChange SourceChange { get; set; }
        public string Parameter { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}