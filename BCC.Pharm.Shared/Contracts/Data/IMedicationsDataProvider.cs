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

        /// <summary>
        /// Обновление информации о препаратах.
        /// </summary>
        /// <param name="differences">Объекты, которые изменились.</param>
        /// <param name="sourceChange">Каким способом были обновлены.</param>
        ///<param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task UpdateAsync(DifferencesObjects<MedicationDto> differences, SourceChange sourceChange, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление препаратов.
        /// </summary>
        /// <param name="medications">Препараты.</param>
        /// <param name="sourceChange">Каким способом были добавлены.</param>
        ///<param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task AddAsync(IReadOnlyCollection<MedicationDto> medications, SourceChange sourceChange, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medications">Препараты.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task DeleteAsync(IReadOnlyCollection<MedicationDto> medications, CancellationToken cancellationToken);
    }
}