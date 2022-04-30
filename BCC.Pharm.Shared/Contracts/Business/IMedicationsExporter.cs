using System.Collections.Generic;
using System.IO;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Shared.Contracts.Business
{
    /// <summary>
    /// Экспорт прайс-листа.
    /// </summary>
    public interface IMedicationsExporter
    {
        /// <summary>
        /// Получение сериализванных данных в определенном текстовом формате.
        /// </summary>
        /// <param name="medications">Препараты.</param>
        /// <returns>Сериализванные данные</returns>
        string GetExportedData(IReadOnlyCollection<MedicationDto> medications);
    }
}