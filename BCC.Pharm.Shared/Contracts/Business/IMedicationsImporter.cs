using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Shared.Contracts.Business
{
    /// <summary>
    /// Импорт данных о препаратах
    /// </summary>
    public interface IMedicationsImporter
    {
        /// <summary>
        /// Получение десириализованных данных.
        /// </summary>
        /// <param name="fileReader">Тексотвый поток с данными.</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<MedicationDto>> ReadAsync(TextReader fileReader);
    }
}