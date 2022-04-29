using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Shared.Contracts.Business
{
    /// <summary>
    /// Импорт данных о препаратах из файла
    /// </summary>
    public interface IImportDataFile
    {
        Task<IReadOnlyCollection<MedicationDto>> LoadAsync(TextReader fileReader);
    }
}