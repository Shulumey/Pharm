using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Queries
{
    /// <summary>
    /// Запрос на получение истории изменений препарата.
    /// </summary>
    public static class GetMedicationHistory
    {
        public sealed class Query: IRequest<IReadOnlyCollection<MedicationHistoryDto>>
        {
            /// <summary>
            /// Id препарата.
            /// </summary>
            public int MedicationId { get; }

            public Query(int medicationId)
            {
                MedicationId = medicationId;
            }
        }

        /// <inheritdoc />
        public sealed class Handler: IRequestHandler<Query, IReadOnlyCollection<MedicationHistoryDto>>
        {
            /// <inheritdoc cref="IMedicationsDataProvider"/>
            private readonly IMedicationsDataProvider _dataProvider;

            public Handler(IMedicationsDataProvider dataProvider)
            {
                _dataProvider = dataProvider;
            }
            
            /// <inheritdoc />
            public async Task<IReadOnlyCollection<MedicationHistoryDto>> Handle(Query request, CancellationToken cancellationToken) =>
                await _dataProvider.GetMedicationHistoryAsync(request.MedicationId, cancellationToken);
        }
    }
}