using System.Collections.Generic;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Dtos;
using Newtonsoft.Json;

namespace BCC.Pharm.Business.Export
{
    /// <inheritdoc />
    public class JsonMedicationsExporter : IMedicationsExporter
    {
        /// <inheritdoc />
        public string GetExportedData(IReadOnlyCollection<MedicationDto> medications) => JsonConvert.SerializeObject(medications, Formatting.Indented);
    }
}