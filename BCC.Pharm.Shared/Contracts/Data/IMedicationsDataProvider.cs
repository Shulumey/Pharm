using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Shared.Contracts.Data
{
    /// <summary>
    /// Провайдер данных для работы с препаратами.
    /// </summary>
    public interface IMedicationsDataProvider
    {
        /// <summary>
        /// Получение актуальных данных о препаратах.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<MedicationDto>> GetAllMedicationsAsync(CancellationToken cancellationToken);
    }
}